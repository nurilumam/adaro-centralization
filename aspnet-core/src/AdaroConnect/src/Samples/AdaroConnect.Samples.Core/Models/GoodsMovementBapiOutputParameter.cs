using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;
using AdaroConnect.Abstraction.Model;

namespace AdaroConnect.Application.Core.Models
{
    public class GoodsMovementBapiOutputParameter:IBapiOutput
    {
        [RfcEntityProperty("RETURN")]
        public BapiReturnParameter BapiReturn { get; set; }
        

        [RfcEntityProperty("MATERIALDOCUMENT")]
        public MaterialDocument MaterialDocument { get; set; }
    }

    public class MaterialDocument
    {
        [RfcEntityProperty("MAT_DOC", "Material document number",RfcDataTypes.CHAR,10)]
        public string DocumentNumber { get; set; }

        [RfcEntityProperty("DOC_YEAR","Material Document Year",RfcDataTypes.NUMERIC,4)]
        public int DocumentYear { get; set; }
    }
}
