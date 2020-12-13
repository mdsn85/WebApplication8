
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication8.Models;

namespace WebApplication8.Models
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly ApplicationDbContext db;
        public NotificationRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<Notification1> Get(int id, bool icludeRelated = true)
        {
                return await db.Notifications.FindAsync(id);
        }

        public void  Add(Notification1 Notification)
        {
            db.Notifications.Add(Notification);
        }

        public void Delete(Notification1 Notification)
        {
            db.Notifications.Remove(Notification);
        }
    }
}
