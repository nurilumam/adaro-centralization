using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetAllEkkosInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MANDTFilter { get; set; }

        public string EBELNFilter { get; set; }

        public string BUKRSFilter { get; set; }

        public string BSTYPFilter { get; set; }

        public string BSARTFilter { get; set; }

        public string BSAKZFilter { get; set; }

        public string LOEKZFilter { get; set; }

        public string STATUFilter { get; set; }

        public DateTime? MaxAEDATFilter { get; set; }
        public DateTime? MinAEDATFilter { get; set; }

        public string ERNAMFilter { get; set; }

        public long? MaxPINCRFilter { get; set; }
        public long? MinPINCRFilter { get; set; }

        public long? MaxLPONRFilter { get; set; }
        public long? MinLPONRFilter { get; set; }

        public string LIFNRFilter { get; set; }

        public string ZTERMFilter { get; set; }

        public decimal? MaxZBD1TFilter { get; set; }
        public decimal? MinZBD1TFilter { get; set; }

        public decimal? MaxZBD2TFilter { get; set; }
        public decimal? MinZBD2TFilter { get; set; }

        public decimal? MaxZBD3TFilter { get; set; }
        public decimal? MinZBD3TFilter { get; set; }

        public decimal? MaxZBD1PFilter { get; set; }
        public decimal? MinZBD1PFilter { get; set; }

        public decimal? MaxZBD2PFilter { get; set; }
        public decimal? MinZBD2PFilter { get; set; }

        public string EKORGFilter { get; set; }

        public string EKGRPFilter { get; set; }

        public string WAERSFilter { get; set; }

        public decimal? MaxWKURSFilter { get; set; }
        public decimal? MinWKURSFilter { get; set; }

        public string KUFIXFilter { get; set; }

        public DateTime? MaxBEDATFilter { get; set; }
        public DateTime? MinBEDATFilter { get; set; }

        public DateTime? MaxKDATBFilter { get; set; }
        public DateTime? MinKDATBFilter { get; set; }

        public DateTime? MaxKDATEFilter { get; set; }
        public DateTime? MinKDATEFilter { get; set; }

        public DateTime? MaxBWBDTFilter { get; set; }
        public DateTime? MinBWBDTFilter { get; set; }

        public DateTime? MaxGWLDTFilter { get; set; }
        public DateTime? MinGWLDTFilter { get; set; }

        public DateTime? MaxIHRANFilter { get; set; }
        public DateTime? MinIHRANFilter { get; set; }

        public string KUNNRFilter { get; set; }

        public string KONNRFilter { get; set; }

        public string ABGRUFilter { get; set; }

        public string AUTLFFilter { get; set; }

        public string WEAKTFilter { get; set; }

        public string RESWKFilter { get; set; }

        public string LBLIFFilter { get; set; }

        public string INCO1Filter { get; set; }

        public string INCO2Filter { get; set; }

        public string SUBMIFilter { get; set; }

        public string KNUMVFilter { get; set; }

        public string KALSMFilter { get; set; }

        public string PROCSTATFilter { get; set; }

        public string UNSEZFilter { get; set; }

        public string FRGGRFilter { get; set; }

        public string FRGSXFilter { get; set; }

        public string FRGKEFilter { get; set; }

        public string FRGZUFilter { get; set; }

        public string ADRNRFilter { get; set; }

    }
}