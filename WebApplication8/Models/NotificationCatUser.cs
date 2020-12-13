using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    [Table("NotificationCatUsers")]
    public class NotificationCatUser
    {
        public int NotificationCatUserId { get; set; }
        public int NotificationCategoryId { get; set; }
        public string UserId { get; set; }

        public virtual NotificationCategory NotificationCategory { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}