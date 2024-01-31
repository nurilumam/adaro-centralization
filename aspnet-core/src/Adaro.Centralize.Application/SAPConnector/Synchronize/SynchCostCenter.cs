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

namespace Adaro.Centralize.SAPConnector.Synchronize
{
    public class SynchCostCenter : AsyncBackgroundJob<ImportCostCenterArgs>, ITransientDependency
    {
        private readonly RoleManager _roleManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICostCentersAppService _costCentersAppService;

        public SynchCostCenter (
            RoleManager roleManager,
            IAppNotifier appNotifier,
            IBinaryObjectManager binaryObjectManager,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            ICostCentersAppService costCentersAppService)
        {
            _roleManager = roleManager;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _costCentersAppService = costCentersAppService;
        }

        public override async Task ExecuteAsync(ImportCostCenterArgs args)
        {

        }


    }
}
