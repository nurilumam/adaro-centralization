using Microsoft.AspNetCore.Components;
using Adaro.Centralize.Authorization.Accounts;
using Adaro.Centralize.Authorization.Accounts.Dto;
using Adaro.Centralize.Core.Dependency;
using Adaro.Centralize.Core.Threading;
using Adaro.Centralize.Mobile.MAUI.Models.Login;
using Adaro.Centralize.Mobile.MAUI.Shared;

namespace Adaro.Centralize.Mobile.MAUI.Pages.Login
{
    public partial class EmailActivationModal : ModalBase
    {
        public override string ModalId => "email-activation-modal";

        [Parameter] public EventCallback OnSave { get; set; }

        public EmailActivationModel emailActivationModel { get; set; } = new EmailActivationModel();

        private readonly IAccountAppService _accountAppService;

        public EmailActivationModal()
        {
            _accountAppService = DependencyResolver.Resolve<IAccountAppService>();
        }

        protected virtual async Task Save()
        {
            await SetBusyAsync(async () =>
            {
                await WebRequestExecuter.Execute(
                async () =>
                    await _accountAppService.SendEmailActivationLink(new SendEmailActivationLinkInput
                    {
                        EmailAddress = emailActivationModel.EmailAddress
                    }),
                    async () =>
                    {
                        await OnSave.InvokeAsync();
                    }
                );
            });
        }

        protected virtual async Task Cancel()
        {
            await Hide();
        }
    }
}
