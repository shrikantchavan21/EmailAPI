using EmailAPI.Models;
using System.Threading.Tasks;

namespace EmailAPI.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(WelcomeRequest request);
    }
}
