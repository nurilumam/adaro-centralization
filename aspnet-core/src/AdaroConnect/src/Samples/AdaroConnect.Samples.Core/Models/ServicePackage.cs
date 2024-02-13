using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;

namespace AdaroConnect.Application.Core.Models
{
    [RfcEntity("ESLL", Description = "Lines of Service Package Table")]
    public class ServicePackage : ISapTable
    {
        [RfcEntityProperty("MANDT", Description = "Client", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        public string MANDT { get; set; }

        [RfcEntityProperty("PACKNO", Description = "Package number", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        public decimal PACKNO { get; set; }

        [RfcEntityProperty("INTROW", Description = "Line Number", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        public decimal INTROW { get; set; }

        [RfcEntityProperty("EXTROW", Description = "Line Number", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        public decimal EXTROW { get; set; }

        [RfcEntityProperty("DEL", Description = "Deletion Indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string DEL { get; set; }

        [RfcEntityProperty("SRVPOS", Description = "Activity Number", SapDataType = RfcDataTypes.CHAR, Length = 18)]
        public string SRVPOS { get; set; }

        [RfcEntityProperty("RANG", Description = "Hierarchy level of group", SapDataType = RfcDataTypes.INTEGER, Length = 3)]
        public int RANG { get; set; }

        [RfcEntityProperty("EXTGROUP", Description = "Outline Level", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        public string EXTGROUP { get; set; }

        [RfcEntityProperty("PACKAGE", Description = "Service Assignment", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string PACKAGE { get; set; }

        [RfcEntityProperty("SUB_PACKNO", Description = "Subpackage number", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        public decimal SUB_PACKNO { get; set; }

        [RfcEntityProperty("LBNUM", Description = "Short Description of Service Type", SapDataType = RfcDataTypes.CHAR, Length = 3)]
        public string LBNUM { get; set; }

        [RfcEntityProperty("AUSGB", Description = "Edition of Service Type", SapDataType = RfcDataTypes.INTEGER, Length = 4)]
        public int AUSGB { get; set; }

        [RfcEntityProperty("STLVPOS", Description = "Standard Service Catalog Item", SapDataType = RfcDataTypes.CHAR, Length = 18)]
        public string STLVPOS { get; set; }

        [RfcEntityProperty("EXTSRVNO", Description = "Vendor's Service Number", SapDataType = RfcDataTypes.CHAR, Length = 18)]
        public string EXTSRVNO { get; set; }

        [RfcEntityProperty("MENGE", Description = "Quantity with Sign", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        public double MENGE { get; set; }

        [RfcEntityProperty("MEINS", Description = "Base Unit of Measure", SapDataType = RfcDataTypes.UNIT, Length = 3)]
        public string MEINS { get; set; }

        [RfcEntityProperty("UEBTO", Description = "Overfulfillment Tolerance", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 3)]
        public decimal UEBTO { get; set; }

        [RfcEntityProperty("UEBTK", Description = "Unlimited Overfulfillment", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string UEBTK { get; set; }

        [RfcEntityProperty("WITH_LIM", Description = "Also Search in Limits", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string WITH_LIM { get; set; }

        [RfcEntityProperty("SPINF", Description = "Update Conditions", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string SPINF { get; set; }

        [RfcEntityProperty("PEINH", Description = "Price unit", SapDataType = RfcDataTypes.DECIMAL_COMMAND_SIGN, Length = 5)]
        public decimal PEINH { get; set; }

        [RfcEntityProperty("BRTWR", Description = "Gross Price", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        public decimal BRTWR { get; set; }

        [RfcEntityProperty("NETWR", Description = "Net Value of Item", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        public decimal NETWR { get; set; }

        [RfcEntityProperty("FROMPOS", Description = "Lower Limit", SapDataType = RfcDataTypes.CHAR, Length = 6)]
        public string FROMPOS { get; set; }

        [RfcEntityProperty("TOPOS", Description = "Upper Limit", SapDataType = RfcDataTypes.CHAR, Length = 6)]
        public string TOPOS { get; set; }

        [RfcEntityProperty("KTEXT1", Description = "Short Text", SapDataType = RfcDataTypes.CHAR, Length = 40)]
        public string KTEXT1 { get; set; }

        [RfcEntityProperty("VRTKZ", Description = "Distribution indicator for multiple account assignment", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string VRTKZ { get; set; }

        [RfcEntityProperty("TWRKZ", Description = "Partial invoice indicator", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        public string TWRKZ { get; set; }

        [RfcEntityProperty("PERNR", Description = "Personnel Number", SapDataType = RfcDataTypes.NUMERIC, Length = 8)]
        public double PERNR { get; set; }

        [RfcEntityProperty("MOLGA", Description = "Country Grouping", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        public string MOLGA { get; set; }

        [RfcEntityProperty("LGART", Description = "Wage Type", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        public string LGART { get; set; }

        [RfcEntityProperty("LGTXT", Description = "Wage Type Long Text", SapDataType = RfcDataTypes.CHAR, Length = 25)]
        public string LGTXT { get; set; }

        //[RfcEntityProperty("STELL", Description = "Job", SapDataType = RfcDataTypes.NUMERIC, Length = 8)]
        //public double STELL { get; set; }

        //[RfcEntityProperty("IFTNR", Description = "Sequence Number for CO/MM-SRV Interface Tables", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public double IFTNR { get; set; }

        [RfcEntityProperty("BUDAT", Description = "Posting Date in the Document", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        public string BUDAT { get; set; }

        [RfcEntityProperty("INSDT", Description = "Date on Which This Record was Stored in the Table", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        public string INSDT { get; set; }

        //[RfcEntityProperty("PLN_PACKNO", Description = "Source package number", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal PLN_PACKNO { get; set; }

        //[RfcEntityProperty("PLN_INTROW", Description = "Entry: Planned package line", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal PLN_INTROW { get; set; }

        //[RfcEntityProperty("KNT_PACKNO", Description = "Entry: Unplanned from contract", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal KNT_PACKNO { get; set; }

        //[RfcEntityProperty("KNT_INTROW", Description = "Entry: Unplanned from contract", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal KNT_INTROW { get; set; }

        //[RfcEntityProperty("TMP_PACKNO", Description = "Entry: Unplanned service from model specifications", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal TMP_PACKNO { get; set; }

        //[RfcEntityProperty("TMP_INTROW", Description = "Entry: Unplanned service from model specifications", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal TMP_INTROW { get; set; }

        //[RfcEntityProperty("STLV_LIM", Description = "Service line refers to standard service catalog limits", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string STLV_LIM { get; set; }

        //[RfcEntityProperty("LIMIT_ROW", Description = "Entry: Unplanned, limit line", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal LIMIT_ROW { get; set; }

        //[RfcEntityProperty("ACT_MENGE", Description = "Purchase Order: Entered Quantity", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double ACT_MENGE { get; set; }

        //[RfcEntityProperty("ACT_WERT", Description = "Entered Value", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        //public decimal ACT_WERT { get; set; }

        //[RfcEntityProperty("KNT_WERT", Description = "Contract: Value Released (via Release Orders)", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        //public decimal KNT_WERT { get; set; }

        //[RfcEntityProperty("KNT_MENGE", Description = "Contract: Quantity Released (by Issue of Release Orders)", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double KNT_MENGE { get; set; }

        //[RfcEntityProperty("ZIELWERT", Description = "Target Value", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        //public decimal ZIELWERT { get; set; }

        //[RfcEntityProperty("UNG_WERT", Description = "Contract: Unplanned Released Value", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        //public decimal UNG_WERT { get; set; }

        //[RfcEntityProperty("UNG_MENGE", Description = "Contract: Unplanned Released Quantity", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double UNG_MENGE { get; set; }

        //[RfcEntityProperty("ALT_INTROW", Description = "Alternatives: Reference to basic item", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal ALT_INTROW { get; set; }

        //[RfcEntityProperty("BASIC", Description = "Basic Line", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string BASIC { get; set; }

        //[RfcEntityProperty("ALTERNAT", Description = "Alternative Line", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string ALTERNAT { get; set; }

        //[RfcEntityProperty("BIDDER", Description = "Bidder's Line", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string BIDDER { get; set; }

        //[RfcEntityProperty("SUPPLE", Description = "Supplementary Line", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string SUPPLE { get; set; }

        //[RfcEntityProperty("FREEQTY", Description = "Line with Open Quantity", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string FREEQTY { get; set; }

        //[RfcEntityProperty("INFORM", Description = "Informatory Line", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string INFORM { get; set; }

        //[RfcEntityProperty("PAUSCH", Description = "Blanket Line", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string PAUSCH { get; set; }

        //[RfcEntityProperty("EVENTUAL", Description = "Contingency Line", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string EVENTUAL { get; set; }

        //[RfcEntityProperty("MWSKZ", Description = "Tax on sales/purchases code", SapDataType = RfcDataTypes.CHAR, Length = 2)]
        //public string MWSKZ { get; set; }

        //[RfcEntityProperty("TXJCD", Description = "Tax Jurisdiction", SapDataType = RfcDataTypes.CHAR, Length = 15)]
        //public string TXJCD { get; set; }

        //[RfcEntityProperty("PRS_CHG", Description = "Price Change in Entry Sheet", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string PRS_CHG { get; set; }

        //[RfcEntityProperty("MATKL", Description = "Material Group", SapDataType = RfcDataTypes.CHAR, Length = 9)]
        //public string MATKL { get; set; }

        //[RfcEntityProperty("TBTWR", Description = "Gross Price", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        //public decimal TBTWR { get; set; }

        //[RfcEntityProperty("NAVNW", Description = "Non-deductible input tax", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        //public decimal NAVNW { get; set; }

        //[RfcEntityProperty("BASWR", Description = "Tax base amount", SapDataType = RfcDataTypes.DECIMAL, Length = 11)]
        //public decimal BASWR { get; set; }

        //[RfcEntityProperty("KKNUMV", Description = "Number of the document condition", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string KKNUMV { get; set; }

        //[RfcEntityProperty("IWEIN", Description = "Unit for Work", SapDataType = RfcDataTypes.UNIT, Length = 3)]
        //public string IWEIN { get; set; }

        //[RfcEntityProperty("INT_WORK", Description = "Internal Work", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 8)]
        //public double INT_WORK { get; set; }

        //[RfcEntityProperty("EXTERNALID", Description = "SRM Reference Key", SapDataType = RfcDataTypes.CHAR, Length = 40)]
        //public string EXTERNALID { get; set; }

        //[RfcEntityProperty("KSTAR", Description = "Cost Element", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string KSTAR { get; set; }

        //[RfcEntityProperty("ACT_WORK", Description = "Internal Work", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 8)]
        //public double ACT_WORK { get; set; }

        //[RfcEntityProperty("MAPNO", Description = "Mapping Field f. PACKNO, INTROW at Item Level for Commitment", SapDataType = RfcDataTypes.NUMERIC, Length = 4)]
        //public decimal MAPNO { get; set; }

        //[RfcEntityProperty("SRVMAPKEY", Description = "Item Key for eSOA Messages", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal SRVMAPKEY { get; set; }

        [RfcEntityProperty("SDATE", Description = "Date", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        public string SDATE { get; set; }

        //[RfcEntityProperty("BEGTIME", Description = "Start Time", SapDataType = RfcDataTypes.TIME, Length = 6)]
        //public string BEGTIME { get; set; }

        //[RfcEntityProperty("ENDTIME", Description = "End Time", SapDataType = RfcDataTypes.TIME, Length = 6)]
        //public string ENDTIME { get; set; }

        //[RfcEntityProperty("PERSEXT", Description = "External Personnel Number", SapDataType = RfcDataTypes.CHAR, Length = 40)]
        //public string PERSEXT { get; set; }

        //[RfcEntityProperty("CATSCOUNTE", Description = "Counter for Records in Time Recording", SapDataType = RfcDataTypes.CHAR, Length = 12)]
        //public string CATSCOUNTE { get; set; }

        //[RfcEntityProperty("STOKZ", Description = "Indicator: Document Has Been Reversed", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string STOKZ { get; set; }

        [RfcEntityProperty("BELNR", Description = "Document number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        public string BELNR { get; set; }

        //[RfcEntityProperty("FORMELNR", Description = "Formula Number", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string FORMELNR { get; set; }

        //[RfcEntityProperty("FRMVAL1", Description = "Formula Value", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double FRMVAL1 { get; set; }

        //[RfcEntityProperty("FRMVAL2", Description = "Formula Value", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double FRMVAL2 { get; set; }

        //[RfcEntityProperty("FRMVAL3", Description = "Formula Value", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double FRMVAL3 { get; set; }

        //[RfcEntityProperty("FRMVAL4", Description = "Formula Value", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double FRMVAL4 { get; set; }

        //[RfcEntityProperty("FRMVAL5", Description = "Formula Value", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double FRMVAL5 { get; set; }

        //[RfcEntityProperty("USERF1_NUM", Description = "User-Defined Field", SapDataType = RfcDataTypes.NUMERIC, Length = 10)]
        //public decimal USERF1_NUM { get; set; }

        //[RfcEntityProperty("USERF2_NUM", Description = "User-Defined Field", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double USERF2_NUM { get; set; }

        //[RfcEntityProperty("USERF1_TXT", Description = "User-Defined Field", SapDataType = RfcDataTypes.CHAR, Length = 40)]
        //public string USERF1_TXT { get; set; }

        //[RfcEntityProperty("USERF2_TXT", Description = "User-Defined Field", SapDataType = RfcDataTypes.CHAR, Length = 10)]
        //public string USERF2_TXT { get; set; }

        //[RfcEntityProperty("KNOBJ", Description = "Number of Object with Assigned Dependencies", SapDataType = RfcDataTypes.NUMERIC, Length = 18)]
        //public decimal KNOBJ { get; set; }

        //[RfcEntityProperty("CHGTEXT", Description = "Short Text Change Allowed", SapDataType = RfcDataTypes.CHAR, Length = 1)]
        //public string CHGTEXT { get; set; }

        ////[RfcEntityProperty(".INCLUDE", Description = "", SapDataType = RfcDataTypes. )]
        ////public string .INCLUDE { get;
        ////set; }

        ////[RfcEntityProperty(".INCLUDE", Description = "Fields for Unit Costing", SapDataType = RfcDataTypes. )]
        ////public string .INCLUDE { get;
        ////set; }

        //[RfcEntityProperty("KALNR", Description = "Cost Estimate Number for Cost Est. w/o Qty Structure", SapDataType = RfcDataTypes.NUMERIC, Length = 12)]
        //public decimal KALNR { get; set; }

        //[RfcEntityProperty("KLVAR", Description = "Costing Variant", SapDataType = RfcDataTypes.CHAR, Length = 4)]
        //public string KLVAR { get; set; }

        //[RfcEntityProperty("EXTREFKEY", Description = "External Reference Key for Service", SapDataType = RfcDataTypes.CHAR, Length = 40)]
        //public string EXTREFKEY { get; set; }

        //[RfcEntityProperty("INV_MENGE", Description = "Purchase Order: Quantity Entered from the Invoice", SapDataType = RfcDataTypes.QUAN_DOUBLE, Length = 13)]
        //public double INV_MENGE { get; set; }


        //[RfcEntityProperty("PER_SDATE", Description = "Period of Performance Start Date", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        //public string PER_SDATE { get; set; }

        //[RfcEntityProperty("PER_EDATE", Description = "Period of Performance End Date", SapDataType = RfcDataTypes.CHAR, Length = 8)]
        //public string PER_EDATE { get; set; }
    }
}
