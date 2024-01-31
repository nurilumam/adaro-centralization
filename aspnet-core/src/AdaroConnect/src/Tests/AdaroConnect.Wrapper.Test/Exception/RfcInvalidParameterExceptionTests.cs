using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaroConnect.Wrapper.Enumeration;
using AdaroConnect.Wrapper.Exception;

namespace AdaroConnect.Wrapper.Test.Exception
{
    [TestClass]
    [TestCategory("UnitTest")]
    public sealed class RfcInvalidParameterExceptionTests
    {
        [TestMethod]
        public void Constructor_Message_ShouldSetMessageAndSetResultCode()
        {
            var exception = new RfcInvalidParameterException("Test message");

            exception.Message.Should().Be("SAP RFC Error: RFC_INVALID_PARAMETER with message: Test message");
            exception.ResultCode.Should().Be(RfcResultCodes.RFC_INVALID_PARAMETER);
        }

        [TestMethod]
        public void Constructor_NullMessage_ShouldSetFixedMessageAndSetResultCode()
        {
            var exception = new RfcInvalidParameterException(null);

            exception.Message.Should().Be("SAP RFC Error: RFC_INVALID_PARAMETER");
            exception.ResultCode.Should().Be(RfcResultCodes.RFC_INVALID_PARAMETER);
        }
    }
}
