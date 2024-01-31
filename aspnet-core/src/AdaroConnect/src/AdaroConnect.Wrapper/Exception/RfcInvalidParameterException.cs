using AdaroConnect.Wrapper.Enumeration;

namespace AdaroConnect.Wrapper.Exception
{
    public class RfcInvalidParameterException:RfcException
    {
        public RfcInvalidParameterException(string message) 
            : base(RfcResultCodes.RFC_INVALID_PARAMETER,message)
        {
        }
    }
}
