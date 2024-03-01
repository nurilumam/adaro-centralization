using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Adaro.Centralize.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var lookupPages = pages.CreateChildPermission(AppPermissions.Pages_LookupPages, L("LookupPages"));
            lookupPages.CreateChildPermission(AppPermissions.Pages_LookupPages_Create, L("CreateNewLookupPage"));
            lookupPages.CreateChildPermission(AppPermissions.Pages_LookupPages_Edit, L("EditLookupPage"));
            lookupPages.CreateChildPermission(AppPermissions.Pages_LookupPages_Delete, L("DeleteLookupPage"));

            var transferBudgetItems = pages.CreateChildPermission(AppPermissions.Pages_TransferBudgetItems, L("TransferBudgetItems"));
            transferBudgetItems.CreateChildPermission(AppPermissions.Pages_TransferBudgetItems_Create, L("CreateNewTransferBudgetItem"));
            transferBudgetItems.CreateChildPermission(AppPermissions.Pages_TransferBudgetItems_Edit, L("EditTransferBudgetItem"));
            transferBudgetItems.CreateChildPermission(AppPermissions.Pages_TransferBudgetItems_Delete, L("DeleteTransferBudgetItem"));

            var transferBudgets = pages.CreateChildPermission(AppPermissions.Pages_TransferBudgets, L("TransferBudgets"));
            transferBudgets.CreateChildPermission(AppPermissions.Pages_TransferBudgets_Create, L("CreateNewTransferBudget"));
            transferBudgets.CreateChildPermission(AppPermissions.Pages_TransferBudgets_Edit, L("EditTransferBudget"));
            transferBudgets.CreateChildPermission(AppPermissions.Pages_TransferBudgets_Delete, L("DeleteTransferBudget"));

            var ekpOs = pages.CreateChildPermission(AppPermissions.Pages_EKPOs, L("EKPOs"));
            ekpOs.CreateChildPermission(AppPermissions.Pages_EKPOs_Create, L("CreateNewEKPO"));
            ekpOs.CreateChildPermission(AppPermissions.Pages_EKPOs_Edit, L("EditEKPO"));
            ekpOs.CreateChildPermission(AppPermissions.Pages_EKPOs_Delete, L("DeleteEKPO"));

            var ekkos = pages.CreateChildPermission(AppPermissions.Pages_Ekkos, L("Ekkos"));
            ekkos.CreateChildPermission(AppPermissions.Pages_Ekkos_Create, L("CreateNewEkko"));
            ekkos.CreateChildPermission(AppPermissions.Pages_Ekkos_Edit, L("EditEkko"));
            ekkos.CreateChildPermission(AppPermissions.Pages_Ekkos_Delete, L("DeleteEkko"));

            var jobSynchronizes = pages.CreateChildPermission(AppPermissions.Pages_JobSynchronizes, L("JobSynchronizes"));
            jobSynchronizes.CreateChildPermission(AppPermissions.Pages_JobSynchronizes_Create, L("CreateNewJobSynchronize"));
            jobSynchronizes.CreateChildPermission(AppPermissions.Pages_JobSynchronizes_Edit, L("EditJobSynchronize"));
            jobSynchronizes.CreateChildPermission(AppPermissions.Pages_JobSynchronizes_Delete, L("DeleteJobSynchronize"));

            var costCenters = pages.CreateChildPermission(AppPermissions.Pages_CostCenters, L("CostCenters"));
            costCenters.CreateChildPermission(AppPermissions.Pages_CostCenters_Create, L("CreateNewCostCenter"));
            costCenters.CreateChildPermission(AppPermissions.Pages_CostCenters_Edit, L("EditCostCenter"));
            costCenters.CreateChildPermission(AppPermissions.Pages_CostCenters_Delete, L("DeleteCostCenter"));

            var dataProductions = pages.CreateChildPermission(AppPermissions.Pages_DataProductions, L("DataProductions"));
            dataProductions.CreateChildPermission(AppPermissions.Pages_DataProductions_Create, L("CreateNewDataProduction"));
            dataProductions.CreateChildPermission(AppPermissions.Pages_DataProductions_Edit, L("EditDataProduction"));
            dataProductions.CreateChildPermission(AppPermissions.Pages_DataProductions_Delete, L("DeleteDataProduction"));

            var materialRequests = pages.CreateChildPermission(AppPermissions.Pages_MaterialRequests, L("MaterialRequests"));
            materialRequests.CreateChildPermission(AppPermissions.Pages_MaterialRequests_Create, L("CreateNewMaterialRequest"));
            materialRequests.CreateChildPermission(AppPermissions.Pages_MaterialRequests_Edit, L("EditMaterialRequest"));
            materialRequests.CreateChildPermission(AppPermissions.Pages_MaterialRequests_Delete, L("DeleteMaterialRequest"));

            var enumTables = pages.CreateChildPermission(AppPermissions.Pages_EnumTables, L("EnumTables"));
            enumTables.CreateChildPermission(AppPermissions.Pages_EnumTables_Create, L("CreateNewEnumTable"));
            enumTables.CreateChildPermission(AppPermissions.Pages_EnumTables_Edit, L("EditEnumTable"));
            enumTables.CreateChildPermission(AppPermissions.Pages_EnumTables_Delete, L("DeleteEnumTable"));

            var materials = pages.CreateChildPermission(AppPermissions.Pages_Materials, L("Materials"));
            materials.CreateChildPermission(AppPermissions.Pages_Materials_Create, L("CreateNewMaterial"));
            materials.CreateChildPermission(AppPermissions.Pages_Materials_Edit, L("EditMaterial"));
            materials.CreateChildPermission(AppPermissions.Pages_Materials_Delete, L("DeleteMaterial"));

            var generalLedgerMappings = pages.CreateChildPermission(AppPermissions.Pages_GeneralLedgerMappings, L("GeneralLedgerMappings"));
            generalLedgerMappings.CreateChildPermission(AppPermissions.Pages_GeneralLedgerMappings_Create, L("CreateNewGeneralLedgerMapping"));
            generalLedgerMappings.CreateChildPermission(AppPermissions.Pages_GeneralLedgerMappings_Edit, L("EditGeneralLedgerMapping"));
            generalLedgerMappings.CreateChildPermission(AppPermissions.Pages_GeneralLedgerMappings_Delete, L("DeleteGeneralLedgerMapping"));

            var materialGroups = pages.CreateChildPermission(AppPermissions.Pages_MaterialGroups, L("MaterialGroups"));
            materialGroups.CreateChildPermission(AppPermissions.Pages_MaterialGroups_Create, L("CreateNewMaterialGroup"));
            materialGroups.CreateChildPermission(AppPermissions.Pages_MaterialGroups_Edit, L("EditMaterialGroup"));
            materialGroups.CreateChildPermission(AppPermissions.Pages_MaterialGroups_Delete, L("DeleteMaterialGroup"));

            var unspsCs = pages.CreateChildPermission(AppPermissions.Pages_UNSPSCs, L("UNSPSCs"));
            unspsCs.CreateChildPermission(AppPermissions.Pages_UNSPSCs_Create, L("CreateNewUNSPSC"));
            unspsCs.CreateChildPermission(AppPermissions.Pages_UNSPSCs_Edit, L("EditUNSPSC"));
            unspsCs.CreateChildPermission(AppPermissions.Pages_UNSPSCs_Delete, L("DeleteUNSPSC"));

            var travelRequests = pages.CreateChildPermission(AppPermissions.Pages_TravelRequests, L("TravelRequests"));
            travelRequests.CreateChildPermission(AppPermissions.Pages_TravelRequests_Create, L("CreateNewTravelRequest"));
            travelRequests.CreateChildPermission(AppPermissions.Pages_TravelRequests_Edit, L("EditTravelRequest"));
            travelRequests.CreateChildPermission(AppPermissions.Pages_TravelRequests_Delete, L("DeleteTravelRequest"));

            var airports = pages.CreateChildPermission(AppPermissions.Pages_Airports, L("Airports"));
            airports.CreateChildPermission(AppPermissions.Pages_Airports_Create, L("CreateNewAirport"));
            airports.CreateChildPermission(AppPermissions.Pages_Airports_Edit, L("EditAirport"));
            airports.CreateChildPermission(AppPermissions.Pages_Airports_Delete, L("DeleteAirport"));

            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangeProfilePicture, L("UpdateUsersProfilePicture"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeDefaultLanguage, L("ChangeDefaultLanguage"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            var webhooks = administration.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription, L("Webhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Create, L("CreatingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Edit, L("EditingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_ChangeActivity, L("ChangingWebhookActivity"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Detail, L("DetailingSubscription"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ListSendAttempts, L("ListingSendAttempts"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ResendWebhook, L("ResendingWebhook"));

            var dynamicProperties = administration.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties, L("DynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Create, L("CreatingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Edit, L("EditingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Delete, L("DeletingDynamicProperties"));

            var dynamicPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue, L("DynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Create, L("CreatingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Edit, L("EditingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Delete, L("DeletingDynamicPropertyValue"));

            var dynamicEntityProperties = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties, L("DynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Create, L("CreatingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Edit, L("EditingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Delete, L("DeletingDynamicEntityProperties"));

            var dynamicEntityPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue, L("EntityDynamicPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Create, L("CreatingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Edit, L("EditingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Delete, L("DeletingDynamicEntityPropertyValue"));

            var massNotification = administration.CreateChildPermission(AppPermissions.Pages_Administration_MassNotification, L("MassNotifications"));
            massNotification.CreateChildPermission(AppPermissions.Pages_Administration_MassNotification_Create, L("MassNotificationCreate"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);

            var maintenance = administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            maintenance.CreateChildPermission(AppPermissions.Pages_Administration_NewVersion_Create, L("SendNewVersionNotification"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CentralizeConsts.LocalizationSourceName);
        }
    }
}