﻿using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using Adaro.Centralize.Authorization;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Startup
{
    public class AppAreaNameNavigationProvider : NavigationProvider
    {
        public const string MenuName = "App";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Host.Dashboard,
                        L("Dashboard"),
                        url: "AppAreaName/HostDashboard",
                        icon: "flaticon-line-graph",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_Host_Dashboard)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Common.MaterialRequests,
                        L("MaterialRequests"),
                        url: "AppAreaName/MaterialRequests",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_MaterialRequests)
                    )
                )
                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Common.EnumTables,
                        L("EnumTables"),
                        url: "AppAreaName/EnumTables",
                        icon: "flaticon-more",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_EnumTables)
                    )
                )

                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.MasterDataManagement.MasterData,
                        L("MasterData"),
                        icon: "las la-boxes fs-2"
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.MasterDataManagement.Materials,
                            L("Materials"),
                            url: "AppAreaName/Materials",
                            icon: "las la-box-open fs-2",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Materials)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.MasterDataManagement.GeneralLedgerMappings,
                            L("GeneralLedgerMappings"),
                            url: "AppAreaName/GeneralLedgerMappings",
                            icon: "las la-network-wired fs-2",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_GeneralLedgerMappings)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.MasterDataManagement.MaterialGroups,
                            L("MaterialGroups"),
                            url: "AppAreaName/MaterialGroups",
                            icon: "las la-layer-group fs-2",
                            permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_MaterialGroups)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.MasterDataManagement.UNSPSCs,
                        L("UNSPSCs"),
                        url: "AppAreaName/UNSPSCs",
                        icon: "las la-code fs-2",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_UNSPSCs)
                        )
                    )
                )

                //.AddItem(new MenuItemDefinition(
                //        AppAreaNamePageNames.Common.TravelRequests,
                //        L("TravelRequests"),
                //        url: "AppAreaName/TravelRequests",
                //        icon: "flaticon-more",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_TravelRequests)
                //    )
                //)
                //.AddItem(new MenuItemDefinition(
                //        AppAreaNamePageNames.Common.Airports,
                //        L("Airports"),
                //        url: "AppAreaName/Airports",
                //        icon: "flaticon-more",
                //        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Airports)
                //    )
                //)

                .AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Host.Tenants,
                        L("Tenants"),
                        url: "AppAreaName/Tenants",
                        icon: "flaticon-list-3",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenants)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Host.Editions,
                        L("Editions"),
                        url: "AppAreaName/Editions",
                        icon: "flaticon-app",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Editions)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Tenant.Dashboard,
                        L("Dashboard"),
                        url: "AppAreaName/TenantDashboard",
                        icon: "flaticon-line-graph",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Tenant_Dashboard)
                    )
                ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Common.Administration,
                        L("Administration"),
                        icon: "flaticon-interface-8"
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.OrganizationUnits,
                            L("OrganizationUnits"),
                            url: "AppAreaName/OrganizationUnits",
                            icon: "flaticon-map",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_OrganizationUnits)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.Roles,
                            L("Roles"),
                            url: "AppAreaName/Roles",
                            icon: "flaticon-suitcase",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Roles)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.Users,
                            L("Users"),
                            url: "AppAreaName/Users",
                            icon: "flaticon-users",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Users)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.Languages,
                            L("Languages"),
                            url: "AppAreaName/Languages",
                            icon: "flaticon-tabs",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Languages)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.AuditLogs,
                            L("AuditLogs"),
                            url: "AppAreaName/AuditLogs",
                            icon: "flaticon-folder-1",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_AuditLogs)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Host.Maintenance,
                            L("Maintenance"),
                            url: "AppAreaName/Maintenance",
                            icon: "flaticon-lock",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Host_Maintenance)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.SubscriptionManagement,
                            L("Subscription"),
                            url: "AppAreaName/SubscriptionManagement",
                            icon: "flaticon-refresh",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Tenant_SubscriptionManagement)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.UiCustomization,
                            L("VisualSettings"),
                            url: "AppAreaName/UiCustomization",
                            icon: "flaticon-medical",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_UiCustomization)
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.WebhookSubscriptions,
                            L("WebhookSubscriptions"),
                            url: "AppAreaName/WebhookSubscription",
                            icon: "flaticon2-world",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_WebhookSubscription)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.DynamicProperties,
                            L("DynamicProperties"),
                            url: "AppAreaName/DynamicProperty",
                            icon: "flaticon-interface-8",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_DynamicProperties)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Host.Settings,
                            L("Settings"),
                            url: "AppAreaName/HostSettings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Host_Settings)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Tenant.Settings,
                            L("Settings"),
                            url: "AppAreaName/Settings",
                            icon: "flaticon-settings",
                            permissionDependency: new SimplePermissionDependency(AppPermissions
                                .Pages_Administration_Tenant_Settings)
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppAreaNamePageNames.Common.Notifications,
                            L("Notifications"),
                            icon: "flaticon-alarm"
                        ).AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Common.Notifications_Inbox,
                                L("Inbox"),
                                url: "AppAreaName/Notifications",
                                icon: "flaticon-mail-1"
                            )
                        )
                        .AddItem(new MenuItemDefinition(
                                AppAreaNamePageNames.Common.Notifications_MassNotifications,
                                L("MassNotifications"),
                                url: "AppAreaName/Notifications/MassNotifications",
                                icon: "flaticon-paper-plane",
                                permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_Administration_MassNotification)
                            )
                        )
                    )
                ).AddItem(new MenuItemDefinition(
                        AppAreaNamePageNames.Common.DemoUiComponents,
                        L("DemoUiComponents"),
                        url: "AppAreaName/DemoUiComponents",
                        icon: "flaticon-shapes",
                        permissionDependency: new SimplePermissionDependency(AppPermissions.Pages_DemoUiComponents)
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CentralizeConsts.LocalizationSourceName);
        }
    }
}