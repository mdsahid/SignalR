using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;
using SignalRAuth.Models;

namespace SignalRAuth.Hubs
{
    public class MyHub : Hub
    {
        ApplicationDbContext db = new ApplicationDbContext();
        UserMessage UM;
        static int _total;

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnConnected()
        {
            Interlocked.Increment(ref _total);
            Clients.All.NotifyUser(Context.User.Identity.Name, _total);
            LoadPreviousMessages();
            return base.OnConnected();
        }

        private void LoadPreviousMessages()
        {
            var list = from m in db.UserMessages
                       orderby m.PostDate
                       select new
                       {
                           user = m.UserName,
                           message = m.Messages
                       };

            foreach (var i in list)
            {
                Clients.Caller.SendMessages(i.user, i.message);
            }

            
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Interlocked.Decrement(ref _total);
            Clients.All.NotifyDisconnectUser(Context.User.Identity.Name);
            return base.OnDisconnected(stopCalled);

        }

        public Task Send(string message)
        {
            UM = new UserMessage { Messages = message, UserName = Context.User.Identity.Name, PostDate = DateTime.Now };
            // Save into db
            db.UserMessages.Add(UM);
            db.SaveChangesAsync();
            var user = Context.User.Identity.Name;
            return Clients.All.SendMessages(user, message);

        }
              
    }
}