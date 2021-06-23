using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Setting
{
    public class JWTcs
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }

    }
}