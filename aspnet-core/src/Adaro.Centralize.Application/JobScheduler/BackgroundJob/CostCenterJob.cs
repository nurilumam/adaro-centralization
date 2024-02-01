using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Adaro.Centralize.Authorization.Roles;
using Adaro.Centralize.Authorization.Users.Dto;
using Adaro.Centralize.Authorization.Users.Importing.Dto;
using Adaro.Centralize.Notifications;
using Adaro.Centralize.Storage;
using Adaro.Centralize.SAPConnector.Dto.Synchronize;
using Abp.Domain.Repositories;

namespace Adaro.Centralize.SAPConnector.JobScheduler
{
    public class CostCenterJob : AsyncBackgroundJob<ImportCostCenterArgs>, ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ICostCentersSynchService _costCentersSynchService;

        public CostCenterJob(
            IObjectMapper objectMapper,
            ICostCentersSynchService costCentersSynchService)
        {
            _objectMapper = objectMapper;
            _costCentersSynchService = costCentersSynchService;
        }

        public override async Task ExecuteAsync(ImportCostCenterArgs args)
        {
            var param = new Dtos.CostCenterSynchDto();
            param.Year = args.Year;

            await _costCentersSynchService.SynchronizeFromSAP(param);
        }


    }
}
