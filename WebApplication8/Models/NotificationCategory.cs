using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public enum NotificationType { New, Change };

    public class NotificationCategory
    {

        public int NotificationCategoryId { get; set; }
        public string Name { get; set; }
        //public Nullable<int> Type { get; set; }
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(250)]
        public string Details { get; set; }
        [StringLength(250)]
        public string DetailsURL { get; set; }





        public NotificationType NotificationType { get; set; }

        public virtual ICollection<NotificationCatUser> NotificationCatUser { get; set; }

        public NotificationCategory()
        {
            NotificationCatUser = new Collection<NotificationCatUser>();
        }
    }
}