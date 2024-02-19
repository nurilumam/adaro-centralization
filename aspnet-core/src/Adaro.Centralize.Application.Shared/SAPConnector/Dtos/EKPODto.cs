using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class EKPODto : EntityDto<Guid>
    {
        public string MANDT { get; set; }

        public string EBELN { get; set; }

        public long? EBELP { get; set; }

        public string UNIQUEID { get; set; }

        public string LOEKZ { get; set; }

        public DateTime? AEDAT { get; set; }

        public string TXZ01 { get; set; }

        public string MATNR { get; set; }

        public string BUKRS { get; set; }

        public string BEDNR { get; set; }

        public string MATKL { get; set; }

        public string INFNR { get; set; }

        public string IDNLF { get; set; }

        public long? KTMNG { get; set; }

        public long? MENGE { get; set; }

        public string MEINS { get; set; }

        public string BPRME { get; set; }

        public decimal? BPUMZ { get; set; }

        public decimal? BPUMN { get; set; }

        public decimal? UMREZ { get; set; }

        public decimal? UMREN { get; set; }

        public decimal? NETPR { get; set; }

        public decimal? PEINH { get; set; }

        public decimal? NETWR { get; set; }

        public decimal? BRTWR { get; set; }

        public DateTime? AGDAT { get; set; }

        public decimal? WEBAZ { get; set; }

        public string MWSKZ { get; set; }

        public string BONUS { get; set; }

        public string INSMK { get; set; }

        public string SPINF { get; set; }

        public string PRSDR { get; set; }

        public string BWTAR { get; set; }

        public string BWTTY { get; set; }

        public string ABSKZ { get; set; }

        public string PSTYP { get; set; }

        public string KNTTP { get; set; }

        public string KONNR { get; set; }

        public long? KTPNR { get; set; }

        public decimal? PACKNO { get; set; }

        public string ANFNR { get; set; }

        public string BANFN { get; set; }

        public long? BNFPO { get; set; }

    }
}