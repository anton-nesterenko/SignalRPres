using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalREcho
{
    [HubName("echo")]
    public class EchoHub : Hub
    {
        public void Echo(string message)
        {
            Clients.Others.echo(Context.ConnectionId, message);
        }
    }
}