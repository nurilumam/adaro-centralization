using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.SAPConnector.Exporting;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Adaro.Centralize.Storage;
using AdaroConnect.Application.Core.Abstracts;
using AdaroConnect.Application.Core.Models;
using Adaro.Centralize.Common;
using Abp.Domain.Uow;
using PayPalCheckoutSdk.Orders;

namespace Adaro.Centralize.SAPConnector
{
    public class SAPSynchService : CentralizeAppServiceBase, ISAPSynchService
    {
        private readonly IRepository<CostCenter, Guid> _costCenterRepository;
        private readonly IRepository<EKKO, Guid> _SAP_EKKORepository;
        private readonly IRepository<EKPO, Guid> _SAP_EKPORepository;
        private readonly ICostCenterManager _costCenterManagerSAP;
        private readonly IPurchaseOrderManager _purchaseOrderManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;


        public SAPSynchService(
            IRepository<CostCenter, Guid> costCenterRepository,
            IRepository<EKKO, Guid> SAP_EKKORepository,
            IRepository<EKPO, Guid> SAP_EKPORepository,
            ICostCenterManager costCenterManagerSAP,
            IPurchaseOrderManager purchaseOrderManager,
            IUnitOfWorkManager unitOfWorkManager) 
        {
            _costCenterRepository = costCenterRepository;
            _SAP_EKKORepository = SAP_EKKORepository;
            _SAP_EKPORepository = SAP_EKPORepository;
            _costCenterManagerSAP = costCenterManagerSAP;
            _purchaseOrderManager = purchaseOrderManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public virtual async Task<DtoResponseModel> SynchronizeFromSAP(CostCenterSynchDto input)
        {
            var response = new DtoResponseModel();

            try
            {
                CostCenterGetListOutputParameter result = await _costCenterManagerSAP.GetCostCenterAsync();
                response.AddMessage($"Total Cost Center from SAP : {result.CostCenter.Count()}");

                if (result.CostCenter != null && result.CostCenter.Count() > 0)
                {
                    var listCostCenter = result.CostCenter.GroupBy(x => x.CostCenter).Select(x => x.FirstOrDefault()).ToList();
                    var costCenterNames = result.CostCenter.Select(x => x.CostCenter).ToList();

                    using (var uow = _unitOfWorkManager.Begin())
                    {
                        var existingCostCenters = _costCenterRepository
                            .GetAll()
                            .Where(x => costCenterNames.Contains(x.CostCenterName))
                            .ToList();

                        if (existingCostCenters != null && existingCostCenters.Count() > 0)
                        {
                            response.AddMessage($"Total Cost Center to Update : {existingCostCenters.Count()}");

                            foreach (var updateCostCenter in existingCostCenters)
                            {
                                var itemCostCenter = result.CostCenter.FirstOrDefault(x => x.CostCenter == updateCostCenter.CostCenterName);
                                if(itemCostCenter != null)
                                {
                                    ObjectMapper.Map(itemCostCenter, updateCostCenter);
                                    _costCenterRepository.Update(updateCostCenter);
                                }
                            }

                            listCostCenter = listCostCenter.Where(x => !existingCostCenters.Select(y => y.CostCenterName).Contains(x.CostCenter)).ToList();
                        }

                        if (listCostCenter != null && listCostCenter.Count > 0)
                        {
                            response.AddMessage($"Total Cost Center to Insert : {listCostCenter.Count}");
                            var xx = new List<CostCenter>();

                            foreach (var itemCostCenter in listCostCenter)
                            {
                                var costCenter = ObjectMapper.Map<CostCenter>(itemCostCenter);
                                costCenter.CostCenterName = itemCostCenter.CostCenter;
                                xx.Add(costCenter);

                                if (AbpSession.TenantId != null)
                                {
                                    costCenter.TenantId = (int?)AbpSession.TenantId;
                                }

                                await _costCenterRepository.InsertAsync(costCenter);
                            }
                        }

                        await uow.CompleteAsync();
                    }
                }

                response.SetSuccess();

            }
            catch(Exception ex) {
                response.AddMessage(L(ex.Message));
                response.SetError();
            }

            return response;
        }

        public virtual async Task<DtoResponseModel> PurchasingDocumentHeader(PurchasingOrderSynchDto input)
        {
            var response = new DtoResponseModel();

            try
            {
                using (var uow = _unitOfWorkManager.Begin())
                {                    
                    var result = await _purchaseOrderManager.GetPurchaseOrderItem(new SAPGeneralParameterModel() { CompanyCode = "2000", DateFrom = input.DateFrom });
                    response.AddMessage($"Total Purchasing Document Header from SAP : {result.Count()}");

                    if (result != null && result.Count() > 0)
                    {

                        var DocumentItems = result.Select(x => x.UNIQUEID).ToList();
                        var Exisiting_EKPO = _SAP_EKPORepository.GetAll().Where(x => DocumentItems.Contains(x.UNIQUEID)).ToList();

                        if (Exisiting_EKPO != null && Exisiting_EKPO.Count() > 0)
                        {
                            response.AddMessage($"Total Purchasing Document Header to Update : {Exisiting_EKPO.Count()}");

                            foreach (var E_EKPO in Exisiting_EKPO)
                            {
                                var itemEKKO = result.FirstOrDefault(x => x.UNIQUEID == E_EKPO.UNIQUEID);
                                ObjectMapper.Map(itemEKKO, E_EKPO);
                                await _SAP_EKPORepository.UpdateAsync(E_EKPO);
                            }

                            result = result.Where(x => !Exisiting_EKPO.Select(y => y.UNIQUEID).Contains(x.UNIQUEID)).ToList();
                        }

                        if (result != null && result.Count > 0)
                        {
                            response.AddMessage($"Total Purchasing Document Header to Insert : {result.Count}");
                            var xx = new List<CostCenter>();

                            foreach (var itemEKPO in result)
                            {
                                var newEKPO = ObjectMapper.Map<EKPO>(itemEKPO);

                                if (AbpSession.TenantId != null)
                                {
                                    newEKPO.TenantId = (int?)AbpSession.TenantId;
                                }

                                await _SAP_EKPORepository.InsertAsync(newEKPO);
                            }
                        }

                    }

                    await uow.CompleteAsync();
                }
                
                response.SetSuccess();
            }
            catch (Exception ex)
            {
                response.AddMessage(L(ex.Message));
                response.SetError();
            }

            return response;
        }

        public virtual async Task<DtoResponseModel> PurchasingDocumentItem(PurchasingOrderSynchDto input)
        {
            var response = new DtoResponseModel();

            try
            {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    var result = await _purchaseOrderManager.GetPurchaseOrderItem(new SAPGeneralParameterModel() { CompanyCode = "2000", DateFrom = input.DateFrom });
                    response.AddMessage($"Total Purchasing Document Header from SAP : {result.Count()}");

                    if (result != null && result.Count() > 0)
                    {

                        var DocumentNumbers = result.Select(x => x.EBELN).ToList();
                        var Exisiting_EKKO = _SAP_EKKORepository.GetAll().Where(x => DocumentNumbers.Contains(x.EBELN)).ToList();

                        if (Exisiting_EKKO != null && Exisiting_EKKO.Count() > 0)
                        {
                            response.AddMessage($"Total Purchasing Document Header to Update : {Exisiting_EKKO.Count()}");

                            foreach (var E_EKKO in Exisiting_EKKO)
                            {
                                var itemEKKO = result.FirstOrDefault(x => x.EBELN == E_EKKO.EBELN);
                                ObjectMapper.Map(itemEKKO, E_EKKO);
                                await _SAP_EKKORepository.UpdateAsync(E_EKKO);
                            }

                            result = result.Where(x => !Exisiting_EKKO.Select(y => y.EBELN).Contains(x.EBELN)).ToList();
                        }

                        if (result != null && result.Count > 0)
                        {
                            response.AddMessage($"Total Purchasing Document Header to Insert : {result.Count}");
                            var xx = new List<CostCenter>();

                            foreach (var itemEKKO in result)
                            {
                                var newEKKO = ObjectMapper.Map<EKKO>(itemEKKO);

                                if (AbpSession.TenantId != null)
                                {
                                    newEKKO.TenantId = (int?)AbpSession.TenantId;
                                }

                                await _SAP_EKKORepository.InsertAsync(newEKKO);
                            }
                        }

                    }

                    await uow.CompleteAsync();
                }

                response.SetSuccess();
            }
            catch (Exception ex)
            {
                response.AddMessage(L(ex.Message));
                response.SetError();
            }

            return response;
        }
    }
}