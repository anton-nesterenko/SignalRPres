using System.Collections.Generic;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalREcho.App_Start;

[assembly: PreApplicationStartMethod(typeof(Startup), "Gogogo")]

namespace SignalREcho.App_Start
{
    public static class Startup
    {
        public static void Gogogo()
        {
            GlobalHost.DependencyResolver.UseRedis(
                server: "localhost",
                port: 6379,
                password: "",
                eventKeys: new List<string>() {"SignalREcho"}
                );
        }
    }
}