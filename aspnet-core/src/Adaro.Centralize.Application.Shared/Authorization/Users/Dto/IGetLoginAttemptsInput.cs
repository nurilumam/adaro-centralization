using Abp.Application.Services.Dto;

namespace Adaro.Centralize.Authorization.Users.Dto
{
    public interface IGetLoginAttemptsInput: ISortedResultRequest
    {
        string Filter { get; set; }
    }
}