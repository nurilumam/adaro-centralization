using System;
using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;

namespace AdaroConnect.Application.Core.Models
{
    [RfcEntity("EKPO", Description = " Purchasing Document Item Table")]
    public class PurchasingDocumentItem : ISapTable
    {
        [RfcEntityProperty("MANDT", Description = "Client", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        public string MANDT { get; set; }

        [RfcEntityProperty("EBELN", Description = "Purchasing Document Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string EBELN { get; set; }

        [RfcEntityProperty("EBELP", Description = "Item Number of Purchasing Document", SapDataType = RfcDataTypes.NUMERIC, Length = 5)]
        public double EBELP { get; set; }

        [RfcEntityProperty("UNIQUEID", Description = "Document Item", SapDataType = RfcDataTypes.CHAR, Length = 20)]
        public string UNIQUEID { get; set; }


        [RfcEntityProperty("LOEKZ", Description = "Deletion indicator in purchasing document", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string LOEKZ { get; set; }

        [RfcEntityProperty("STATU", Description = "RFQ status", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string STATU { get; set; }

        [RfcEntityProperty("AEDAT", Description = "Purchasing Document Item Change Date", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime AEDAT { get; set; }

        [RfcEntityProperty("CREATIONDATE", Description = "Creation Date", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime CREATIONDATE { get; set; }

        [RfcEntityProperty("TXZ01", Description = "Short Text", SapDataType = RfcDataTypes.CHAR, Length = 40)]
        public string TXZ01 { get; set; }

        [RfcEntityProperty("MATNR", Description = "Material Number", SapDataType = RfcDataTypes.CHAR, Length = 18)]
        public string MATNR { get; set; }

        [RfcEntityProperty("EMATN", Description = "Material number", SapDataType = RfcDataTypes.CHAR, Length = 18)]
        public string EMATN { get; set; }

        [RfcEntityProperty("BUKRS", Description = "Company Code", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string BUKRS { get; set; }

        [RfcEntityProperty("WERKS", Description = "Plant", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string WERKS { get; set; }

        [RfcEntityProperty("LGORT", Description = "Storage location", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string LGORT { get; set; }

        [RfcEntityProperty("BEDNR", Description = "Requirement Tracking Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string BEDNR { get; set; }

        [RfcEntityProperty("MATKL", Description = "Material Group", SapDataType = RfcDataTypes.CHAR, Length = 9)]
        public string MATKL { get; set; }

        [RfcEntityProperty("INFNR", Description = "Number of purchasing info record", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string INFNR { get; set; }

        [RfcEntityProperty("IDNLF", Description = "Material Number Used by Vendor", SapDataType = RfcDataTypes.CHAR, Length = 35)]
        public string IDNLF { get; set; }

        [RfcEntityProperty("KTMNG", Description = "Target Quantity", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        public double KTMNG { get; set; }

        [RfcEntityProperty("MENGE", Description = "Purchase Order Quantity", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        public double MENGE { get; set; }

        [RfcEntityProperty("MEINS", Description = "Order unit", SapDataType = RfcDataTypes.UNIT, Length = 3)]
        public string MEINS { get; set; }

        [RfcEntityProperty("BPRME", Description = "Order Price Unit (purchasing)", SapDataType = RfcDataTypes.UNIT, Length = 3)]
        public string BPRME { get; set; }

        [RfcEntityProperty("BPUMZ", Description = "Numerator for Conversion of Order Price Unit into Order Unit", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 5)]
        public decimal BPUMZ { get; set; }

        [RfcEntityProperty("BPUMN", Description = "Denominator for Conv. of Order Price Unit into Order Unit", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 5)]
        public decimal BPUMN { get; set; }

        [RfcEntityProperty("UMREZ", Description = "Numerator for Conversion of Order Unit to Base Unit", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 5)]
        public decimal UMREZ { get; set; }

        [RfcEntityProperty("UMREN", Description = "Denominator for Conversion of Order Unit to Base Unit", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 5)]
        public decimal UMREN { get; set; }

        [RfcEntityProperty("NETPR", Description = "Net Price in Purchasing Document (in Document Currency)", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        public decimal NETPR { get; set; }

        [RfcEntityProperty("PEINH", Description = "Price unit", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 5)]
        public decimal PEINH { get; set; }

        [RfcEntityProperty("NETWR", Description = "Net Order Value in PO Currency", SapDataType = RfcDataTypes.DECIMAL, Length = 13)]
        public decimal NETWR { get; set; }

        [RfcEntityProperty("BRTWR", Description = "Gross order value in PO currency", SapDataType = RfcDataTypes.DECIMAL, Length = 13)]
        public decimal BRTWR { get; set; }

        [RfcEntityProperty("AGDAT", Description = "Deadline for Submission of Bid/Quotation", SapDataType = RfcDataTypes.DATE_8, Length = 8)]
        public DateTime AGDAT { get; set; }

        [RfcEntityProperty("WEBAZ", Description = "Goods receipt processing time in days", SapDataType = RfcDataTypes.DECIMAL, Length = 3)]
        public decimal WEBAZ { get; set; }

        [RfcEntityProperty("MWSKZ", Description = "Tax on sales/purchases code", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        public string MWSKZ { get; set; }

        [RfcEntityProperty("BONUS", Description = "Settlement Group 1 (Purchasing)", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        public string BONUS { get; set; }

        [RfcEntityProperty("INSMK", Description = "Stock Type", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string INSMK { get; set; }

        [RfcEntityProperty("SPINF", Description = "Indicator: Update Info Record", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string SPINF { get; set; }

        [RfcEntityProperty("PRSDR", Description = "Price Printout", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string PRSDR { get; set; }

        //[RfcEntityProperty("SCHPR", Description = "Indicator: Estimated Price", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string SCHPR { get; set; }

        //[RfcEntityProperty("MAHNZ", Description = "Number of Reminders/Expediters", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //public string MAHNZ { get; set; }

        //[RfcEntityProperty("MAHN1", Description = "Number of Days for First Reminder/Expediter", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //public string MAHN1 { get; set; }

        //[RfcEntityProperty("MAHN2", Description = "Number of Days for Second Reminder/Expediter", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //public string MAHN2 { get; set; }

        //[RfcEntityProperty("MAHN3", Description = "Number of Days for Third Reminder/Expediter", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //public string MAHN3 { get; set; }

        //[RfcEntityProperty("UEBTO", Description = "Overdelivery Tolerance Limit", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //public string UEBTO { get; set; }

        //[RfcEntityProperty("UEBTK", Description = "Indicator: Unlimited Overdelivery Allowed", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string UEBTK { get; set; }

        //[RfcEntityProperty("UNTTO", Description = "Underdelivery Tolerance Limit", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //public string UNTTO { get; set; }

        [RfcEntityProperty("BWTAR", Description = "Valuation type", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string BWTAR { get; set; }

        [RfcEntityProperty("BWTTY", Description = "Valuation Category", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string BWTTY { get; set; }

        [RfcEntityProperty("ABSKZ", Description = "Rejection Indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string ABSKZ { get; set; }

        //[RfcEntityProperty("AGMEM", Description = "Internal Comment on Quotation", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        //public string AGMEM { get; set; }

        //[RfcEntityProperty("ELIKZ", Description = "&quot;Delivery Completed&quot; Indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string ELIKZ { get; set; }

        //[RfcEntityProperty("EREKZ", Description = "Final Invoice Indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string EREKZ { get; set; }

        [RfcEntityProperty("PSTYP", Description = "Item category in purchasing document", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string PSTYP { get; set; }

        [RfcEntityProperty("KNTTP", Description = "Account assignment category", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string KNTTP { get; set; }

        //[RfcEntityProperty("KZVBR", Description = "Consumption posting", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string KZVBR { get; set; }

        //[RfcEntityProperty("VRTKZ", Description = "Distribution indicator for multiple account assignment", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string VRTKZ { get; set; }

        //[RfcEntityProperty("TWRKZ", Description = "Partial invoice indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string TWRKZ { get; set; }

        //[RfcEntityProperty("WEPOS", Description = "Goods Receipt Indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string WEPOS { get; set; }

        //[RfcEntityProperty("WEUNB", Description = "Goods Receipt, Non-Valuated", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string WEUNB { get; set; }

        //[RfcEntityProperty("REPOS", Description = "Invoice receipt indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string REPOS { get; set; }

        //[RfcEntityProperty("WEBRE", Description = "Indicator: GR-Based Invoice Verification", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string WEBRE { get; set; }

        //[RfcEntityProperty("KZABS", Description = "Order Acknowledgment Requirement", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string KZABS { get; set; }

        //[RfcEntityProperty("LABNR", Description = "Order Acknowledgment Number", SapDataType = RfcDataTypes.CHAR, Length = 20)]
        //public string LABNR { get; set; }

        [RfcEntityProperty("KONNR", Description = "Number of principal purchase agreement", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string KONNR { get; set; }

        [RfcEntityProperty("KTPNR", Description = "Item number of principal purchase agreement", SapDataType = RfcDataTypes.NUMERIC, Length = 5)]
        public double KTPNR { get; set; }

        //[RfcEntityProperty("ABDAT", Description = "Reconciliation date for agreed cumulative quantity", SapDataType = RfcDataTypes.DATS, Length = 8)]
        //public string ABDAT { get; set; }

        //[RfcEntityProperty("ABFTZ", Description = "Agreed Cumulative Quantity", SapDataType = RfcDataTypes.QUAN, Length = 13)]
        //public string ABFTZ { get; set; }

        //[RfcEntityProperty("ETFZ1", Description = "Firm Zone (Go-Ahead for Production)", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //public string ETFZ1 { get; set; }

        //[RfcEntityProperty("ETFZ2", Description = "Trade-Off Zone (Go-Ahead for Materials Procurement)", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //public string ETFZ2 { get; set; }

        //[RfcEntityProperty("KZSTU", Description = "Firm/Trade-Off Zones Binding with Regard to Mat. Planning", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string KZSTU { get; set; }

        //[RfcEntityProperty("NOTKZ", Description = "Exclusion in Outline Agreement Item with Material Class", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string NOTKZ { get; set; }

        //[RfcEntityProperty("LMEIN", Description = "Base Unit of Measure", SapDataType = RfcDataTypes.UNIT, Length = 3)]
        //public string LMEIN { get; set; }

        //[RfcEntityProperty("EVERS", Description = "Shipping Instructions", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        //public string EVERS { get; set; }

        //[RfcEntityProperty("ZWERT", Description = "Target value for outline agreement in document currency", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string ZWERT { get; set; }

        //[RfcEntityProperty("NAVNW", Description = "Non-deductible input tax", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string NAVNW { get; set; }

        //[RfcEntityProperty("ABMNG", Description = "Standard release order quantity", SapDataType = RfcDataTypes.QUAN, Length = 13)]
        //public string ABMNG { get; set; }

        //[RfcEntityProperty("PRDAT", Description = "Date of Price Determination", SapDataType = RfcDataTypes.DATS, Length = 8)]
        //public string PRDAT { get; set; }

        //[RfcEntityProperty("BSTYP", Description = "Purchasing document category", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string BSTYP { get; set; }

        //[RfcEntityProperty("EFFWR", Description = "Effective value of item", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string EFFWR { get; set; }

        //[RfcEntityProperty("XOBLR", Description = "Item affects commitments", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string XOBLR { get; set; }

        //[RfcEntityProperty("KUNNR", Description = "Customer", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string KUNNR { get; set; }

        //[RfcEntityProperty("ADRNR", Description = "Manual address number in purchasing document item", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string ADRNR { get; set; }

        //[RfcEntityProperty("EKKOL", Description = "Condition Group with Vendor", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string EKKOL { get; set; }

        //[RfcEntityProperty("SKTOF", Description = "Item Does Not Qualify for Cash Discount", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string SKTOF { get; set; }

        //[RfcEntityProperty("STAFO", Description = "Update group for statistics update", SapDataType = RfcDataTypes.CHAR, Length = 6)]
        //public string STAFO { get; set; }

        //[RfcEntityProperty("PLIFZ", Description = "Planned Delivery Time in Days", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //public string PLIFZ { get; set; }

        //[RfcEntityProperty("NTGEW", Description = "Net Weight", SapDataType = RfcDataTypes.QUAN, Length = 13)]
        //public string NTGEW { get; set; }

        //[RfcEntityProperty("GEWEI", Description = "Unit of Weight", SapDataType = RfcDataTypes.UNIT, Length = 3)]
        //public string GEWEI { get; set; }

        //[RfcEntityProperty("TXJCD", Description = "Tax Jurisdiction", SapDataType = RfcDataTypes.CHAR, Length = 15)]
        //public string TXJCD { get; set; }

        //[RfcEntityProperty("ETDRK", Description = "Indicator: Print-relevant schedule lines exist", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string ETDRK { get; set; }

        //[RfcEntityProperty("SOBKZ", Description = "Special Stock Indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string SOBKZ { get; set; }

        //[RfcEntityProperty("ARSNR", Description = "Settlement reservation number", SapDataType = RfcDataTypes.NUMC, Length = 10)]
        //public string ARSNR { get; set; }

        //[RfcEntityProperty("ARSPS", Description = "Item number of the settlement reservation", SapDataType = RfcDataTypes.NUMC, Length = 4)]
        //public string ARSPS { get; set; }

        //[RfcEntityProperty("INSNC", Description = "Quality inspection indicator cannot be changed", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string INSNC { get; set; }

        //[RfcEntityProperty("SSQSS", Description = "Control Key for Quality Management in Procurement", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        //public string SSQSS { get; set; }

        //[RfcEntityProperty("ZGTYP", Description = "Certificate Type", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string ZGTYP { get; set; }

        //[RfcEntityProperty("EAN11", Description = "International Article Number (EAN/UPC)", SapDataType = RfcDataTypes.CHAR, Length = 18)]
        //public string EAN11 { get; set; }

        //[RfcEntityProperty("BSTAE", Description = "Confirmation Control Key", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string BSTAE { get; set; }

        //[RfcEntityProperty("REVLV", Description = "Revision level", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        //public string REVLV { get; set; }

        //[RfcEntityProperty("GEBER", Description = "Fund", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string GEBER { get; set; }

        //[RfcEntityProperty("FISTL", Description = "Funds Center", SapDataType = RfcDataTypes.CHAR, Length = 16)]
        //public string FISTL { get; set; }

        //[RfcEntityProperty("FIPOS", Description = "Commitment Item", SapDataType = RfcDataTypes.CHAR, Length = 14)]
        //public string FIPOS { get; set; }

        //[RfcEntityProperty("KO_GSBER", Description = "Business area reported to the partner", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string KO_GSBER { get; set; }

        //[RfcEntityProperty("KO_PARGB", Description = "assumed business area of the business partner", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string KO_PARGB { get; set; }

        //[RfcEntityProperty("KO_PRCTR", Description = "Profit Center", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string KO_PRCTR { get; set; }

        //[RfcEntityProperty("KO_PPRCTR", Description = "Partner Profit Center", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string KO_PPRCTR { get; set; }

        //[RfcEntityProperty("MEPRF", Description = "Price Determination (Pricing) Date Control", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string MEPRF { get; set; }

        //[RfcEntityProperty("BRGEW", Description = "Gross weight", SapDataType = RfcDataTypes.QUAN, Length = 13)]
        //public string BRGEW { get; set; }

        //[RfcEntityProperty("VOLUM", Description = "Volume", SapDataType = RfcDataTypes.QUAN, Length = 13)]
        //public string VOLUM { get; set; }

        //[RfcEntityProperty("VOLEH", Description = "Volume unit", SapDataType = RfcDataTypes.UNIT, Length = 3)]
        //public string VOLEH { get; set; }

        //[RfcEntityProperty("INCO1", Description = "Incoterms (part 1)", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        //public string INCO1 { get; set; }

        //[RfcEntityProperty("INCO2", Description = "Incoterms (part 2)", SapDataType = RfcDataTypes.CHAR, Length = 28)]
        //public string INCO2 { get; set; }

        //[RfcEntityProperty("VORAB", Description = "Advance procurement: project stock", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string VORAB { get; set; }


        //[RfcEntityProperty("KOLIF", Description = "Prior Vendor", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string KOLIF { get; set; }

        //[RfcEntityProperty("LTSNR", Description = "Vendor Subrange", SapDataType = RfcDataTypes.CHAR, Length = 6)]
        //public string LTSNR { get; set; }

        [RfcEntityProperty("PACKNO", Description = "Package number", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        public decimal PACKNO { get; set; }

        ////[RfcEntityProperty("FPLNR", Description = "Invoicing plan number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        ////public string FPLNR { get; set; }

        ////[RfcEntityProperty("GNETWR", Description = "Net Order Value in PO Currency", SapDataType = RfcDataTypes.CURR, Length = 13)]
        ////public string GNETWR { get; set; }

        ////[RfcEntityProperty("STAPO", Description = "Item is statistical", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        ////public string STAPO { get; set; }

        ////[RfcEntityProperty("UEBPO", Description = "Higher-Level Item in Purchasing Documents", SapDataType = RfcDataTypes.NUMC, Length = 5)]
        ////public string UEBPO { get; set; }

        ////[RfcEntityProperty("LEWED", Description = "Latest Possible Goods Receipt", SapDataType = RfcDataTypes.DATS, Length = 8)]
        ////public string LEWED { get; set; }

        ////[RfcEntityProperty("EMLIF", Description = "Vendor to be supplied/who is to receive delivery", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        ////public string EMLIF { get; set; }

        ////[RfcEntityProperty("LBLKZ", Description = "Subcontracting vendor", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        ////public string LBLKZ { get; set; }

        ////[RfcEntityProperty("SATNR", Description = "Cross-Plant Configurable Material", SapDataType = RfcDataTypes.CHAR, Length = 18)]
        ////public string SATNR { get; set; }

        ////[RfcEntityProperty("ATTYP", Description = "Material Category", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        ////public string ATTYP { get; set; }

        ////[RfcEntityProperty("KANBA", Description = "Kanban Indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        ////public string KANBA { get; set; }

        ////[RfcEntityProperty("ADRN2", Description = "Number of delivery address", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        ////public string ADRN2 { get; set; }

        ////[RfcEntityProperty("CUOBJ", Description = "Configuration (internal object number)", SapDataType = RfcDataTypes.NUMC, Length = 18)]
        ////public string CUOBJ { get; set; }

        ////[RfcEntityProperty("XERSY", Description = "Evaluated Receipt Settlement (ERS)", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        ////public string XERSY { get; set; }

        ////[RfcEntityProperty("EILDT", Description = "Start Date for GR-Based Settlement", SapDataType = RfcDataTypes.DATS, Length = 8)]
        ////public string EILDT { get; set; }

        ////[RfcEntityProperty("DRDAT", Description = "Last Transmission", SapDataType = RfcDataTypes.DATS, Length = 8)]
        ////public string DRDAT { get; set; }

        ////[RfcEntityProperty("DRUHR", Description = "Time", SapDataType = RfcDataTypes.TIMS, Length = 6)]
        ////public string DRUHR { get; set; }

        ////[RfcEntityProperty("DRUNR", Description = "Sequential Number", SapDataType = RfcDataTypes.NUMC, Length = 4)]
        ////public string DRUNR { get; set; }

        ////[RfcEntityProperty("AKTNR", Description = "Promotion", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        ////public string AKTNR { get; set; }

        ////[RfcEntityProperty("ABELN", Description = "Allocation Table Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        ////public string ABELN { get; set; }

        ////[RfcEntityProperty("ABELP", Description = "Item number of allocation table", SapDataType = RfcDataTypes.NUMC, Length = 5)]
        ////public string ABELP { get; set; }

        ////[RfcEntityProperty("ANZPU", Description = "Number of Points", SapDataType = RfcDataTypes.QUAN, Length = 13)]
        ////public string ANZPU { get; set; }

        ////[RfcEntityProperty("PUNEI", Description = "Points unit", SapDataType = RfcDataTypes.UNIT, Length = 3)]
        ////public string PUNEI { get; set; }

        ////[RfcEntityProperty("SAISO", Description = "Season Category", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        ////public string SAISO { get; set; }

        ////[RfcEntityProperty("SAISJ", Description = "Season Year", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        ////public string SAISJ { get; set; }

        ////[RfcEntityProperty("EBON2", Description = "Settlement Group 2 (Rebate Settlement, Purchasing)", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        ////public string EBON2 { get; set; }

        ////[RfcEntityProperty("EBON3", Description = "Settlement Group 3 (Rebate Settlement, Purchasing)", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        ////public string EBON3 { get; set; }

        ////[RfcEntityProperty("EBONF", Description = "Item Relevant to Subsequent (Period-End Rebate) Settlement", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        ////public string EBONF { get; set; }

        ////[RfcEntityProperty("MLMAA", Description = "Material ledger activated at material level", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        ////public string MLMAA { get; set; }

        ////[RfcEntityProperty("MHDRZ", Description = "Minimum Remaining Shelf Life", SapDataType = RfcDataTypes.DEC, Length = 4)]
        ////public string MHDRZ { get; set; }

        [RfcEntityProperty("ANFNR", Description = "RFQ Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string ANFNR { get; set; }

        ////[RfcEntityProperty("ANFPS", Description = "Item Number of RFQ", SapDataType = RfcDataTypes.NUMC, Length = 5)]
        ////public string ANFPS { get; set; }

        ////[RfcEntityProperty("KZKFG", Description = "Origin of Configuration", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        ////public string KZKFG { get; set; }

        ////[RfcEntityProperty("USEQU", Description = "Quota arrangement usage", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        ////public string USEQU { get; set; }

        ////[RfcEntityProperty("UMSOK", Description = "Special stock indicator for physical stock transfer", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        ////public string UMSOK { get; set; }

        [RfcEntityProperty("BANFN", Description = "Purchase requisition number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string BANFN { get; set; }

        [RfcEntityProperty("BNFPO", Description = "Item number of purchase requisition", SapDataType = RfcDataTypes.NUMERIC, Length = 5)]
        public double BNFPO { get; set; }

        //[RfcEntityProperty("MTART", Description = "Material type", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string MTART { get; set; }

        //[RfcEntityProperty("UPTYP", Description = "Subitem Category, Purchasing Document", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string UPTYP { get; set; }

        //[RfcEntityProperty("UPVOR", Description = "Subitems Exist", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string UPVOR { get; set; }

        //[RfcEntityProperty("KZWI1", Description = "Subtotal 1 from pricing procedure for condition", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string KZWI1 { get; set; }

        //[RfcEntityProperty("KZWI2", Description = "Subtotal 2 from pricing procedure for condition", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string KZWI2 { get; set; }

        //[RfcEntityProperty("KZWI3", Description = "Subtotal 3 from pricing procedure for condition", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string KZWI3 { get; set; }

        //[RfcEntityProperty("KZWI4", Description = "Subtotal 4 from pricing procedure for condition", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string KZWI4 { get; set; }

        //[RfcEntityProperty("KZWI5", Description = "Subtotal 5 from pricing procedure for condition", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string KZWI5 { get; set; }

        //[RfcEntityProperty("KZWI6", Description = "Subtotal 6 from pricing procedure for condition", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string KZWI6 { get; set; }

        //[RfcEntityProperty("SIKGR", Description = "Processing key for sub-items", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        //public string SIKGR { get; set; }

        //[RfcEntityProperty("MFZHI", Description = "Maximum Cumulative Material Go-Ahead Quantity", SapDataType = RfcDataTypes.QUAN, Length = 15)]
        //public string MFZHI { get; set; }

        //[RfcEntityProperty("FFZHI", Description = "Maximum Cumulative Production Go-Ahead Quantity", SapDataType = RfcDataTypes.QUAN, Length = 15)]
        //public string FFZHI { get; set; }

        //[RfcEntityProperty("RETPO", Description = "Returns Item", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string RETPO { get; set; }

        //[RfcEntityProperty("AUREL", Description = "Relevant to Allocation Table", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string AUREL { get; set; }

        //[RfcEntityProperty("BSGRU", Description = "Reason for Ordering", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        //public string BSGRU { get; set; }

        //[RfcEntityProperty("LFRET", Description = "Delivery Type for Returns to Vendors", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string LFRET { get; set; }

        //[RfcEntityProperty("MFRGR", Description = "Material freight group", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        //public string MFRGR { get; set; }

        //[RfcEntityProperty("NRFHG", Description = "Material qualifies for discount in kind", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string NRFHG { get; set; }

        //[RfcEntityProperty("J_1BNBM", Description = "Brazilian NCM Code", SapDataType = RfcDataTypes.CHAR, Length = 16)]
        //public string J_1BNBM { get; set; }

        //[RfcEntityProperty("J_1BMATUSE", Description = "Usage of the material", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string J_1BMATUSE { get; set; }

        //[RfcEntityProperty("J_1BMATORG", Description = "Origin of the material", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string J_1BMATORG { get; set; }

        //[RfcEntityProperty("J_1BOWNPRO", Description = "Produced in-house", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string J_1BOWNPRO { get; set; }

        //[RfcEntityProperty("J_1BINDUST", Description = "Material CFOP category", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        //public string J_1BINDUST { get; set; }

        //[RfcEntityProperty("ABUEB", Description = "Release Creation Profile", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string ABUEB { get; set; }

        //[RfcEntityProperty("NLABD", Description = "Next Forecast Delivery Schedule Transmission", SapDataType = RfcDataTypes.DATS, Length = 8)]
        //public string NLABD { get; set; }

        //[RfcEntityProperty("NFABD", Description = "Next JIT Delivery Schedule Transmission", SapDataType = RfcDataTypes.DATS, Length = 8)]
        //public string NFABD { get; set; }

        //[RfcEntityProperty("KZBWS", Description = "Valuation of Special Stock", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string KZBWS { get; set; }

        //[RfcEntityProperty("BONBA", Description = "Rebate basis 1", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //public string BONBA { get; set; }

        //[RfcEntityProperty("FABKZ", Description = "Indicator: Item Relevant to JIT Delivery Schedules", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string FABKZ { get; set; }

        //[RfcEntityProperty("J_1AINDXP", Description = "Inflation Index", SapDataType = RfcDataTypes.CHAR, Length = 5)]
        //public string J_1AINDXP { get; set; }

        //[RfcEntityProperty("J_1AIDATEP", Description = "Last day of the time period where the index value is valid", SapDataType = RfcDataTypes.DATS, Length = 8)]
        //public string J_1AIDATEP { get; set; }

        //[RfcEntityProperty("MPROF", Description = "Mfr part profile", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string MPROF { get; set; }

        //[RfcEntityProperty("EGLKZ", Description = "&quot;Outward Delivery Completed&quot; Indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string EGLKZ { get; set; }

        //[RfcEntityProperty("KZTLF", Description = "Partial Delivery at Item Level (Stock Transfer)", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string KZTLF { get; set; }

        //[RfcEntityProperty("KZFME", Description = "Units of measure usage", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string KZFME { get; set; }

        //[RfcEntityProperty("RDPRF", Description = "Rounding Profile", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string RDPRF { get; set; }

        //[RfcEntityProperty("TECHS", Description = "Parameter Variant/Standard Variant", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        //public string TECHS { get; set; }

        //[RfcEntityProperty("CHG_SRV", Description = "Configuration changed", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string CHG_SRV { get; set; }

        //[RfcEntityProperty("CHG_FPLNR", Description = "No invoice for this item although not free of charge", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string CHG_FPLNR { get; set; }

        //[RfcEntityProperty("MFRPN", Description = "Manufacturer Part Number", SapDataType = RfcDataTypes.CHAR, Length = 40)]
        //public string MFRPN { get; set; }

        //[RfcEntityProperty("MFRNR", Description = "Manufacturer number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string MFRNR { get; set; }

        //[RfcEntityProperty("EMNFR", Description = "External manufacturer code name or number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string EMNFR { get; set; }

        //[RfcEntityProperty("NOVET", Description = "Item blocked for SD delivery", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string NOVET { get; set; }

        //[RfcEntityProperty("AFNAM", Description = "Name of requisitioner/requester", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        //public string AFNAM { get; set; }

        //[RfcEntityProperty("TZONRC", Description = "Time zone of recipient location", SapDataType = RfcDataTypes.CHAR, Length = 6)]
        //public string TZONRC { get; set; }

        //[RfcEntityProperty("IPRKZ", Description = "Period Indicator for Shelf Life Expiration Date", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string IPRKZ { get; set; }

    }
}
