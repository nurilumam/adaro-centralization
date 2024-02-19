using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.SAPConnector
{
    [Table("EKKO")]
    public class EKKO : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(EkkoConsts.MaxMANDTLength, MinimumLength = EkkoConsts.MinMANDTLength)]
        public virtual string MANDT { get; set; }

        [StringLength(EkkoConsts.MaxEBELNLength, MinimumLength = EkkoConsts.MinEBELNLength)]
        public virtual string EBELN { get; set; }

        [StringLength(EkkoConsts.MaxBUKRSLength, MinimumLength = EkkoConsts.MinBUKRSLength)]
        public virtual string BUKRS { get; set; }

        [StringLength(EkkoConsts.MaxBSTYPLength, MinimumLength = EkkoConsts.MinBSTYPLength)]
        public virtual string BSTYP { get; set; }

        [StringLength(EkkoConsts.MaxBSARTLength, MinimumLength = EkkoConsts.MinBSARTLength)]
        public virtual string BSART { get; set; }

        [StringLength(EkkoConsts.MaxBSAKZLength, MinimumLength = EkkoConsts.MinBSAKZLength)]
        public virtual string BSAKZ { get; set; }

        [StringLength(EkkoConsts.MaxLOEKZLength, MinimumLength = EkkoConsts.MinLOEKZLength)]
        public virtual string LOEKZ { get; set; }

        [StringLength(EkkoConsts.MaxSTATULength, MinimumLength = EkkoConsts.MinSTATULength)]
        public virtual string STATU { get; set; }

        public virtual DateTime? AEDAT { get; set; }

        [StringLength(EkkoConsts.MaxERNAMLength, MinimumLength = EkkoConsts.MinERNAMLength)]
        public virtual string ERNAM { get; set; }

        public virtual long? PINCR { get; set; }

        public virtual long? LPONR { get; set; }

        [StringLength(EkkoConsts.MaxLIFNRLength, MinimumLength = EkkoConsts.MinLIFNRLength)]
        public virtual string LIFNR { get; set; }

        [StringLength(EkkoConsts.MaxZTERMLength, MinimumLength = EkkoConsts.MinZTERMLength)]
        public virtual string ZTERM { get; set; }

        public virtual decimal? ZBD1T { get; set; }

        public virtual decimal? ZBD2T { get; set; }

        public virtual decimal? ZBD3T { get; set; }

        public virtual decimal? ZBD1P { get; set; }

        public virtual decimal? ZBD2P { get; set; }

        [StringLength(EkkoConsts.MaxEKORGLength, MinimumLength = EkkoConsts.MinEKORGLength)]
        public virtual string EKORG { get; set; }

        [StringLength(EkkoConsts.MaxEKGRPLength, MinimumLength = EkkoConsts.MinEKGRPLength)]
        public virtual string EKGRP { get; set; }

        [StringLength(EkkoConsts.MaxWAERSLength, MinimumLength = EkkoConsts.MinWAERSLength)]
        public virtual string WAERS { get; set; }

        public virtual decimal? WKURS { get; set; }

        [StringLength(EkkoConsts.MaxKUFIXLength, MinimumLength = EkkoConsts.MinKUFIXLength)]
        public virtual string KUFIX { get; set; }

        public virtual DateTime? BEDAT { get; set; }

        public virtual DateTime? KDATB { get; set; }

        public virtual DateTime? KDATE { get; set; }

        public virtual DateTime? BWBDT { get; set; }

        public virtual DateTime? GWLDT { get; set; }

        public virtual DateTime? IHRAN { get; set; }

        [StringLength(EkkoConsts.MaxKUNNRLength, MinimumLength = EkkoConsts.MinKUNNRLength)]
        public virtual string KUNNR { get; set; }

        [StringLength(EkkoConsts.MaxKONNRLength, MinimumLength = EkkoConsts.MinKONNRLength)]
        public virtual string KONNR { get; set; }

        [StringLength(EkkoConsts.MaxABGRULength, MinimumLength = EkkoConsts.MinABGRULength)]
        public virtual string ABGRU { get; set; }

        [StringLength(EkkoConsts.MaxAUTLFLength, MinimumLength = EkkoConsts.MinAUTLFLength)]
        public virtual string AUTLF { get; set; }

        [StringLength(EkkoConsts.MaxWEAKTLength, MinimumLength = EkkoConsts.MinWEAKTLength)]
        public virtual string WEAKT { get; set; }

        [StringLength(EkkoConsts.MaxRESWKLength, MinimumLength = EkkoConsts.MinRESWKLength)]
        public virtual string RESWK { get; set; }

        [StringLength(EkkoConsts.MaxLBLIFLength, MinimumLength = EkkoConsts.MinLBLIFLength)]
        public virtual string LBLIF { get; set; }

        [StringLength(EkkoConsts.MaxINCO1Length, MinimumLength = EkkoConsts.MinINCO1Length)]
        public virtual string INCO1 { get; set; }

        [StringLength(EkkoConsts.MaxINCO2Length, MinimumLength = EkkoConsts.MinINCO2Length)]
        public virtual string INCO2 { get; set; }

        [StringLength(EkkoConsts.MaxSUBMILength, MinimumLength = EkkoConsts.MinSUBMILength)]
        public virtual string SUBMI { get; set; }

        [StringLength(EkkoConsts.MaxKNUMVLength, MinimumLength = EkkoConsts.MinKNUMVLength)]
        public virtual string KNUMV { get; set; }

        [StringLength(EkkoConsts.MaxKALSMLength, MinimumLength = EkkoConsts.MinKALSMLength)]
        public virtual string KALSM { get; set; }

        [StringLength(EkkoConsts.MaxPROCSTATLength, MinimumLength = EkkoConsts.MinPROCSTATLength)]
        public virtual string PROCSTAT { get; set; }

        [StringLength(EkkoConsts.MaxUNSEZLength, MinimumLength = EkkoConsts.MinUNSEZLength)]
        public virtual string UNSEZ { get; set; }

        [StringLength(EkkoConsts.MaxFRGGRLength, MinimumLength = EkkoConsts.MinFRGGRLength)]
        public virtual string FRGGR { get; set; }

        [StringLength(EkkoConsts.MaxFRGSXLength, MinimumLength = EkkoConsts.MinFRGSXLength)]
        public virtual string FRGSX { get; set; }

        [StringLength(EkkoConsts.MaxFRGKELength, MinimumLength = EkkoConsts.MinFRGKELength)]
        public virtual string FRGKE { get; set; }

        [StringLength(EkkoConsts.MaxFRGZULength, MinimumLength = EkkoConsts.MinFRGZULength)]
        public virtual string FRGZU { get; set; }

        [StringLength(EkkoConsts.MaxADRNRLength, MinimumLength = EkkoConsts.MinADRNRLength)]
        public virtual string ADRNR { get; set; }

    }
}