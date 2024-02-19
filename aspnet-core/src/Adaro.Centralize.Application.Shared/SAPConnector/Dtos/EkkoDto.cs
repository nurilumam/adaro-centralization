using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class EkkoDto : EntityDto<Guid>
    {
        public string MANDT { get; set; }

        public string EBELN { get; set; }

        public string BUKRS { get; set; }

        public string BSTYP { get; set; }

        public DateTime? AEDAT { get; set; }

        public decimal? ZBD1T { get; set; }

        public decimal? ZBD2T { get; set; }

        public decimal? ZBD3T { get; set; }

        public string EKGRP { get; set; }

        public decimal? WKURS { get; set; }

        public string KUFIX { get; set; }

        public DateTime? BEDAT { get; set; }

        public DateTime? KDATB { get; set; }

        public DateTime? KDATE { get; set; }

        public DateTime? BWBDT { get; set; }

        public DateTime? GWLDT { get; set; }

        public DateTime? IHRAN { get; set; }

        public string KUNNR { get; set; }

        public string KONNR { get; set; }

        public string ABGRU { get; set; }

        public string AUTLF { get; set; }

        public string WEAKT { get; set; }

        public string RESWK { get; set; }

        public string LBLIF { get; set; }

        public string INCO1 { get; set; }

        public string INCO2 { get; set; }

        public string SUBMI { get; set; }

        public string KNUMV { get; set; }

    }
}