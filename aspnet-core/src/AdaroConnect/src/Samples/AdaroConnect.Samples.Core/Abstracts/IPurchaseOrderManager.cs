using System.Collections.Generic;
using System.Threading.Tasks;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Abstracts
{
    public interface IPurchaseOrderManager : IPrintable<POGetItemsOutputParameter>
    {
        Task<POGetItemsOutputParameter> GetPurchaseOrderItems();

        Task<List<PurchasingDocumentHeader>> GetPurchaseOrder(SAPGeneralParameterModel param);
        Task<List<PurchasingDocumentItem>> GetPurchaseOrderItem(SAPGeneralParameterModel param);
        Task<List<AccountAssignmentPurchasing>> GetAccountAssignmentPurchasing();
        Task<List<ServicePackage>> GetServicePackage();
    }
}
