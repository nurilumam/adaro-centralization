using System.Threading.Tasks;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Abstracts
{
    public interface IBillOfMaterialManager:IPrintable<BomOutputParameter>,IPrintable<Topmat>
    {
        Task<BomOutputParameter> GetBillOfMaterialAsync(string materialCode, string plantCode, string alias = null);
    }
}
