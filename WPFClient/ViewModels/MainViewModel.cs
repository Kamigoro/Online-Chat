using ChatClient.Views;
using MaterialDesignThemes.Wpf;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFChatClient.Services;

namespace ChatClient.ViewModels
{
    public class MainViewModel
    {
        public string Username { get; set; }
        public SignalRChatService ChatService { get; set; }
        private HubConnection _hubConnection;
        public event Action CloseWindowRequest_Event;
        public event Action ConnexionFailed_Event;


        public MainViewModel()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl("https://chatserver-signalr.azurewebsites.net/chat").Build();
            ChatService = new SignalRChatService(_hubConnection);
        }

        public void Login()
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            ChatService.Connect().ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    new ChatWindow(Username, ChatService).Show();
                    CloseWindowRequest_Event.Invoke();
                }
                else
                {
                    ConnexionFailed_Event.Invoke();
                }
            }, scheduler);
        }
    }
}
