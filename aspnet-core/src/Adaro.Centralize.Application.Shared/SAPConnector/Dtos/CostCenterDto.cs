using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class CostCenterDto : EntityDto<Guid>
    {
        public string ControllingArea { get; set; }

        public string CostCenterName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string CostCenterCode { get; set; }

        public string DepartmentName { get; set; }

        public string Period { get; set; }

    }
}