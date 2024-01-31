using System.Threading.Tasks;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Abstracts
{
    public interface ICostCenterManager : IPrintable<CostCenterGetListOutputParameter>
    {
        Task<CostCenterGetListOutputParameter> GetCostCenterAsync();
    }
}
