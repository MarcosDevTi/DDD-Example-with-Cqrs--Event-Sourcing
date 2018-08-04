using System.Threading.Tasks;

namespace DDDExample.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
