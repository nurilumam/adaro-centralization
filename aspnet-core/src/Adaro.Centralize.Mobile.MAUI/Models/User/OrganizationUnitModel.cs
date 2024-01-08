using Abp.AutoMapper;
using Adaro.Centralize.Organizations.Dto;

namespace Adaro.Centralize.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}
