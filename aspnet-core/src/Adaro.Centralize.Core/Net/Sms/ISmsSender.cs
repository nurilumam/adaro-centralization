using System.Threading.Tasks;

namespace Adaro.Centralize.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}