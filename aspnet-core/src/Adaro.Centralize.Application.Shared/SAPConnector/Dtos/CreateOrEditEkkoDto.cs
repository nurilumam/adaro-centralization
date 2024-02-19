using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class CreateOrEditEkkoDto : EntityDto<Guid?>
    {

        [StringLength(EkkoConsts.MaxMANDTLength, MinimumLength = EkkoConsts.MinMANDTLength)]
        public string MANDT { get; set; }

        [StringLength(EkkoConsts.MaxEBELNLength, MinimumLength = EkkoConsts.MinEBELNLength)]
        public string EBELN { get; set; }

        [StringLength(EkkoConsts.MaxBUKRSLength, MinimumLength = EkkoConsts.MinBUKRSLength)]
        public string BUKRS { get; set; }

        [StringLength(EkkoConsts.MaxBSTYPLength, MinimumLength = EkkoConsts.MinBSTYPLength)]
        public string BSTYP { get; set; }

        [StringLength(EkkoConsts.MaxBSARTLength, MinimumLength = EkkoConsts.MinBSARTLength)]
        public string BSART { get; set; }

        [StringLength(EkkoConsts.MaxBSAKZLength, MinimumLength = EkkoConsts.MinBSAKZLength)]
        public string BSAKZ { get; set; }

        [StringLength(EkkoConsts.MaxLOEKZLength, MinimumLength = EkkoConsts.MinLOEKZLength)]
        public string LOEKZ { get; set; }

        [StringLength(EkkoConsts.MaxSTATULength, MinimumLength = EkkoConsts.MinSTATULength)]
        public string STATU { get; set; }

        public DateTime? AEDAT { get; set; }

        [StringLength(EkkoConsts.MaxERNAMLength, MinimumLength = EkkoConsts.MinERNAMLength)]
        public string ERNAM { get; set; }

        public long? PINCR { get; set; }

        public long? LPONR { get; set; }

        [StringLength(EkkoConsts.MaxLIFNRLength, MinimumLength = EkkoConsts.MinLIFNRLength)]
        public string LIFNR { get; set; }

        [StringLength(EkkoConsts.MaxZTERMLength, MinimumLength = EkkoConsts.MinZTERMLength)]
        public string ZTERM { get; set; }

        public decimal? ZBD1T { get; set; }

        public decimal? ZBD2T { get; set; }

        public decimal? ZBD3T { get; set; }

        public decimal? ZBD1P { get; set; }

        public decimal? ZBD2P { get; set; }

        [StringLength(EkkoConsts.MaxEKORGLength, MinimumLength = EkkoConsts.MinEKORGLength)]
        public string EKORG { get; set; }

        [StringLength(EkkoConsts.MaxEKGRPLength, MinimumLength = EkkoConsts.MinEKGRPLength)]
        public string EKGRP { get; set; }

        [StringLength(EkkoConsts.MaxWAERSLength, MinimumLength = EkkoConsts.MinWAERSLength)]
        public string WAERS { get; set; }

        public decimal? WKURS { get; set; }

        [StringLength(EkkoConsts.MaxKUFIXLength, MinimumLength = EkkoConsts.MinKUFIXLength)]
        public string KUFIX { get; set; }

        public DateTime? BEDAT { get; set; }

        public DateTime? KDATB { get; set; }

        public DateTime? KDATE { get; set; }

        public DateTime? BWBDT { get; set; }

        public DateTime? GWLDT { get; set; }

        public DateTime? IHRAN { get; set; }

        [StringLength(EkkoConsts.MaxKUNNRLength, MinimumLength = EkkoConsts.MinKUNNRLength)]
        public string KUNNR { get; set; }

        [StringLength(EkkoConsts.MaxKONNRLength, MinimumLength = EkkoConsts.MinKONNRLength)]
        public string KONNR { get; set; }

        [StringLength(EkkoConsts.MaxABGRULength, MinimumLength = EkkoConsts.MinABGRULength)]
        public string ABGRU { get; set; }

        [StringLength(EkkoConsts.MaxAUTLFLength, MinimumLength = EkkoConsts.MinAUTLFLength)]
        public string AUTLF { get; set; }

        [StringLength(EkkoConsts.MaxWEAKTLength, MinimumLength = EkkoConsts.MinWEAKTLength)]
        public string WEAKT { get; set; }

        [StringLength(EkkoConsts.MaxRESWKLength, MinimumLength = EkkoConsts.MinRESWKLength)]
        public string RESWK { get; set; }

        [StringLength(EkkoConsts.MaxLBLIFLength, MinimumLength = EkkoConsts.MinLBLIFLength)]
        public string LBLIF { get; set; }

        [StringLength(EkkoConsts.MaxINCO1Length, MinimumLength = EkkoConsts.MinINCO1Length)]
        public string INCO1 { get; set; }

        [StringLength(EkkoConsts.MaxINCO2Length, MinimumLength = EkkoConsts.MinINCO2Length)]
        public string INCO2 { get; set; }

        [StringLength(EkkoConsts.MaxSUBMILength, MinimumLength = EkkoConsts.MinSUBMILength)]
        public string SUBMI { get; set; }

        [StringLength(EkkoConsts.MaxKNUMVLength, MinimumLength = EkkoConsts.MinKNUMVLength)]
        public string KNUMV { get; set; }

        [StringLength(EkkoConsts.MaxKALSMLength, MinimumLength = EkkoConsts.MinKALSMLength)]
        public string KALSM { get; set; }

        [StringLength(EkkoConsts.MaxPROCSTATLength, MinimumLength = EkkoConsts.MinPROCSTATLength)]
        public string PROCSTAT { get; set; }

        [StringLength(EkkoConsts.MaxUNSEZLength, MinimumLength = EkkoConsts.MinUNSEZLength)]
        public string UNSEZ { get; set; }

        [StringLength(EkkoConsts.MaxFRGGRLength, MinimumLength = EkkoConsts.MinFRGGRLength)]
        public string FRGGR { get; set; }

        [StringLength(EkkoConsts.MaxFRGSXLength, MinimumLength = EkkoConsts.MinFRGSXLength)]
        public string FRGSX { get; set; }

        [StringLength(EkkoConsts.MaxFRGKELength, MinimumLength = EkkoConsts.MinFRGKELength)]
        public string FRGKE { get; set; }

        [StringLength(EkkoConsts.MaxFRGZULength, MinimumLength = EkkoConsts.MinFRGZULength)]
        public string FRGZU { get; set; }

        [StringLength(EkkoConsts.MaxADRNRLength, MinimumLength = EkkoConsts.MinADRNRLength)]
        public string ADRNR { get; set; }

    }
}