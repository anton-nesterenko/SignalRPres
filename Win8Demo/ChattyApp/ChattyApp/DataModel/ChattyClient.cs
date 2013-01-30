using System;
using System.Collections.ObjectModel;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Windows.UI.Core;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace ChattyApp.Data
{
    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// 
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public class ChattyClient
    {
        private HubConnection _connection;
        private IHubProxy _hub;

        public Action<dynamic, dynamic> AddMessage { get; set; }

        public ChattyClient()
        {
            Init();
        }

        public async void Speak(string message)
        {
            await _hub.Invoke("speak", message);
        }

        private async void Init()
        {
            _connection = new HubConnection("http://localhost:54506");
            _hub = _connection.CreateHubProxy("chatty");
            _hub.Subscribe("spoke").Data += tokens =>
                                                {
                                                    var name = tokens[0].ToString();
                                                    var message = tokens[1].ToString();
                                                    AddMessage(name, message);
                                                };

            await _connection.Start();
            await _hub.Invoke("setName", "Win8Tomas");
        }
    }
}
