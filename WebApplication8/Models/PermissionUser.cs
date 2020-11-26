using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class PermissionUser
    {
        public int PermissionUserId { get; set; }

        public int PermissionId { get; set; }
        public virtual Permission Permissions { get; set; }

        public string Username { get; set; }
    }
}