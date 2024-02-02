using System;
using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;
using AdaroConnect.Abstraction.Model;

namespace AdaroConnect.Application.Core.Models
{

    public class POGetItemsOutputParameter : IRfcOutput
    {
        [RfcEntityProperty("RETURN")]
        public BapiReturnParameter[] BapiReturn { get; set; }


        [RfcEntityProperty("PO_HEADERS")]
        public PurchaseOrderHeader[] PurchaseOrderHeaders { get; set; }

        [RfcEntityProperty("PO_ITEMS")]
        public PurchaseOrderItem[] PurchaseOrderItems { get; set; }
    }

    
    public class PurchaseOrderHeader
    {
        [RfcEntityProperty("PO_NUMBER", "PO Number", RfcDataTypes.CHAR, 10)]
        public string PurchaseOrderNo { get; set; }

        [RfcEntityProperty("CO_CODE", "Co Code", RfcDataTypes.CHAR, 4)]
        public string COCode { get; set; }

        [RfcEntityProperty("STATUS", "STATUS", RfcDataTypes.CHAR, 1)]
        public string Status { get; set; }

        [RfcEntityProperty("CREATED_ON", "CREATED_ON - RFCTYPE_DATE - 8", RfcDataTypes.DATE_8)]
        public DateTime CreatedOn { get; set; }


        [RfcEntityProperty("CREATED_BY", "CREATED_BY", RfcDataTypes.CHAR, 12)]
        public string CreatedBy { get; set; }

        [RfcEntityProperty("VENDOR", "VENDOR", RfcDataTypes.CHAR, 10)]
        public string Vendor { get; set; }

    }


    public class PurchaseOrderItem
    {
        [RfcEntityProperty("PO_NUMBER", "PO Number", RfcDataTypes.CHAR, 10)]
        public string PurchaseOrderNo { get; set; }


        [RfcEntityProperty("PO_ITEM", "PO_ITEM", RfcDataTypes.CHAR, 5)]
        public string PO_ITEM { get; set; }


        [RfcEntityProperty("ADDRESS", "ADDRESS", RfcDataTypes.CHAR, 10)]
        public string Address { get; set; }


        [RfcEntityProperty("MATERIAL", "MATERIAL", RfcDataTypes.CHAR, 18)]
        public string Material { get; set; }


        [RfcEntityProperty("STORE_LOC", "STORE_LOC", RfcDataTypes.CHAR, 4)]
        public string StorageLocation { get; set; }


        [RfcEntityProperty("PLANT", "PLANT", RfcDataTypes.CHAR, 4)]
        public string Plant { get; set; }


        [RfcEntityProperty("UNIT", "UNIT", RfcDataTypes.CHAR, 3)]
        public string Unit { get; set; }


        [RfcEntityProperty("NET_PRICE", "NET_PRICE", RfcDataTypes.DECIMAL, 12)]
        public string NetPrice { get; set; }


        [RfcEntityProperty("PRICE_UNIT", "PRICE_UNIT", RfcDataTypes.DECIMAL, 3)]
        public string PriceUnit { get; set; }


        [RfcEntityProperty("VEND_MAT", "VEND_MAT", RfcDataTypes.CHAR, 22)]
        public string VendorMaterial { get; set; }
    }
}

