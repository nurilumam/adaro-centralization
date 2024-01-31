using System.Threading.Tasks;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Abstracts
{
    public interface IMaterialSaveDataManager:IPrintable<MaterialSaveDataBapiOutputParameter>
    {
        Task<MaterialSaveDataBapiOutputParameter> CreateMaterialAsync();
    }
}
