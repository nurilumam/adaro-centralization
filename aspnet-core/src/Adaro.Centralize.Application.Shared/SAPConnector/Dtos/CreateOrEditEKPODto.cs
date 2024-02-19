using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class CreateOrEditEKPODto : EntityDto<Guid?>
    {

        [StringLength(EKPOConsts.MaxMANDTLength, MinimumLength = EKPOConsts.MinMANDTLength)]
        public string MANDT { get; set; }

        [StringLength(EKPOConsts.MaxEBELNLength, MinimumLength = EKPOConsts.MinEBELNLength)]
        public string EBELN { get; set; }

        public long? EBELP { get; set; }

        [StringLength(EKPOConsts.MaxUNIQUEIDLength, MinimumLength = EKPOConsts.MinUNIQUEIDLength)]
        public string UNIQUEID { get; set; }

        [StringLength(EKPOConsts.MaxLOEKZLength, MinimumLength = EKPOConsts.MinLOEKZLength)]
        public string LOEKZ { get; set; }

        [StringLength(EKPOConsts.MaxSTATULength, MinimumLength = EKPOConsts.MinSTATULength)]
        public string STATU { get; set; }

        public DateTime? AEDAT { get; set; }

        [StringLength(EKPOConsts.MaxTXZ01Length, MinimumLength = EKPOConsts.MinTXZ01Length)]
        public string TXZ01 { get; set; }

        [StringLength(EKPOConsts.MaxMATNRLength, MinimumLength = EKPOConsts.MinMATNRLength)]
        public string MATNR { get; set; }

        [StringLength(EKPOConsts.MaxEMATNLength, MinimumLength = EKPOConsts.MinEMATNLength)]
        public string EMATN { get; set; }

        [StringLength(EKPOConsts.MaxBUKRSLength, MinimumLength = EKPOConsts.MinBUKRSLength)]
        public string BUKRS { get; set; }

        [StringLength(EKPOConsts.MaxWERKSLength, MinimumLength = EKPOConsts.MinWERKSLength)]
        public string WERKS { get; set; }

        [StringLength(EKPOConsts.MaxLGORTLength, MinimumLength = EKPOConsts.MinLGORTLength)]
        public string LGORT { get; set; }

        [StringLength(EKPOConsts.MaxBEDNRLength, MinimumLength = EKPOConsts.MinBEDNRLength)]
        public string BEDNR { get; set; }

        [StringLength(EKPOConsts.MaxMATKLLength, MinimumLength = EKPOConsts.MinMATKLLength)]
        public string MATKL { get; set; }

        [StringLength(EKPOConsts.MaxINFNRLength, MinimumLength = EKPOConsts.MinINFNRLength)]
        public string INFNR { get; set; }

        [StringLength(EKPOConsts.MaxIDNLFLength, MinimumLength = EKPOConsts.MinIDNLFLength)]
        public string IDNLF { get; set; }

        public long? KTMNG { get; set; }

        public long? MENGE { get; set; }

        [StringLength(EKPOConsts.MaxMEINSLength, MinimumLength = EKPOConsts.MinMEINSLength)]
        public string MEINS { get; set; }

        [StringLength(EKPOConsts.MaxBPRMELength, MinimumLength = EKPOConsts.MinBPRMELength)]
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

        [StringLength(EKPOConsts.MaxMWSKZLength, MinimumLength = EKPOConsts.MinMWSKZLength)]
        public string MWSKZ { get; set; }

        [StringLength(EKPOConsts.MaxBONUSLength, MinimumLength = EKPOConsts.MinBONUSLength)]
        public string BONUS { get; set; }

        [StringLength(EKPOConsts.MaxINSMKLength, MinimumLength = EKPOConsts.MinINSMKLength)]
        public string INSMK { get; set; }

        [StringLength(EKPOConsts.MaxSPINFLength, MinimumLength = EKPOConsts.MinSPINFLength)]
        public string SPINF { get; set; }

        [StringLength(EKPOConsts.MaxPRSDRLength, MinimumLength = EKPOConsts.MinPRSDRLength)]
        public string PRSDR { get; set; }

        [StringLength(EKPOConsts.MaxBWTARLength, MinimumLength = EKPOConsts.MinBWTARLength)]
        public string BWTAR { get; set; }

        [StringLength(EKPOConsts.MaxBWTTYLength, MinimumLength = EKPOConsts.MinBWTTYLength)]
        public string BWTTY { get; set; }

        [StringLength(EKPOConsts.MaxABSKZLength, MinimumLength = EKPOConsts.MinABSKZLength)]
        public string ABSKZ { get; set; }

        [StringLength(EKPOConsts.MaxPSTYPLength, MinimumLength = EKPOConsts.MinPSTYPLength)]
        public string PSTYP { get; set; }

        [StringLength(EKPOConsts.MaxKNTTPLength, MinimumLength = EKPOConsts.MinKNTTPLength)]
        public string KNTTP { get; set; }

        [StringLength(EKPOConsts.MaxKONNRLength, MinimumLength = EKPOConsts.MinKONNRLength)]
        public string KONNR { get; set; }

        public long? KTPNR { get; set; }

        public decimal? PACKNO { get; set; }

        [StringLength(EKPOConsts.MaxANFNRLength, MinimumLength = EKPOConsts.MinANFNRLength)]
        public string ANFNR { get; set; }

        [StringLength(EKPOConsts.MaxBANFNLength, MinimumLength = EKPOConsts.MinBANFNLength)]
        public string BANFN { get; set; }

        public long? BNFPO { get; set; }

    }
}