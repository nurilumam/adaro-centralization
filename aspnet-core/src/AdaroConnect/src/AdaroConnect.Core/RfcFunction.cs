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
    internal sealed class RfcFunction : IRfcFunction
    {
        #region Variables

        private readonly IRfcInterop _interop;
        private readonly IntPtr _rfcConnectionHandle;
        private readonly IntPtr _functionHandle;
        private bool _disposed;

        #endregion

        #region Methods

        #region Constructors

        public RfcFunction(IRfcInterop interop, IntPtr rfcConnectionHandle, IntPtr functionHandle)
        {
            _interop = interop;
            _rfcConnectionHandle = rfcConnectionHandle;
            _functionHandle = functionHandle;
        }

        #endregion

        #region IRfcFunction Implementation

        public void Invoke()
        {
            RfcResultCodes resultCode = _interop.Invoke(_rfcConnectionHandle, funcHandle: _functionHandle, out RfcErrorInfo errorInfo);

            resultCode.ThrowOnError(errorInfo);
        }
        public async Task<bool> InvokeAsync()
        {
            RfcResultCodes resultCode;

            await Task.Run(() =>
            {
                resultCode = _interop.Invoke(_rfcConnectionHandle, funcHandle: _functionHandle, out RfcErrorInfo errorInfo);
                resultCode.ThrowOnError(errorInfo);
            });

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
        public TOutput Invoke<TOutput>()
        {
            Invoke();
            return OutputMapper.Extract<TOutput>(_interop, _functionHandle);
        }
        public async Task<TOutput> InvokeAsync<TOutput>()
        {
            await InvokeAsync();
            return OutputMapper.Extract<TOutput>(_interop, _functionHandle);
        }
        public TOutput Invoke<TOutput>(object input)
        {
            Invoke(input);
            return OutputMapper.Extract<TOutput>(_interop, _functionHandle);
        }
        public async Task<TOutput> InvokeAsync<TOutput>(object input)
        {
            await InvokeAsync(input);
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

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            
            if (disposing)
                Destroy();

            _disposed = true;
        }

        #endregion

        #endregion
    }
}
