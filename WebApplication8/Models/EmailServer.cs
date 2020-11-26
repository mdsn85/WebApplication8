using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class EmailServer
    {
        public int EmailServerId { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
    }
}