using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
