using System.Threading.Tasks;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Abstracts
{
    public interface IVendorManager: IPrintable<VendorBapiOutputParameter>
    {
        Task<VendorBapiOutputParameter> GetVendorsByCompanyCodeAsync(string companyCode);
    }
}
