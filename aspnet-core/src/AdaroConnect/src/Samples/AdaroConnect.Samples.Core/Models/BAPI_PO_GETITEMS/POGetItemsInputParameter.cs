using System;
using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;

namespace AdaroConnect.Application.Core.Models
{

    public class POGetItemsInputParameter : IRfcInput
    {
        [RfcEntityProperty("PURCH_ORG", "PURCH_ORG", RfcDataTypes.CHAR, 4)]
        public string PURCH_ORG { get; set; }

        [RfcEntityProperty("WITH_PO_HEADERS", "WITH_PO_HEADERS", RfcDataTypes.CHAR, 1)]
        public string WITH_PO_HEADERS { get; set; }

    }


}
