using System.Threading.Tasks;
using AdaroConnect.Samples.Core.Models;

namespace AdaroConnect.Samples.Core.Abstracts
{
    public interface IIRequisitionManagerManager : IPrintable<IRequisitionOutputParameter>
    {
        Task<IRequisitionOutputParameter> GetRequisitionAsync();
    }
}
