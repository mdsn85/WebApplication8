using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication8.Models.Repository
{
    interface INotificationCategoryRepository
    {

        IEnumerable<NotificationCategory> GetNotificationCategories();
        NotificationCategory GetNotificationCategoryByID(int NotificationCategoryId);
        void InsertNotificationCategory(NotificationCategory NotificationCategory);
        void DeleteNotificationCategory(int NotificationCategoryID);
        void UpdateNotificationCategory(NotificationCategory NotificationCategory);
        void Save();
    }
}
