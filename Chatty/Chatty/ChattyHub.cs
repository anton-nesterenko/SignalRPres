using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Chatty
{
    [HubName("chatty")]
    public class ChattyHub : Hub
    {
        public void Speak(string message)
        {
            string user = Context.ConnectionId;
            if (message.StartsWith("-p"))
            {
                var targetConnectionId = message.Split(' ')[1];
                message = message.Substring(targetConnectionId.Length + 4);
                Clients.Client(targetConnectionId).secret(user + ": " + message);
            }
            else
            {
                Clients.Others.spoke(user + ": " + message);
            }
        }

        public override Task OnConnected()
        {
            return Clients.Others.connected(Context.ConnectionId);
        }

    }
}