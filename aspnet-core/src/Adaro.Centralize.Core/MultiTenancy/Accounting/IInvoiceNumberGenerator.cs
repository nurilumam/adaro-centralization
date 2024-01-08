﻿using System.Threading.Tasks;
using Abp.Dependency;

namespace Adaro.Centralize.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}