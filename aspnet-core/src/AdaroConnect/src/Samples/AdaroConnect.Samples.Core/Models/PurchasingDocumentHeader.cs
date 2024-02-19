using System;
using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;

namespace AdaroConnect.Application.Core.Models
{
    [RfcEntity("EKKO", Description = " Purchasing Document Header Table")]
    public class PurchasingDocumentHeader : ISapTable
    {
        [RfcEntityProperty("MANDT", Description = "Client", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        public string MANDT { get; set; }

        [RfcEntityProperty("EBELN", Description = "Purchasing Document Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string EBELN { get; set; }

        [RfcEntityProperty("BUKRS", Description = "Company Code", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string BUKRS { get; set; }

        [RfcEntityProperty("BSTYP", Description = "Purchasing Document Category", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string BSTYP { get; set; }

        [RfcEntityProperty("BSART", Description = "Purchasing Document Type", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string BSART { get; set; }

        [RfcEntityProperty("BSAKZ", Description = "Control indicator for purchasing document type", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string BSAKZ { get; set; }

        [RfcEntityProperty("LOEKZ", Description = "Deletion indicator in purchasing document", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string LOEKZ { get; set; }

        [RfcEntityProperty("STATU", Description = "Status of Purchasing Document", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string STATU { get; set; }

        [RfcEntityProperty("AEDAT", Description = "Date on which the record was created", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime AEDAT { get; set; }

        [RfcEntityProperty("ERNAM", Description = "Name of Person who Created the Object", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        public string ERNAM { get; set; }

        [RfcEntityProperty("PINCR", Description = "Item Number Interval", SapDataType = RfcDataTypes.NUMERIC, Length = 5)]
        public double PINCR { get; set; }

        [RfcEntityProperty("LPONR", Description = "Last Item Number", SapDataType = RfcDataTypes.NUMERIC, Length = 5)]
        public double LPONR { get; set; }

        [RfcEntityProperty("LIFNR", Description = "Vendor's account number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string LIFNR { get; set; }

        [RfcEntityProperty("ZTERM", Description = "Terms of payment key", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string ZTERM { get; set; }

        [RfcEntityProperty("ZBD1T", Description = "Cash (Prompt Payment) Discount Days", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 3)]
        public decimal ZBD1T { get; set; }

        [RfcEntityProperty("ZBD2T", Description = "Cash (Prompt Payment) Discount Days", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 3)]
        public decimal ZBD2T { get; set; }

        [RfcEntityProperty("ZBD3T", Description = "Cash (Prompt Payment) Discount Days", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 3)]
        public decimal ZBD3T { get; set; }

        [RfcEntityProperty("ZBD1P", Description = "Cash discount percentage 1", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 5)]
        public decimal ZBD1P { get; set; }

        [RfcEntityProperty("ZBD2P", Description = "Cash Discount Percentage 2", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 5)]
        public decimal ZBD2P { get; set; }

        [RfcEntityProperty("EKORG", Description = "Purchasing organization", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string EKORG { get; set; }

        [RfcEntityProperty("EKGRP", Description = "Purchasing Group", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        public string EKGRP { get; set; }

        [RfcEntityProperty("WAERS", Description = "Currency Key", SapDataType = RfcDataTypes.CHAR, Length = 5)]
        public string WAERS { get; set; }

        [RfcEntityProperty("WKURS", Description = "Exchange Rate", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 9)]
        public decimal WKURS { get; set; }

        [RfcEntityProperty("KUFIX", Description = "Indicator: Fixing of Exchange Rate", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string KUFIX { get; set; }

        [RfcEntityProperty("BEDAT", Description = "Purchasing Document Date", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime BEDAT { get; set; }

        [RfcEntityProperty("KDATB", Description = "Start of Validity Period", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime? KDATB { get; set; }

        [RfcEntityProperty("KDATE", Description = "End of Validity Period", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime? KDATE { get; set; }

        [RfcEntityProperty("BWBDT", Description = "Closing Date for Applications", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime? BWBDT { get; set; }

        //[RfcEntityProperty("ANGDT", Description = "Deadline for Submission of Bid/Quotation", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        //public string ANGDT { get; set; }

        //[RfcEntityProperty("BNDDT", Description = "Binding Period for Quotation", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        //public string BNDDT { get; set; }

        [RfcEntityProperty("GWLDT", Description = "Warranty Date", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime? GWLDT { get; set; }

        //[RfcEntityProperty("AUSNR", Description = "Bid invitation number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string AUSNR { get; set; }

        //[RfcEntityProperty("ANGNR", Description = "Quotation Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string ANGNR { get; set; }

        [RfcEntityProperty("IHRAN", Description = "Quotation Submission Date", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime? IHRAN { get; set; }

        //[RfcEntityProperty("IHREZ", Description = "Your Reference", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        //public string IHREZ { get; set; }

        //[RfcEntityProperty("VERKF", Description = "Responsible Salesperson at Vendor's Office", SapDataType = RfcDataTypes.CHAR, Length = 30)]
        //public string VERKF { get; set; }

        //[RfcEntityProperty("TELF1", Description = "Vendor's Telephone Number", SapDataType = RfcDataTypes.CHAR, Length = 16)]
        //public string TELF1 { get; set; }

        //[RfcEntityProperty("LLIEF", Description = "Supplying Vendor", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string LLIEF { get; set; }

        [RfcEntityProperty("KUNNR", Description = "Customer Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string KUNNR { get; set; }

        [RfcEntityProperty("KONNR", Description = "Number of principal purchase agreement", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string KONNR { get; set; }

        [RfcEntityProperty("ABGRU", Description = "Reason for rejection of quotations and sales orders", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        public string ABGRU { get; set; }

        [RfcEntityProperty("AUTLF", Description = "Complete Delivery Stipulated for Each Purchase Order", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string AUTLF { get; set; }

        [RfcEntityProperty("WEAKT", Description = "Indicator: Goods Receipt Message", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string WEAKT { get; set; }

        [RfcEntityProperty("RESWK", Description = "Supplying (issuing) plant in case of stock transport order", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string RESWK { get; set; }

        //[RfcEntityProperty("LBLIF", Description = "Field not used", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string LBLIF { get; set; }

        [RfcEntityProperty("INCO1", Description = "Incoterms (part 1)", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        public string INCO1 { get; set; }

        [RfcEntityProperty("INCO2", Description = "Incoterms (part 2)", SapDataType = RfcDataTypes.CHAR, Length = 28)]
        public string INCO2 { get; set; }

        ////[RfcEntityProperty("KTWRT", Description = "Target Value for Header Area per Distribution", SapDataType = RfcDataTypes.CURR, Length = 15)]
        ////public string KTWRT { get; set; }

        [RfcEntityProperty("SUBMI", Description = "Collective Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string SUBMI { get; set; }

        [RfcEntityProperty("KNUMV", Description = "Number of the document condition", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string KNUMV { get; set; }

        [RfcEntityProperty("KALSM", Description = "Procedure (Pricing, Output Control, Acct. Det., Costing,...)", SapDataType = RfcDataTypes.CHAR, Length = 6)]
        public string KALSM { get; set; }

        [RfcEntityProperty("PROCSTAT", Description = "Purchasing document processing state", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        public string PROCSTAT { get; set; }

        //[RfcEntityProperty("STAFO", Description = "Update group for statistics update", SapDataType = RfcDataTypes.CHAR, Length = 6)]
        //public string STAFO { get; set; }

        //[RfcEntityProperty("LIFRE", Description = "Different Invoicing Party", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string LIFRE { get; set; }

        //[RfcEntityProperty("EXNUM", Description = "Number of foreign trade data in MM and SD documents", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string EXNUM { get; set; }

        [RfcEntityProperty("UNSEZ", Description = "Our Reference", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        public string UNSEZ { get; set; }

        //[RfcEntityProperty("LOGSY", Description = "Logical System", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string LOGSY { get; set; }

        //[RfcEntityProperty("UPINC", Description = "Item Number Interval for Subitems", SapDataType = RfcDataTypes.NUMERIC, Length = 5)]
        //public string UPINC { get; set; }

        //[RfcEntityProperty("STAKO", Description = "Document with time-dependent conditions", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string STAKO { get; set; }

        [RfcEntityProperty("FRGGR", Description = "Release group", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        public string FRGGR { get; set; }

        [RfcEntityProperty("FRGSX", Description = "Release Strategy", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        public string FRGSX { get; set; }

        [RfcEntityProperty("FRGKE", Description = "Release Indicator: Purchasing Document", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string FRGKE { get; set; }

        [RfcEntityProperty("FRGZU", Description = "Release status", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        public string FRGZU { get; set; }

        //[RfcEntityProperty("FRGRL", Description = "Release Not Yet Completely Effected", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string FRGRL { get; set; }

        //[RfcEntityProperty("LANDS", Description = "Country for Tax Return", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        //public string LANDS { get; set; }

        //[RfcEntityProperty("LPHIS", Description = "Indicator for scheduling agreement release documentation", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string LPHIS { get; set; }

        [RfcEntityProperty("ADRNR", Description = "Address number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string ADRNR { get; set; }

        //[RfcEntityProperty("STCEG_L", Description = "Country of sales tax ID number", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        //public string STCEG_L { get; set; }

        //[RfcEntityProperty("STCEG", Description = "VAT Registration Number", SapDataType = RfcDataTypes.CHAR, Length = 20)]
        //public string STCEG { get; set; }

        //[RfcEntityProperty("ABSGR", Description = "Reason for Cancellation", SapDataType = RfcDataTypes.NUMERIC, Length = 2)]
        //public string ABSGR { get; set; }

        //[RfcEntityProperty("ADDNR", Description = "Document number for additional", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string ADDNR { get; set; }

        //[RfcEntityProperty("KORNR", Description = "Correction of miscellaneous provisions", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string KORNR { get; set; }
    }
}
