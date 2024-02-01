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
using Abp.EntityFrameworkCore.Repositories;

namespace Adaro.Centralize.SAPConnector
{
    public class CostCentersSynchService : CentralizeAppServiceBase, ICostCentersSynchService
    {
        private readonly IRepository<CostCenter, Guid> _costCenterRepository;
        private readonly ICostCenterManager _costCenterManagerSAP;
        private readonly IUnitOfWorkManager _unitOfWorkManager;


        public CostCentersSynchService(
            IRepository<CostCenter, Guid> costCenterRepository,
            ICostCenterManager costCenterManagerSAP,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _costCenterRepository = costCenterRepository;
            _costCenterManagerSAP = costCenterManagerSAP;
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
                                var itemCostCenter = result.CostCenter.FirstOrDefault(x => x.Name == updateCostCenter.CostCenterName);
                                ObjectMapper.Map(updateCostCenter, itemCostCenter);
                            }


                            listCostCenter = listCostCenter.Where(x =>
                                !existingCostCenters.Select(y => y.CostCenterName).Contains(x.Name))
                                .ToList();
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
    }
}