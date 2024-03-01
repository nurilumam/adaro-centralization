import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/common/session/app-session.service';

import { Injectable } from '@angular/core';
import { AppMenu } from './app-menu';
import { AppMenuItem } from './app-menu-item';

@Injectable()
export class AppNavigationService {
    constructor(
        private _permissionCheckerService: PermissionCheckerService,
        private _appSessionService: AppSessionService
    ) { }

    getMenu(): AppMenu {
        return new AppMenu('MainMenu', 'MainMenu', [
            new AppMenuItem(
                'Dashboard',
                'Pages.Administration.Host.Dashboard',
                'bi bi-clipboard-data',
                '/app/admin/hostDashboard'
            ),
            new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'bi bi-clipboard-data', '/app/main/dashboard'),
            new AppMenuItem('Tenants', 'Pages.Tenants', 'bi bi-list-check', '/app/admin/tenants'),          
           
 

            new AppMenuItem('TransferBudgets', 'Pages.TransferBudgets', 'bi bi-file-earmark-spreadsheet', '/app/main/finance/transferBudgets'),
            // new AppMenuItem('TransferBudgetItems', 'Pages.TransferBudgetItems', 'flaticon-more', '/app/main/finance/transferBudgetItems'),
            
            new AppMenuItem(
                'SAP Data Synchronization ',
                '',
                'bi bi-hdd-rack',
                '',
                [],
                [
                    new AppMenuItem('CostCenters', 'Pages.CostCenters', 'flaticon-more', '/app/main/sapConnector/costCenters'),
                    new AppMenuItem('PurchasingHeader', 'Pages.Ekkos', 'flaticon-more', '/app/main/sapConnector/ekkos'),            
                    new AppMenuItem('PurchasingItem', 'Pages.EKPOs', 'flaticon-more', '/app/main/sapConnector/ekpOs'),            
                    new AppMenuItem('DataProductions', 'Pages.DataProductions', 'flaticon-more', '/app/main/sapConnector/dataProductions'),
                    new AppMenuItem('JobSynchronizes', 'Pages.JobSynchronizes', 'flaticon-more', '/app/main/jobScheduler/jobSynchronizes'),                  
                ]
            ),

            new AppMenuItem(
                'Master Data Management',
                '',
                'bi bi-list-ul',
                '',
                [],
                [
                    new AppMenuItem('Materials', 'Pages.Materials', 'flaticon-more', '/app/main/masterData/materials'),
                    new AppMenuItem('Data Setup',
                    '',
                    'flaticon-interface-8',
                    '',
                    [],
                    [

                        new AppMenuItem('UNSPSCs', 'Pages.UNSPSCs', 'flaticon-more', '/app/main/masterData/unspsCs'),
                        new AppMenuItem('EnumTables', 'Pages.EnumTables', 'flaticon-more', '/app/main/masterData/enumTables'),            
                        new AppMenuItem('GeneralLedgerMappings', 'Pages.GeneralLedgerMappings', 'flaticon-more', '/app/main/masterData/generalLedgerMappings'),                    
                        new AppMenuItem('MaterialGroups', 'Pages.MaterialGroups', 'flaticon-more', '/app/main/masterData/materialGroups'),
                        new AppMenuItem('MaterialRequests', 'Pages.MaterialRequests', 'flaticon-more', '/app/main/masterDataRequest/materialRequests'),
    
                    ]
                    ),
                    

                ]
            ),


            new AppMenuItem(
                'Administration',
                '',
                'flaticon-interface-8',
                '',
                [],
                [
                    new AppMenuItem(
                        'OrganizationUnits',
                        'Pages.Administration.OrganizationUnits',
                        'flaticon-map',
                        '/app/admin/organization-units'
                    ),
                    new AppMenuItem('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
                    new AppMenuItem('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
                    new AppMenuItem(
                        'Languages',
                        'Pages.Administration.Languages',
                        'flaticon-tabs',
                        '/app/admin/languages',
                        ['/app/admin/languages/{name}/texts']
                    ),
                   
            new AppMenuItem('LookupPages', 'Pages.LookupPages', 'flaticon-more', '/app/main/lookupArea/lookupPages'),
             new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),
                    new AppMenuItem(
                        'AuditLogs',
                        'Pages.Administration.AuditLogs',
                        'flaticon-folder-1',
                        '/app/admin/auditLogs'
                    ),
                    new AppMenuItem(
                        'Maintenance',
                        'Pages.Administration.Host.Maintenance',
                        'flaticon-lock',
                        '/app/admin/maintenance'
                    ),
                    new AppMenuItem(
                        'Subscription',
                        'Pages.Administration.Tenant.SubscriptionManagement',
                        'flaticon-refresh',
                        '/app/admin/subscription-management'
                    ),
                    new AppMenuItem(
                        'VisualSettings',
                        'Pages.Administration.UiCustomization',
                        'flaticon-medical',
                        '/app/admin/ui-customization'
                    ),
                    new AppMenuItem(
                        'WebhookSubscriptions',
                        'Pages.Administration.WebhookSubscription',
                        'flaticon2-world',
                        '/app/admin/webhook-subscriptions'
                    ),
                    new AppMenuItem(
                        'DynamicProperties',
                        'Pages.Administration.DynamicProperties',
                        'flaticon-interface-8',
                        '/app/admin/dynamic-property'
                    ),
                    new AppMenuItem(
                        'Settings',
                        'Pages.Administration.Host.Settings',
                        'flaticon-settings',
                        '/app/admin/hostSettings'
                    ),
                    new AppMenuItem(
                        'Settings',
                        'Pages.Administration.Tenant.Settings',
                        'flaticon-settings',
                        '/app/admin/tenantSettings'
                    ),
                    new AppMenuItem(
                        'Notifications',
                        '',
                        'flaticon-alarm',
                        '',
                        [],
                        [
                            new AppMenuItem(
                                'Inbox',
                                '',
                                'flaticon-mail-1',
                                '/app/notifications'
                            ),
                            new AppMenuItem(
                                'MassNotifications',
                                'Pages.Administration.MassNotification',
                                'flaticon-paper-plane',
                                '/app/admin/mass-notifications'
                            )
                        ]
                    )
                ]
            ),
            // new AppMenuItem(
            //     'DemoUiComponents',
            //     'Pages.DemoUiComponents',
            //     'flaticon-shapes',
            //     '/app/admin/demo-ui-components'
            // ),
        ]);
    }

    checkChildMenuItemPermission(menuItem): boolean {
        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];

            if (subMenuItem.permissionName === '' || subMenuItem.permissionName === null) {
                if (subMenuItem.route) {
                    return true;
                }
            } else if (this._permissionCheckerService.isGranted(subMenuItem.permissionName)) {
                if (!subMenuItem.hasFeatureDependency()) {
                    return true;
                }
                
                if (subMenuItem.featureDependencySatisfied()) {
                    return true;
                }
            }

            if (subMenuItem.items && subMenuItem.items.length) {
                let isAnyChildItemActive = this.checkChildMenuItemPermission(subMenuItem);
                if (isAnyChildItemActive) {
                    return true;
                }
            }
        }

        return false;
    }

    showMenuItem(menuItem: AppMenuItem): boolean {
        if (
            menuItem.permissionName === 'Pages.Administration.Tenant.SubscriptionManagement' &&
            this._appSessionService.tenant &&
            !this._appSessionService.tenant.edition
        ) {
            return false;
        }

        let hideMenuItem = false;

        if (menuItem.requiresAuthentication && !this._appSessionService.user) {
            hideMenuItem = true;
        }

        if (menuItem.permissionName && !this._permissionCheckerService.isGranted(menuItem.permissionName)) {
            hideMenuItem = true;
        }

        if (this._appSessionService.tenant || !abp.multiTenancy.ignoreFeatureCheckForHostUsers) {
            if (menuItem.hasFeatureDependency() && !menuItem.featureDependencySatisfied()) {
                hideMenuItem = true;
            }
        }

        if (!hideMenuItem && menuItem.items && menuItem.items.length) {
            return this.checkChildMenuItemPermission(menuItem);
        }

        return !hideMenuItem;
    }

    /**
     * Returns all menu items recursively
     */
    getAllMenuItems(): AppMenuItem[] {
        let menu = this.getMenu();
        let allMenuItems: AppMenuItem[] = [];
        menu.items.forEach((menuItem) => {
            allMenuItems = allMenuItems.concat(this.getAllMenuItemsRecursive(menuItem));
        });

        return allMenuItems;
    }

    private getAllMenuItemsRecursive(menuItem: AppMenuItem): AppMenuItem[] {
        if (!menuItem.items) {
            return [menuItem];
        }

        let menuItems = [menuItem];
        menuItem.items.forEach((subMenu) => {
            menuItems = menuItems.concat(this.getAllMenuItemsRecursive(subMenu));
        });

        return menuItems;
    }
}
