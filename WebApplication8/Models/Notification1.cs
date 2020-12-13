using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Notification1
    {
        [Key]
        public int Notification1Id { get; set; }
        public int? ObjectId { get; set; }

        public DateTime NotificationDate { get; set; }

        public Nullable<bool> IsRead { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsReminder { get; set; }

        public int NotificationCategoryId { get; set; }
        public virtual NotificationCategory NotificationCategory { get; set; }



    }
}