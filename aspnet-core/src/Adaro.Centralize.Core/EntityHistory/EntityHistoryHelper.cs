﻿using Adaro.Centralize.SAPConnector;
using System;
using System.Linq;
using Abp.Organizations;
using Adaro.Centralize.Authorization.Roles;
using Adaro.Centralize.MultiTenancy;

namespace Adaro.Centralize.EntityHistory
{
    public static class EntityHistoryHelper
    {
        public const string EntityHistoryConfigurationName = "EntityHistory";

        public static readonly Type[] HostSideTrackedTypes =
        {
            typeof(GeneralLedgerAccount),
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(GeneralLedgerAccount),
            typeof(OrganizationUnit), typeof(Role)
        };

        public static readonly Type[] TrackedTypes =
            HostSideTrackedTypes
                .Concat(TenantSideTrackedTypes)
                .GroupBy(type => type.FullName)
                .Select(types => types.First())
                .ToArray();
    }
}