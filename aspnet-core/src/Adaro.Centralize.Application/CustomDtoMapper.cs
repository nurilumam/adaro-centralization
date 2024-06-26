﻿using Adaro.Centralize.ReportArea.Dtos;
using Adaro.Centralize.ReportArea;
using Adaro.Centralize.LookupArea.Dtos;
using Adaro.Centralize.LookupArea;
using Adaro.Centralize.Finance.Dtos;
using Adaro.Centralize.Finance;
using Adaro.Centralize.JobScheduler.Dtos;
using Adaro.Centralize.JobScheduler;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.SAPConnector;
using Adaro.Centralize.MasterDataRequest.Dtos;
using Adaro.Centralize.MasterDataRequest;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.MasterData;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Extensions;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using Adaro.Centralize.Auditing.Dto;
using Adaro.Centralize.Authorization.Accounts.Dto;
using Adaro.Centralize.Authorization.Delegation;
using Adaro.Centralize.Authorization.Permissions.Dto;
using Adaro.Centralize.Authorization.Roles;
using Adaro.Centralize.Authorization.Roles.Dto;
using Adaro.Centralize.Authorization.Users;
using Adaro.Centralize.Authorization.Users.Delegation.Dto;
using Adaro.Centralize.Authorization.Users.Dto;
using Adaro.Centralize.Authorization.Users.Importing.Dto;
using Adaro.Centralize.Authorization.Users.Profile.Dto;
using Adaro.Centralize.Chat;
using Adaro.Centralize.Chat.Dto;
using Adaro.Centralize.DynamicEntityProperties.Dto;
using Adaro.Centralize.Editions;
using Adaro.Centralize.Editions.Dto;
using Adaro.Centralize.Friendships;
using Adaro.Centralize.Friendships.Cache;
using Adaro.Centralize.Friendships.Dto;
using Adaro.Centralize.Localization.Dto;
using Adaro.Centralize.MultiTenancy;
using Adaro.Centralize.MultiTenancy.Dto;
using Adaro.Centralize.MultiTenancy.HostDashboard.Dto;
using Adaro.Centralize.MultiTenancy.Payments;
using Adaro.Centralize.MultiTenancy.Payments.Dto;
using Adaro.Centralize.Notifications.Dto;
using Adaro.Centralize.Organizations.Dto;
using Adaro.Centralize.Sessions.Dto;
using Adaro.Centralize.WebHooks.Dto;
using AdaroConnect.Application.Core.Models;
using Material = Adaro.Centralize.MasterData.Material;

namespace Adaro.Centralize
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrEditTransferBudgetDetailDto, TransferBudgetDetail>().ReverseMap();
            configuration.CreateMap<TransferBudgetDetailDto, TransferBudgetDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditGeneralLedgerAccountDto, GeneralLedgerAccount>().ReverseMap();
            configuration.CreateMap<GeneralLedgerAccountDto, GeneralLedgerAccount>().ReverseMap();
            configuration.CreateMap<CreateOrEditZMM020RDto, ZMM020R>().ReverseMap();
            configuration.CreateMap<ZMM020RDto, ZMM020R>().ReverseMap();
            configuration.CreateMap<CreateOrEditRptProcurementAdjustDto, RptProcurementAdjust>().ReverseMap();
            configuration.CreateMap<RptProcurementAdjustDto, RptProcurementAdjust>().ReverseMap();
            configuration.CreateMap<CreateOrEditZMM021RDto, ZMM021R>().ReverseMap();
            configuration.CreateMap<ZMM021RDto, ZMM021R>().ReverseMap();
            configuration.CreateMap<CreateOrEditLookupPageDto, LookupPage>().ReverseMap();
            configuration.CreateMap<LookupPageDto, LookupPage>().ReverseMap();
            configuration.CreateMap<CreateOrEditTransferBudgetDto, TransferBudget>().ReverseMap();
            configuration.CreateMap<TransferBudgetDto, TransferBudget>().ReverseMap();
            configuration.CreateMap<CreateOrEditJobSynchronizeDto, JobSynchronize>().ReverseMap();
            configuration.CreateMap<JobSynchronizeDto, JobSynchronize>().ReverseMap();
            configuration.CreateMap<CreateOrEditCostCenterDto, CostCenter>().ReverseMap();
            configuration.CreateMap<CostCenterDto, CostCenter>().ReverseMap();
            configuration.CreateMap<CreateOrEditDataProductionDto, DataProduction>().ReverseMap();
            configuration.CreateMap<DataProductionDto, DataProduction>().ReverseMap();
            configuration.CreateMap<CreateOrEditMaterialRequestDto, MaterialRequest>().ReverseMap();
            configuration.CreateMap<MaterialRequestDto, MaterialRequest>().ReverseMap();
            configuration.CreateMap<CreateOrEditEnumTableDto, EnumTable>().ReverseMap();
            configuration.CreateMap<EnumTableDto, EnumTable>().ReverseMap();
            configuration.CreateMap<CreateOrEditMaterialDto, Material>().ReverseMap();
            configuration.CreateMap<MaterialDto, Material>().ReverseMap();
            configuration.CreateMap<CreateOrEditGeneralLedgerMappingDto, GeneralLedgerMapping>().ReverseMap();
            configuration.CreateMap<GeneralLedgerMappingDto, GeneralLedgerMapping>().ReverseMap();
            configuration.CreateMap<CreateOrEditMaterialGroupDto, MaterialGroup>().ReverseMap();
            configuration.CreateMap<MaterialGroupDto, MaterialGroup>().ReverseMap();
            configuration.CreateMap<CreateOrEditUNSPSCDto, UNSPSC>().ReverseMap();
            configuration.CreateMap<UNSPSCDto, UNSPSC>().ReverseMap();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(dto => dto.DynamicPropertyName,
                    options => options.MapFrom(entity => entity.DynamicProperty.DisplayName.IsNullOrEmpty() ? entity.DynamicProperty.PropertyName : entity.DynamicProperty.DisplayName));
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();

            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
            // SAP SYNCHRONIZE

            configuration.CreateMap<CostCenterItem, CostCenter>().ReverseMap();
        }
    }
}