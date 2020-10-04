using ChatClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WPFChatClient.Services;

namespace ChatClient.ViewModels
{
    public class ChatViewModel
    {
        public string Username { get; set; }
        public string TextToSend { get; set; }
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public SignalRChatService ChatService { get; set; }
        public event Action ClearTextbox_Request;
        public event Action ConnexionFailed_Event;

        public ChatViewModel(string username, SignalRChatService chatService)
        {
            Username = username;
            ChatService = chatService;
            ChatService.MessageReceived_Event += MessageReceived_EventHandler;
        }

        public void SendMessage()
        {
            ChatService.SendMessage(Username, TextToSend)
                .ContinueWith(task => 
                {
                    if (task.Exception == null)
                    {
                        ClearTextbox_Request.Invoke();
                    }
                    else
                    {
                        ConnexionFailed_Event.Invoke();
                    }
                });
        }

        private void MessageReceived_EventHandler(string username, string text)
        {
            Messages.Add(new Message(username,text));
        }
    }
}
