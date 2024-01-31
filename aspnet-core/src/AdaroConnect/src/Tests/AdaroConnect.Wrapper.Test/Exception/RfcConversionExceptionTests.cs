using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaroConnect.Wrapper.Enumeration;
using AdaroConnect.Wrapper.Exception;

namespace AdaroConnect.Wrapper.Test.Exception
{
    [TestClass]
    [TestCategory("UnitTest")]
    public sealed class RfcConversionExceptionTests
    {
        [TestMethod]
        public void Constructor_Message_ShouldSetMessageAndSetResultCode()
        {
            var exception = new RfcConversionException("Test message");

            exception.Message.Should().Be("SAP RFC Error: RFC_CONVERSION_FAILURE with message: Test message");
            exception.ResultCode.Should().Be(RfcResultCodes.RFC_CONVERSION_FAILURE);
        }

        [TestMethod]
        public void Constructor_NullMessage_ShouldSetFixedMessageAndSetResultCode()
        {
            var exception = new RfcConversionException(null);

            exception.Message.Should().Be("SAP RFC Error: RFC_CONVERSION_FAILURE");
            exception.ResultCode.Should().Be(RfcResultCodes.RFC_CONVERSION_FAILURE);
        }
    }
}
