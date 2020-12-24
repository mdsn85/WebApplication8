using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApplication8.Models;
using System.Data.Entity;
using log4net;

namespace WebApplication8.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, UserHubModels> Users =
            new ConcurrentDictionary<string, UserHubModels>(StringComparer.InvariantCultureIgnoreCase);


        private ApplicationDbContext context = new ApplicationDbContext();

        ILog log = LogManager.GetLogger("application-log");

        //Logged Use Call
        public void GetNotification()
        {
            try
            {

                string loggedUser = Context.User.Identity.Name;
                log.Debug("start get notification:" + loggedUser);
    
                //Get TotalNotification
                List<note> totalNotif = LoadNotifData(loggedUser);
                log.Debug("LoadNotifData:" + totalNotif);
                //Send To
                UserHubModels receiver;
                if (Users.TryGetValue(loggedUser, out receiver))
                {
                    var cid = receiver.ConnectionIds.FirstOrDefault();
                    var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    log.Debug("start prodcats :" + cid);
                    context.Clients.Client(cid).broadcaastNotif(totalNotif);
                }
                else
                {
                    log.Debug("TryGetValue fail:");
                }
            }
            catch (Exception e)
            {
                log.Error(" ERROR mylog - Error while save project to server:" + e.Message + " , stacktrace:" + e.StackTrace);


            }
        }


        //Specific User Call
        public void SendNotification(string SentTo)
        {
            try
            {

                    //Get TotalNotification
                    List<note> totalNotif = LoadNotifData(SentTo);

                    //Send To
                    //var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    //context.Clients.All.broadcaastNotif(totalNotif);
                    UserHubModels receiver;
                    if (Users.TryGetValue(SentTo, out receiver))
                    {
                        var cid = receiver.ConnectionIds.FirstOrDefault();
                        var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                        context.Clients.Client(cid).broadcaastNotif(totalNotif);
                    }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //Specific User Call
        public void SendNotification(string[] recipients)
        {
            try
            {
                foreach (string SentTo in recipients)
                {
                    //Get TotalNotification
                    List<note> totalNotif = LoadNotifData(SentTo);

                    //Send To
                    //var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    //context.Clients.All.broadcaastNotif(totalNotif);
                    UserHubModels receiver;
                    if (Users.TryGetValue(SentTo, out receiver))
                    {
                        var cid = receiver.ConnectionIds.FirstOrDefault();
                        var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                        context.Clients.Client(cid).broadcaastNotif(totalNotif);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        class note
        {
            public int id { get; set; }
            public string Title { get; set; }
            public string URL { get; set; }
            public int ObjectId { get; set; }
            public DateTime NotificationDate { get; set; }
        }
        private List<note> LoadNotifData(string userName)
        {
            try
            {
                var currentUser = context.Users.Where(u => u.UserName == userName).FirstOrDefault();
                //var x = context.
                //get notificatin 
                var listCat1 = context.NotificationCategorys.Include(nc => nc.NotificationCatUser).ToList();
                //7014dcd7-6824-422f-8441-fca6221acc11 , 2e1ed9d7-d498-4e15-9aeb-3c3599920b83
                var listCat = listCat1.Where(nc => nc.NotificationCatUser.Select(b => b.UserId).Contains(currentUser.Id)).ToList();



                var catOfUser = listCat.Select(a => a.NotificationCategoryId).ToList();



                var query = (from t in context.Notifications
                             where catOfUser.Contains(t.NotificationCategory.NotificationCategoryId)
                             && t.IsRead != true
                             select (new note
                             {
                                 id = t.Notification1Id,
                                 Title = t.NotificationCategory.Title,
                                 ObjectId = t.ObjectId ?? 0,
                                 URL = t.NotificationCategory.DetailsURL,
                                 NotificationDate = t.NotificationDate
                             }))
                            .OrderByDescending(q => q.NotificationDate)
                            .ToList();
                //total = query.Count;

                return query.ToList();
            }catch(Exception e)
            {
                log.Error(" ERROR mylog - Error while save project to server:" + e.Message + " , stacktrace:" + e.StackTrace);
            }
            return null;
        }

        public override Task OnConnected()
        {
            //string userName = "Admin"; 
            //string name = Context.User.Identity.Name;
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;


            var user = Users.GetOrAdd(userName, _ => new UserHubModels
            {
                UserName = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userName);
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            UserHubModels user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                    if (!user.ConnectionIds.Any())
                    {
                        UserHubModels removedUser;
                        Users.TryRemove(userName, out removedUser);
                        Clients.Others.userDisconnected(userName);
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}