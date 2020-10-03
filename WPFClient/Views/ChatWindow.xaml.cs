using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChatClient.Models;
using WPFChatClient.Services;
using System.Diagnostics;
using System.Threading;

namespace ChatClient.Views
{
    /// <summary>
    /// Logique d'interaction pour ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private readonly string _username;
        public List<Message> Messages { get; set; } = new List<Message>();
        private readonly SignalRChatService chatService;

        public ChatWindow(string username)
        {
            Messages.Add(new Message("",""));
            InitializeComponent();
            _username = username;
            this.Title = $"Online chat - {_username}";
            HubConnection hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5000/chat").Build();
            chatService = new SignalRChatService(hubConnection);
            chatService.Connect().ContinueWith(task =>
            {
                if(task.Exception != null)
                {
                    Debug.WriteLine("Connexion failed");
                }
                else
                {
                    Debug.WriteLine("Connexion success");
                }
            });
            chatService.MessageReceivedEvent += Message_Received;
        }

        private void Message_Received(string username, string text)
        {
            Message message = new Message(username, text);
            Messages.Add(message);
            MessageDisplayer.ItemsSource = null;
            MessageDisplayer.ItemsSource = Messages;
        }

        private void TBXMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            BTNSendMessage.IsEnabled = string.IsNullOrEmpty(TBXMessage.Text) ? false : true;
        }

        private void BTNSendMessage_Click(object sender, RoutedEventArgs e)
        {
            chatService.SendMessage(_username,TBXMessage.Text);
            TBXMessage.Clear();
        }
    }
}
