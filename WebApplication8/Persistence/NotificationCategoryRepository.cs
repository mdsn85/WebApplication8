using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication8.Models;
using WebApplication8.Models.Repository;

namespace WebApplication8.Persistence
{
    public class NotificationCategoryRepository : INotificationCategoryRepository
    {

        private ApplicationDbContext context;

        public NotificationCategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IEnumerable<NotificationCategory> GetNotificationCategories()
        {
            return context.NotificationCategorys.ToList();
        }


        public NotificationCategory GetNotificationCategoryByID(int NotificationCategoryId)
        {
            return context.NotificationCategorys.Find(NotificationCategoryId);
            
        }

        public void InsertNotificationCategory(NotificationCategory NotificationCategory)
        {
            context.NotificationCategorys.Add(NotificationCategory);
            
        }


        public void UpdateNotificationCategory(NotificationCategory NotificationCategory)
        {
            context.Entry(NotificationCategory).State = System.Data.Entity.EntityState.Modified;
        }


        public void DeleteNotificationCategory(int NotificationCategoryID)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}