using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Models
{
    public class Message
    {
        public string Username { get; set; }
        public string Text { get; set; }

        public Message(string username, string text)
        {
            Username = username;
            Text = text;
        }

    }
}
