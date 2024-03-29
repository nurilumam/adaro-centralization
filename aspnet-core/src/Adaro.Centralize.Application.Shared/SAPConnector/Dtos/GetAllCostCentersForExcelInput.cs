﻿using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetAllCostCentersForExcelInput
    {
        public string Filter { get; set; }

        public string ControllingAreaFilter { get; set; }

        public string CostCenterNameFilter { get; set; }

        public string DescriptionFilter { get; set; }

        public string ActStateFilter { get; set; }

        public int? IsActiveFilter { get; set; }

        public string CostCenterCodeFilter { get; set; }

        public string CostCenterShortFilter { get; set; }

        public string DepartmentNameFilter { get; set; }

        public string PeriodFilter { get; set; }

    }
}