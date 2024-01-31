using System;
using System.Collections.Generic;
using System.Text;

namespace Adaro.Centralize.Common
{
    public class DtoResponseModel
    {
        public DtoResponseModel()
        {
            IsSuccess = false;
            Message = string.Empty;
            ResponseObject = string.Empty;
            ListMessage = new StringBuilder();
        }

        private StringBuilder ListMessage { get; set; }

        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
        public string ResponseObject { get; set; }

        public void SetSuccess(string message)
        {
            IsSuccess = true;
            Message = message;
        }

        public void SetSuccess()
        {
            IsSuccess = true;
        }

        public void SetError(string message)
        {
            IsSuccess = false;
            Message = message;
        }

        public void SetError()
        {
            IsSuccess = false;
        }

        public void AddMessage(string message)
        {
            ListMessage.AppendLine(message);
            Message = ListMessage.ToString();
        }
    }
}
