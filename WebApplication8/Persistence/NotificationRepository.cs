
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication8.Hubs;
using WebApplication8.Models;

namespace WebApplication8.Models
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly ApplicationDbContext db;
        NotificationHub objNotifHub = new NotificationHub();

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


        //                NotificationCategory cat = db.NotificationCategorys.Where(c => c.Name == category).FirstOrDefault();

        //objNotif.NotificationCategoryId = db.NotificationCategorys.Where(c => c.Name == category).FirstOrDefault().NotificationCategoryId;
        public async Task CreateNotificationAsync(int id, string category)
        {
            try
            {
                List<string> users = new List<string>();

                Notification1 objNotif = new Notification1();

                //get all category
                List<int> catids = db.NotificationCategorys.Where(c => c.Name == category).Select(c => c.NotificationCategoryId).ToList();



                foreach (var catid in catids)
                {
                    //NotificationCategory cat = db.NotificationCategorys.Include(rr.NotificationCatUser).Where(c => c.NotificationCategoryId == catid).FirstOrDefault();
                     string u = db.NotificationCatUsers.Where(ux => ux.NotificationCategoryId == catid).FirstOrDefault().UserId;
                    objNotif.NotificationCategoryId = catid;
                    objNotif.ObjectId = id;
                    objNotif.NotificationDate = DateTime.Now;

                    db.Configuration.ProxyCreationEnabled = false;
                    db.Notifications.Add(objNotif);
                    db.SaveChanges();

                    users.Add(u);

                }

                SendNoficationAsync(users);
                

            }
            catch (Exception e)
            {
                //log.Error(" ERROR mylog - Error while send notification for project " + project.ProjectId + ":" + e.Message + " , stacktrace:" + e.StackTrace);
            }
        }






        async Task SendNoficationAsync(List<string> SentTos)
        {

            // string[] SentTo = note.NotificationCategory.NotificationCatUser.Select(n => n.UserId).ToArray();

            foreach (string SentTo in SentTos)
            {

                var currentUser = db.Users.Where(u => u.Id == SentTo).FirstOrDefault().UserName;
                objNotifHub.SendNotification(currentUser);
            }
   
        }
    }
}
