using System;
using System.Collections.Generic;
using System.Text;

namespace AdaroConnect.Application.Core.Models
{
    public class SAPGeneralParameterModel
    {
        public string CompanyCode { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public bool IsActive { get; set; }
    }
}
