using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace SignalChat
{
    [HubName("chatty")]
    public class ChattyHub : Hub
    {
        public void Speak(string name, string message)
        {
            Clients.spoke(name, message);
        }
    }
}