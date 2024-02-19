using Abp.Application.Services.Dto;
using System;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetAllEKPOsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MANDTFilter { get; set; }

        public string EBELNFilter { get; set; }

        public long? MaxEBELPFilter { get; set; }
        public long? MinEBELPFilter { get; set; }

        public string UNIQUEIDFilter { get; set; }

        public string LOEKZFilter { get; set; }

        public string STATUFilter { get; set; }

        public DateTime? MaxAEDATFilter { get; set; }
        public DateTime? MinAEDATFilter { get; set; }

        public string TXZ01Filter { get; set; }

        public string MATNRFilter { get; set; }

        public string EMATNFilter { get; set; }

        public string BUKRSFilter { get; set; }

        public string WERKSFilter { get; set; }

        public string LGORTFilter { get; set; }

        public string BEDNRFilter { get; set; }

        public string MATKLFilter { get; set; }

        public string INFNRFilter { get; set; }

        public string IDNLFFilter { get; set; }

        public long? MaxKTMNGFilter { get; set; }
        public long? MinKTMNGFilter { get; set; }

        public long? MaxMENGEFilter { get; set; }
        public long? MinMENGEFilter { get; set; }

        public string MEINSFilter { get; set; }

        public string BPRMEFilter { get; set; }

        public decimal? MaxBPUMZFilter { get; set; }
        public decimal? MinBPUMZFilter { get; set; }

        public decimal? MaxBPUMNFilter { get; set; }
        public decimal? MinBPUMNFilter { get; set; }

        public decimal? MaxUMREZFilter { get; set; }
        public decimal? MinUMREZFilter { get; set; }

        public decimal? MaxUMRENFilter { get; set; }
        public decimal? MinUMRENFilter { get; set; }

        public decimal? MaxNETPRFilter { get; set; }
        public decimal? MinNETPRFilter { get; set; }

        public decimal? MaxPEINHFilter { get; set; }
        public decimal? MinPEINHFilter { get; set; }

        public decimal? MaxNETWRFilter { get; set; }
        public decimal? MinNETWRFilter { get; set; }

        public decimal? MaxBRTWRFilter { get; set; }
        public decimal? MinBRTWRFilter { get; set; }

        public DateTime? MaxAGDATFilter { get; set; }
        public DateTime? MinAGDATFilter { get; set; }

        public decimal? MaxWEBAZFilter { get; set; }
        public decimal? MinWEBAZFilter { get; set; }

        public string MWSKZFilter { get; set; }

        public string BONUSFilter { get; set; }

        public string INSMKFilter { get; set; }

        public string SPINFFilter { get; set; }

        public string PRSDRFilter { get; set; }

        public string BWTARFilter { get; set; }

        public string BWTTYFilter { get; set; }

        public string ABSKZFilter { get; set; }

        public string PSTYPFilter { get; set; }

        public string KNTTPFilter { get; set; }

        public string KONNRFilter { get; set; }

        public long? MaxKTPNRFilter { get; set; }
        public long? MinKTPNRFilter { get; set; }

        public decimal? MaxPACKNOFilter { get; set; }
        public decimal? MinPACKNOFilter { get; set; }

        public string ANFNRFilter { get; set; }

        public string BANFNFilter { get; set; }

        public long? MaxBNFPOFilter { get; set; }
        public long? MinBNFPOFilter { get; set; }

    }
}