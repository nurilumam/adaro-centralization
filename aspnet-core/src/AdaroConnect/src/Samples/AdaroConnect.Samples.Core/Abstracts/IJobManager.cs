using System.Threading.Tasks;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Abstracts
{
    public interface IJobManager: IPrintable<GetJobOutputParameter>
    {
        Task<GetJobOutputParameter> GetJobsAsync();
    }
}
