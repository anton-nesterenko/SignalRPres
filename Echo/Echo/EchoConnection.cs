using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Echo
{
    public class EchoConnection : PersistentConnection
    {
        protected override System.Threading.Tasks.Task OnReceived(IRequest request, string connectionId, string data)
        {
            return Connection.Broadcast(connectionId + ": " + data);
        }
    }
}