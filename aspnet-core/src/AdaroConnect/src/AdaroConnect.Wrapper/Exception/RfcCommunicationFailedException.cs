using AdaroConnect.Wrapper.Enumeration;

namespace AdaroConnect.Wrapper.Exception
{
    public sealed class RfcCommunicationFailedException : RfcException
    {
        public RfcCommunicationFailedException(string message) 
            : base(RfcResultCodes.RFC_COMMUNICATION_FAILURE, message)
        {
        }
    }
}
