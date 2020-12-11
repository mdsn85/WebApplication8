using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApplication8.Hubs
{
    public class helloHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}