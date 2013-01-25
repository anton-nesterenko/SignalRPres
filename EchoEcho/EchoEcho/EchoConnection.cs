﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace EchoEcho
{
    public class EchoConnection : PersistentConnection
    {
        protected override System.Threading.Tasks.Task OnReceivedAsync(
            IRequest request, string connectionId, string data)
        {
            return Connection.Broadcast(connectionId + ": " + data);
        }
    }
}