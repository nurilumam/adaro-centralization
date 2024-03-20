using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.ReportArea.Dtos
{
    public class GetAllRptProcurementAdjustsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string PurchasingDocumentFilter { get; set; }

        public int? IsContractFilter { get; set; }

        public int? IsAdjustFilter { get; set; }

        public int? MaxDayAdjustFilter { get; set; }
        public int? MinDayAdjustFilter { get; set; }

        public string RemarkFilter { get; set; }

    }
}