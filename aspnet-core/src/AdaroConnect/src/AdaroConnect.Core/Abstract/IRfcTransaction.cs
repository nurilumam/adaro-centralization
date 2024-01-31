using System;

namespace AdaroConnect.Core.Abstract
{
    public interface IRfcTransaction: IDisposable
    {
        IRfcTransactionFunction CreateFunction(string name);
        void SubmitTransaction();
        void ConfirmTransaction();
    }
}
