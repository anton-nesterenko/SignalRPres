using System.Collections.ObjectModel;
using ChattyApp.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Split Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234234

namespace ChattyApp
{
    /// <summary>
    /// A page that displays a group title, a list of items within the group, and details for the
    /// currently selected item.
    /// </summary>
    public sealed partial class SplitPage
    {
        private ChattyClient _chattyClient;
        private ObservableCollection<ChattyMessage> _chatMessages;
        private CoreDispatcher _dispatcher;

        public SplitPage()
        {
            this.InitializeComponent();
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            _chattyClient = new ChattyClient();
            _chatMessages = new ObservableCollection<ChattyMessage>();
            _chattyClient.AddMessage = AddMessage;
            itemListView.ItemsSource = _chatMessages;
        }

        private void AddMessage(dynamic name, dynamic message)
        {
            _dispatcher.RunAsync(CoreDispatcherPriority.Normal, 
                () =>
                    {
                        _chatMessages.Add(new ChattyMessage(name, message));
                    });
        }

        private void Speak_Clicked(object sender, RoutedEventArgs e)
        {
            var text = message.Text;
            _chattyClient.Speak(text);
            message.Text = "";
        }
    }
}
