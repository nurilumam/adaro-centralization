using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.MultiTenancy.Accounting;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Accounting;
using Adaro.Centralize.Web.Controllers;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    public class InvoiceController : CentralizeControllerBase
    {
        private readonly IInvoiceAppService _invoiceAppService;

        public InvoiceController(IInvoiceAppService invoiceAppService)
        {
            _invoiceAppService = invoiceAppService;
        }


        [HttpGet]
        public async Task<ActionResult> Index(long paymentId)
        {
            var invoice = await _invoiceAppService.GetInvoiceInfo(new EntityDto<long>(paymentId));
            var model = new InvoiceViewModel
            {
                Invoice = invoice
            };

            return View(model);
        }
    }
}