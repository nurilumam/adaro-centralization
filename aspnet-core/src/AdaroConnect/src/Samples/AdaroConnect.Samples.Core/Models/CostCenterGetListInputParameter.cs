using System;
using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;

namespace AdaroConnect.Application.Core.Models
{

    public class CostCenterGetListInputParameter : IRfcInput
    {
        [RfcEntityProperty("CONTROLLINGAREA", "Controlling Area", RfcDataTypes.CHAR, 4)]
        public string ControlingArea { get; set; }


        [RfcEntityProperty("COSTCENTER_FROM", "Cost center (from value)", RfcDataTypes.CHAR, 10)]
        public string CostCenter { get; set; }

        [RfcEntityProperty("COSTCENTER_TO", "Cost Center (To Value)", RfcDataTypes.CHAR, 10)]
        public string CostCenterTo { get; set; }





        [RfcEntityProperty("COMPANYCODE_FROM", "Company Code (From Value)", RfcDataTypes.CHAR, 4)]
        public string CompanyCodeFrom { get; set; }

        [RfcEntityProperty("COMPANYCODE_TO", "Company Code (To Value)", RfcDataTypes.CHAR, 4)]
        public string CompanyCodeTo { get; set; }



        [RfcEntityProperty("PERSON_IN_CHARGE_FROM", "Person Responsible (From Value)", RfcDataTypes.CHAR, 18)]
        public string PersonInCharge { get; set; }

        [RfcEntityProperty("PERSON_IN_CHARGE_TO", "Person Responsible (To Value)", RfcDataTypes.CHAR, 18)]
        public string PersonInChargeTo { get; set; }



        [RfcEntityProperty("DATE_FROM", "Selection Date: Key Date", RfcDataTypes.DATE_8)]
        public string DateFrom { get; set; }

        [RfcEntityProperty("DATE_TO", "To date for the selection.", RfcDataTypes.DATE_8)]
        public string DateTo { get; set; }



        [RfcEntityProperty("COSTCENTERGROUP", "Cost center group", RfcDataTypes.CHAR, 15)]
        public string CostCenterGroup { get; set; }



        [RfcEntityProperty("PERSON_IN_CHARGE_USER_FROM", "User Responsible (From-Value)", RfcDataTypes.CHAR, 12)]
        public string PersonInChargeUser { get; set; }


        [RfcEntityProperty("PERSON_IN_CHARGE_USER_TO", "User Responsible (To-Value)", RfcDataTypes.CHAR, 12)]
        public string PersonInChargeUserTo { get; set; }


        [RfcEntityProperty("COSTCENTERLIST", "Interface Structure: Cost Centers - Keys and Texts")]
        public CostCenterItem[] CostCenterList { get; set; }

    }


}
