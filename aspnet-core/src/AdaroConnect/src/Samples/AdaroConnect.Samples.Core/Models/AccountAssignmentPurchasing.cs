using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;

namespace AdaroConnect.Application.Core.Models
{
    [RfcEntity("EKKN", Description = "Account Assignment in Purchasing Document Table")]
    public class AccountAssignmentPurchasing : ISapTable
    {
        [RfcEntityProperty("MANDT", Description = "Client", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        public string MANDT { get; set; }

        [RfcEntityProperty("EBELN", Description = "Purchasing Document Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string EBELN { get; set; }

        [RfcEntityProperty("EBELP", Description = "Item Number of Purchasing Document", SapDataType = RfcDataTypes.NUMERIC, Length = 5)]
        public double EBELP { get; set; }

        //        [RfcEntityProperty("ZEKKN", Description = "Sequential Number of Account Assignment", SapDataType = RfcDataTypes.NUMC, Length = 2)]
        //        public string ZEKKN { get; set; }

        //        [RfcEntityProperty("LOEKZ", Description = "Deletion Indicator: Purchasing Document Account Assignment", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //        public string LOEKZ { get; set; }

        //        [RfcEntityProperty("AEDAT", Description = "Date on which the record was created", SapDataType = RfcDataTypes.DATS, Length = 8)]
        //        public string AEDAT { get; set; }

        //        [RfcEntityProperty("KFLAG", Description = "Change flag: Purchasing (currently not used)", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //        public string KFLAG { get; set; }

        //        [RfcEntityProperty("MENGE", Description = "Quantity", SapDataType = RfcDataTypes.QUAN, Length = 13)]
        //        public string MENGE { get; set; }

        //        [RfcEntityProperty("VPROZ", Description = "Distribution percentage in the case of multiple acct assgt", SapDataType = RfcDataTypes.DEC, Length = 3)]
        //        public string VPROZ { get; set; }

        //        [RfcEntityProperty("NETWR", Description = "Net Order Value in PO Currency", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //        public string NETWR { get; set; }

        //        [RfcEntityProperty("SAKTO", Description = "G/L Account Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //        public string SAKTO { get; set; }

        //        [RfcEntityProperty("GSBER", Description = "Business Area", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //        public string GSBER { get; set; }

        [RfcEntityProperty("KOSTL", Description = "Cost Center", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string KOSTL { get; set; }

        //        [RfcEntityProperty("PROJN", Description = "Old: Project number : No longer used --&gt; PS_POSNR", SapDataType = RfcDataTypes.CHAR, Length = 16)]
        //        public string PROJN { get; set; }

        //        [RfcEntityProperty("VBELN", Description = "Sales and Distribution Document Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //        public string VBELN { get; set; }

        //        [RfcEntityProperty("VBELP", Description = "Sales Document Item", SapDataType = RfcDataTypes.NUMC, Length = 6)]
        //        public string VBELP { get; set; }

        //        [RfcEntityProperty("VETEN", Description = "Schedule line", SapDataType = RfcDataTypes.NUMC, Length = 4)]
        //        public string VETEN { get; set; }

        //        [RfcEntityProperty("KZBRB", Description = "Gross requirements indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //        public string KZBRB { get; set; }

        [RfcEntityProperty("ANLN1", Description = "Main Asset Number", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        public string ANLN1 { get; set; }

        //        [RfcEntityProperty("ANLN2", Description = "Asset Subnumber", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //        public string ANLN2 { get; set; }

        //        [RfcEntityProperty("AUFNR", Description = "Order Number", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        //        public string AUFNR { get; set; }

        //        [RfcEntityProperty("WEMPF", Description = "Goods recipient", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        //        public string WEMPF { get; set; }

        //        [RfcEntityProperty("ABLAD", Description = "Unloading Point", SapDataType = RfcDataTypes.CHAR, Length = 25)]
        //        public string ABLAD { get; set; }

        [RfcEntityProperty("KOKRS", Description = "Controlling Area", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string KOKRS { get; set; }

        //        [RfcEntityProperty("XBKST", Description = "Posting to cost center?", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //        public string XBKST { get; set; }

        //        [RfcEntityProperty("XBAUF", Description = "Post To Order", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //        public string XBAUF { get; set; }

        //        [RfcEntityProperty("XBPRO", Description = "Post to project", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //        public string XBPRO { get; set; }

        //        [RfcEntityProperty("EREKZ", Description = "Final Invoice Indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //        public string EREKZ { get; set; }

        //        [RfcEntityProperty("KSTRG", Description = "Cost Object", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        //        public string KSTRG { get; set; }

        //        [RfcEntityProperty("PAOBJNR", Description = "Profitability Segment Number (CO-PA)", SapDataType = RfcDataTypes.NUMC, Length = 10)]
        //        public string PAOBJNR { get; set; }

        //        [RfcEntityProperty("PRCTR", Description = "Profit Center", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //        public string PRCTR { get; set; }

        [RfcEntityProperty("PS_PSP_PNR", Description = "Work Breakdown Structure Element (WBS Element)", SapDataType = RfcDataTypes.NUMERIC, Length = 8)]
        public double PS_PSP_PNR { get; set; }

        //        [RfcEntityProperty("NPLNR", Description = "Network Number for Account Assignment", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        //        public string NPLNR { get; set; }

        //        [RfcEntityProperty("AUFPL", Description = "Routing number of operations in the order", SapDataType = RfcDataTypes.NUMC, Length = 10)]
        //        public string AUFPL { get; set; }

        //        [RfcEntityProperty("IMKEY", Description = "Internal Key for Real Estate Object", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        //        public string IMKEY { get; set; }

        //        [RfcEntityProperty("APLZL", Description = "Internal counter", SapDataType = RfcDataTypes.NUMC, Length = 8)]
        //        public string APLZL { get; set; }

        //        [RfcEntityProperty("VPTNR", Description = "Partner account number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //        public string VPTNR { get; set; }

        //        [RfcEntityProperty("FIPOS", Description = "Commitment Item", SapDataType = RfcDataTypes.CHAR, Length = 14)]
        //        public string FIPOS { get; set; }

        //        [RfcEntityProperty("RECID", Description = "Recovery Indicator", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        //        public string RECID { get; set; }

        //        [RfcEntityProperty(".INCLUDE", Description = "", SapDataType = RfcDataTypes., Length = 0)]
        //        public string .INCLUDE { get; set; }

        [RfcEntityProperty("FISTL", Description = "Funds Center", SapDataType = RfcDataTypes.CHAR, Length = 16)]
        public string FISTL { get; set; }

        //    [RfcEntityProperty("GEBER", Description = "Fund", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //    public string GEBER { get; set; }

        //    [RfcEntityProperty("FKBER", Description = "Functional Area", SapDataType = RfcDataTypes.CHAR, Length = 16)]
        //    public string FKBER { get; set; }

        //    [RfcEntityProperty("DABRZ", Description = "Reference date for settlement", SapDataType = RfcDataTypes.DATS, Length = 8)]
        //    public string DABRZ { get; set; }

        //    [RfcEntityProperty("AUFPL_ORD", Description = "Routing number of operations in the order", SapDataType = RfcDataTypes.NUMC, Length = 10)]
        //    public string AUFPL_ORD { get; set; }

        //    [RfcEntityProperty("APLZL_ORD", Description = "General counter for order", SapDataType = RfcDataTypes.NUMC, Length = 8)]
        //    public string APLZL_ORD { get; set; }

        //    [RfcEntityProperty("MWSKZ", Description = "Tax on sales/purchases code", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        //    public string MWSKZ { get; set; }

        //    [RfcEntityProperty("TXJCD", Description = "Tax Jurisdiction", SapDataType = RfcDataTypes.CHAR, Length = 15)]
        //    public string TXJCD { get; set; }

        //    [RfcEntityProperty("NAVNW", Description = "Non-deductible input tax", SapDataType = RfcDataTypes.CURR, Length = 13)]
        //    public string NAVNW { get; set; }

        //    [RfcEntityProperty(".INCLUDE", Description = "Include for the Reduction of Funds Reservations (FM)", SapDataType = RfcDataTypes., Length = 0)]
        //    public string .INCLUDE { get; set; }

        //[RfcEntityProperty("KBLNR", Description = "Document Number for Earmarked Funds", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string KBLNR { get; set; }

        //[RfcEntityProperty("KBLPOS", Description = "Earmarked Funds: Document Item", SapDataType = RfcDataTypes.NUMC, Length = 3)]
        //public string KBLPOS { get; set; }
    }
}
