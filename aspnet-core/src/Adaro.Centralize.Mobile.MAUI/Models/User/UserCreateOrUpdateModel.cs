using Abp.AutoMapper;
using Adaro.Centralize.Authorization.Users.Dto;

namespace Adaro.Centralize.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(CreateOrUpdateUserInput))]
    public class UserCreateOrUpdateModel : CreateOrUpdateUserInput
    {

    }
}
