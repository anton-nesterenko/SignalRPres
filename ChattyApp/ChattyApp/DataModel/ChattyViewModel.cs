﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;
using System.Collections.Specialized;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace ChattyApp.Data
{
    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class ChattyViewModel
    {
        public ChattyViewModel() : this(new ChattyClient())
        {
            
        }

        public ChattyViewModel(ChattyClient chattyClient)
        {
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

            chattyClient.AddMessage = (name, message) => _dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                                                                              () => Items.Insert(0, new ChattyMessage(name, message)));
        }

        private ObservableCollection<ChattyMessage> _items = null;
        private CoreDispatcher _dispatcher;

        public ObservableCollection<ChattyMessage> Items
        {
            get { 
                _items = _items ?? new ObservableCollection<ChattyMessage>();
                return _items;
            }
        }

        public void Speak(string text)
        {
        }
    }

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
            _hub.Invoke("setName", "Win8Tomas");
        }
    }
}