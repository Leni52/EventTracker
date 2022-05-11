using System.Threading.Tasks;
using EventTracker.BLL.Entities;

namespace EventTracker.BLL.Interfaces
{
    public interface IMailService
    {
        Task SendMail(MailRequest mailRequest);
    }
}