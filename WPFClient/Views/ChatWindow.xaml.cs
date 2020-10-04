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
using ChatClient.ViewModels;
using MaterialDesignThemes.Wpf;

namespace ChatClient.Views
{
    /// <summary>
    /// Logique d'interaction pour ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public ChatViewModel ChatViewModel { get; set; }

        public ChatWindow(string username, SignalRChatService chatService)
        {
            ChatViewModel = new ChatViewModel(username, chatService);
            ChatViewModel.ClearTextbox_Request += ClearTextbox_RequestHandler;
            ChatViewModel.ConnexionFailed_Event += ConnexionFailed_EventHandler;
            DataContext = ChatViewModel;
            this.Title = $"Online chat - {username}";
            InitializeComponent();
        }

        private void ConnexionFailed_EventHandler()
        {
            var messageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            ConnexionSnackBar.MessageQueue = messageQueue;
            ConnexionSnackBar.MessageQueue.Enqueue("Message couldn't be delivered");
        }

        private void ClearTextbox_RequestHandler()
        {
            Dispatcher.BeginInvoke((Action)delegate ()
            {
                TBXMessage.Clear();
            });
        }

        private void TBXMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            BTNSendMessage.IsEnabled = string.IsNullOrEmpty(TBXMessage.Text) ? false : true;
        }

        private void BTNSendMessage_Click(object sender, RoutedEventArgs e)
        {
            ChatViewModel.SendMessage();
        }
    }
}
