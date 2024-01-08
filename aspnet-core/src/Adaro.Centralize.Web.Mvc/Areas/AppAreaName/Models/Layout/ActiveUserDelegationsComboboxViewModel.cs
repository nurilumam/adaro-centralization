using System.Collections.Generic;
using Adaro.Centralize.Authorization.Delegation;
using Adaro.Centralize.Authorization.Users.Delegation.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Layout
{
    public class ActiveUserDelegationsComboboxViewModel
    {
        public IUserDelegationConfiguration UserDelegationConfiguration { get; set; }

        public List<UserDelegationDto> UserDelegations { get; set; }

        public string CssClass { get; set; }
    }
}
