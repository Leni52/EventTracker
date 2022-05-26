using System.Threading.Tasks;
using EventTracker.BLL.Entities;
using EventTracker.BLL.Models;

namespace EventTracker.BLL.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}