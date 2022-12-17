using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.Notification;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Service
{
    public interface INotificationService
    {
        Task<ResultResponse> SendAsync(NotificationSendDTO obj);
    }
}
