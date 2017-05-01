using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SingnalRChatAppFinal.Hubs
{
    public class MyHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public void Send(string userName,string message)
        {
            Clients.All.sendMessage(userName, message);
        }
    }
}