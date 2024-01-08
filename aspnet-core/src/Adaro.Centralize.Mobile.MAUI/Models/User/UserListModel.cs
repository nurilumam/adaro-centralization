using Abp.AutoMapper;
using Adaro.Centralize.Authorization.Users.Dto;

namespace Adaro.Centralize.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(UserListDto))]
    public class UserListModel : UserListDto
    {
        public string Photo { get; set; }

        public string FullName => Name + " " + Surname;
    }
}
