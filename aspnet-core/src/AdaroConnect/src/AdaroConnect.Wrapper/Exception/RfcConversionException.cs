using AdaroConnect.Wrapper.Enumeration;

namespace AdaroConnect.Wrapper.Exception
{
    public class RfcConversionException : RfcException
    {
        public RfcConversionException(string message) 
            : base(RfcResultCodes.RFC_CONVERSION_FAILURE, message)
        {
        }
    }
}
