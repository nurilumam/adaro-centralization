using System.Threading.Tasks;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Abstracts
{
    public interface IPurchaseOrderManager : IPrintable<POGetItemsOutputParameter>
    {
        Task<POGetItemsOutputParameter> GetPurchaseOrderItems();
    }
}
