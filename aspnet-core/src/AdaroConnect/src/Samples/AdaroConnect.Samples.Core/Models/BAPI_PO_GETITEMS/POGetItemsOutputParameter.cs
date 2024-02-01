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
        public string STATUS { get; set; }

        [RfcEntityProperty("CREATED_ON", "CREATED_ON - RFCTYPE_DATE - 8", RfcDataTypes.CHAR, 8)]
        public string Description { get; set; }


        [RfcEntityProperty("CREATED_BY", "CREATED_BY", RfcDataTypes.CHAR, 12)]
        public string CREATED_BY { get; set; }

        [RfcEntityProperty("VENDOR", "VENDOR", RfcDataTypes.CHAR, 10)]
        public string VENDOR { get; set; }

    }


    public class PurchaseOrderItem
    {
        [RfcEntityProperty("PO_NUMBER", "PO Number", RfcDataTypes.CHAR, 10)]
        public string PurchaseOrderNo { get; set; }


        [RfcEntityProperty("PO_ITEM", "PO_ITEM", RfcDataTypes.CHAR, 5)]
        public string PO_ITEM { get; set; }


        [RfcEntityProperty("ADDRESS", "ADDRESS", RfcDataTypes.CHAR, 10)]
        public string ADDRESS { get; set; }


        [RfcEntityProperty("MATERIAL", "MATERIAL", RfcDataTypes.CHAR, 18)]
        public string MATERIAL { get; set; }


        [RfcEntityProperty("STORE_LOC", "STORE_LOC", RfcDataTypes.CHAR, 4)]
        public string STORE_LOC { get; set; }


        [RfcEntityProperty("PLANT", "PLANT", RfcDataTypes.CHAR, 4)]
        public string PLANT { get; set; }


        [RfcEntityProperty("UNIT", "UNIT", RfcDataTypes.CHAR, 3)]
        public string UNIT { get; set; }


        [RfcEntityProperty("NET_PRICE", "NET_PRICE", RfcDataTypes.DECIMAL, 12)]
        public string NET_PRICE { get; set; }


        [RfcEntityProperty("PRICE_UNIT", "PRICE_UNIT", RfcDataTypes.DECIMAL, 3)]
        public string PRICE_UNIT { get; set; }


        [RfcEntityProperty("VEND_MAT", "VEND_MAT", RfcDataTypes.CHAR, 22)]
        public string VEND_MAT { get; set; }
    }
}

