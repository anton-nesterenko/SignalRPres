﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalChat
{
    [HubName("chatty")]
    public class ChattyHub : Hub
    {
        public static Dictionary<string, string> _connections = new Dictionary<string, string>();

        public void Speak(string message)
        {
            var name = _connections[Context.ConnectionId];
            if (message.StartsWith("-p"))
            {
                var messageSplit = message.Split(' ');
                var targetUser = messageSplit[1];
                message = message.Substring(4 + targetUser.Length);
                var targetUserConnectionId = _connections.Single(y => y.Value == targetUser).Key;
                Clients.Client(targetUserConnectionId).spoke(name, "(private) " +message);
                Clients.Caller.spoke(name, "(" + targetUser + ") " + message);
            }
            else
            {
                Clients.All.spoke(name, message);
            }
        }

        public void SetName(string name)
        {
            _connections[Context.ConnectionId] = name;
            GetAllClients().userConnectecd(name);
        }

        public override Task OnConnected()
        {
            Clients.Caller.connect(_connections.Values);
            var connectionId = Context.ConnectionId;
            _connections.Add(connectionId, "");
            return null;
        }

        public override Task OnDisconnected()
        {
            var name = _connections[Context.ConnectionId];
            GetAllClients().userDisconnected(name);
            _connections.Remove(Context.ConnectionId);
            return null;
        }

        private static dynamic GetAllClients()
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<ChattyHub>().Clients;
            return clients.All;
        }
    }
}
