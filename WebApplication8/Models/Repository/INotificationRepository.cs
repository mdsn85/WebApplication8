using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication8.Models
{
    public interface INotificationRepository
    {
        void Add(Notification1 Notification);
        void Delete(Notification1 Notification);
        Task<Notification1> Get(int id, bool icludeRelated = true);

    }
}
