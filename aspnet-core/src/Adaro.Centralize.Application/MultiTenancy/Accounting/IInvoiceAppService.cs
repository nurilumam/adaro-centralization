using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Adaro.Centralize.MultiTenancy.Accounting.Dto;

namespace Adaro.Centralize.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
