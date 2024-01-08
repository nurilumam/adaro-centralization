using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Chat.Dto;

namespace Adaro.Centralize.Chat
{
    public interface IChatAppService : IApplicationService
    {
        GetUserChatFriendsWithSettingsOutput GetUserChatFriendsWithSettings();

        Task<ListResultDto<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input);

        Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input);
    }
}
