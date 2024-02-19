using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Common;

namespace Adaro.Centralize.SAPConnector
{
    public interface ISAPSynchService : IApplicationService
    {
        Task<DtoResponseModel> SynchronizeFromSAP(CostCenterSynchDto input);

        Task<DtoResponseModel> PurchasingDocumentHeader(PurchasingOrderSynchDto input);
    }
}