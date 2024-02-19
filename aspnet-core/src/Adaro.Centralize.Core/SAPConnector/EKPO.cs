using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.SAPConnector
{
    [Table("EKPO")]
    public class EKPO : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [StringLength(EKPOConsts.MaxMANDTLength, MinimumLength = EKPOConsts.MinMANDTLength)]
        public virtual string MANDT { get; set; }

        [StringLength(EKPOConsts.MaxEBELNLength, MinimumLength = EKPOConsts.MinEBELNLength)]
        public virtual string EBELN { get; set; }

        public virtual long? EBELP { get; set; }

        [StringLength(EKPOConsts.MaxUNIQUEIDLength, MinimumLength = EKPOConsts.MinUNIQUEIDLength)]
        public virtual string UNIQUEID { get; set; }

        [StringLength(EKPOConsts.MaxLOEKZLength, MinimumLength = EKPOConsts.MinLOEKZLength)]
        public virtual string LOEKZ { get; set; }

        [StringLength(EKPOConsts.MaxSTATULength, MinimumLength = EKPOConsts.MinSTATULength)]
        public virtual string STATU { get; set; }

        public virtual DateTime? AEDAT { get; set; }

        [StringLength(EKPOConsts.MaxTXZ01Length, MinimumLength = EKPOConsts.MinTXZ01Length)]
        public virtual string TXZ01 { get; set; }

        [StringLength(EKPOConsts.MaxMATNRLength, MinimumLength = EKPOConsts.MinMATNRLength)]
        public virtual string MATNR { get; set; }

        [StringLength(EKPOConsts.MaxEMATNLength, MinimumLength = EKPOConsts.MinEMATNLength)]
        public virtual string EMATN { get; set; }

        [StringLength(EKPOConsts.MaxBUKRSLength, MinimumLength = EKPOConsts.MinBUKRSLength)]
        public virtual string BUKRS { get; set; }

        [StringLength(EKPOConsts.MaxWERKSLength, MinimumLength = EKPOConsts.MinWERKSLength)]
        public virtual string WERKS { get; set; }

        [StringLength(EKPOConsts.MaxLGORTLength, MinimumLength = EKPOConsts.MinLGORTLength)]
        public virtual string LGORT { get; set; }

        [StringLength(EKPOConsts.MaxBEDNRLength, MinimumLength = EKPOConsts.MinBEDNRLength)]
        public virtual string BEDNR { get; set; }

        [StringLength(EKPOConsts.MaxMATKLLength, MinimumLength = EKPOConsts.MinMATKLLength)]
        public virtual string MATKL { get; set; }

        [StringLength(EKPOConsts.MaxINFNRLength, MinimumLength = EKPOConsts.MinINFNRLength)]
        public virtual string INFNR { get; set; }

        [StringLength(EKPOConsts.MaxIDNLFLength, MinimumLength = EKPOConsts.MinIDNLFLength)]
        public virtual string IDNLF { get; set; }

        public virtual long? KTMNG { get; set; }

        public virtual long? MENGE { get; set; }

        [StringLength(EKPOConsts.MaxMEINSLength, MinimumLength = EKPOConsts.MinMEINSLength)]
        public virtual string MEINS { get; set; }

        [StringLength(EKPOConsts.MaxBPRMELength, MinimumLength = EKPOConsts.MinBPRMELength)]
        public virtual string BPRME { get; set; }

        public virtual decimal? BPUMZ { get; set; }

        public virtual decimal? BPUMN { get; set; }

        public virtual decimal? UMREZ { get; set; }

        public virtual decimal? UMREN { get; set; }

        public virtual decimal? NETPR { get; set; }

        public virtual decimal? PEINH { get; set; }

        public virtual decimal? NETWR { get; set; }

        public virtual decimal? BRTWR { get; set; }

        public virtual DateTime? AGDAT { get; set; }

        public virtual decimal? WEBAZ { get; set; }

        [StringLength(EKPOConsts.MaxMWSKZLength, MinimumLength = EKPOConsts.MinMWSKZLength)]
        public virtual string MWSKZ { get; set; }

        [StringLength(EKPOConsts.MaxBONUSLength, MinimumLength = EKPOConsts.MinBONUSLength)]
        public virtual string BONUS { get; set; }

        [StringLength(EKPOConsts.MaxINSMKLength, MinimumLength = EKPOConsts.MinINSMKLength)]
        public virtual string INSMK { get; set; }

        [StringLength(EKPOConsts.MaxSPINFLength, MinimumLength = EKPOConsts.MinSPINFLength)]
        public virtual string SPINF { get; set; }

        [StringLength(EKPOConsts.MaxPRSDRLength, MinimumLength = EKPOConsts.MinPRSDRLength)]
        public virtual string PRSDR { get; set; }

        [StringLength(EKPOConsts.MaxBWTARLength, MinimumLength = EKPOConsts.MinBWTARLength)]
        public virtual string BWTAR { get; set; }

        [StringLength(EKPOConsts.MaxBWTTYLength, MinimumLength = EKPOConsts.MinBWTTYLength)]
        public virtual string BWTTY { get; set; }

        [StringLength(EKPOConsts.MaxABSKZLength, MinimumLength = EKPOConsts.MinABSKZLength)]
        public virtual string ABSKZ { get; set; }

        [StringLength(EKPOConsts.MaxPSTYPLength, MinimumLength = EKPOConsts.MinPSTYPLength)]
        public virtual string PSTYP { get; set; }

        [StringLength(EKPOConsts.MaxKNTTPLength, MinimumLength = EKPOConsts.MinKNTTPLength)]
        public virtual string KNTTP { get; set; }

        [StringLength(EKPOConsts.MaxKONNRLength, MinimumLength = EKPOConsts.MinKONNRLength)]
        public virtual string KONNR { get; set; }

        public virtual long? KTPNR { get; set; }

        public virtual decimal? PACKNO { get; set; }

        [StringLength(EKPOConsts.MaxANFNRLength, MinimumLength = EKPOConsts.MinANFNRLength)]
        public virtual string ANFNR { get; set; }

        [StringLength(EKPOConsts.MaxBANFNLength, MinimumLength = EKPOConsts.MinBANFNLength)]
        public virtual string BANFN { get; set; }

        public virtual long? BNFPO { get; set; }

    }
}