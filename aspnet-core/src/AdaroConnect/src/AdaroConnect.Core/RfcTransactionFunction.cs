using System;
using System.Threading.Tasks;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Wrapper.Abstract;
using AdaroConnect.Wrapper.Enumeration;
using AdaroConnect.Wrapper.Extension;
using AdaroConnect.Wrapper.Mappers;
using AdaroConnect.Wrapper.Struct;

namespace AdaroConnect.Core
{
    internal  sealed class RfcTransactionFunction:IRfcTransactionFunction
    {
        #region Variables

        private readonly IRfcInterop _interop;
        private readonly IntPtr _transactionHandle;
        private readonly IntPtr _functionHandle;
        private bool _disposed;

        #endregion

        #region Methods

        #region Constructors

        public RfcTransactionFunction(IRfcInterop interop,IntPtr transactionHandle, IntPtr functionHandle)
        {
            _interop = interop;
            _transactionHandle = transactionHandle;
            _functionHandle = functionHandle;
        }

        #endregion

        #region IRfcFunction Implementation

        public void Invoke()
        {
            RfcResultCodes resultCode = _interop.InvokeInTransaction(_transactionHandle, funcHandle: _functionHandle, out RfcErrorInfo errorInfo);

            resultCode.ThrowOnError(errorInfo);
        }
        public async Task<bool> InvokeAsync()
        {
            RfcResultCodes resultCode;

            await Task.Run(() =>
            {
                resultCode = _interop.InvokeInTransaction(_transactionHandle, funcHandle: _functionHandle, out RfcErrorInfo errorInfo);
                resultCode.ThrowOnError(errorInfo);
            }).ConfigureAwait(false);

            return true;
        }
        public void Invoke(object input)
        {
            InputMapper.Apply(_interop, _functionHandle, input);
            Invoke();
        }
        public async Task<bool> InvokeAsync(object input)
        {
            InputMapper.Apply(_interop, _functionHandle, input);
            return await InvokeAsync();
        }
        public TOutput ReadSubmitResult<TOutput>()
        {
            return OutputMapper.Extract<TOutput>(_interop, _functionHandle);
        }
        
        #endregion


        private void Destroy()
        {
            if (_functionHandle == IntPtr.Zero)
                return;

            RfcResultCodes resultCode = _interop.DestroyFunction(_functionHandle, out RfcErrorInfo errorInfo);
            resultCode.ThrowOnError(errorInfo);
        }

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
        }

        private  void Dispose(bool disposing)
        {
            if(_disposed)
                return;

            if (disposing)
                Destroy();
            
            _disposed = true;
        }

        #endregion

        #endregion
    }
}
