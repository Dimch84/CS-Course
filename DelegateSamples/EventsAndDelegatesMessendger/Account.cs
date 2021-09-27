using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsAndDelegatesMessendger
{
    public delegate void OnMessage(Message message);
    public delegate void OnSend(object sender, OnSendEventArgs e);

    public class Account
    {
        public string Username { get; private set; }
        public List<Message> Messages { get; set; }

        public event OnSend OnSend;

        public OnMessage NewMessage { get; set; } = delegate (Message message)  // (message)  =>
        {
            if (message.Send)
                Console.WriteLine($"{message.To.Username}, new message from {message.From.Username}:\n  {message.Preview}", ConsoleColor.Green);
        };

        public Account(string username)
        {
            Username = username;
            Messages = new List<Message>();
        }

        public Message CreateMessage(string text, Account to)
        {
            var message = new Message(this, to, text);
            Messages.Add(message);
            return message;
        }

        public void Send(Message message)
        {
            message.Send = true;
            message.To.Messages.Add(message);
            message.To.NewMessage(message);
            if(OnSend != null)
                OnSend(this, new OnSendEventArgs(message.ReadMessage(this), message.From.Username, message.To.Username));
        }

        public void OnNewMessage(Message message)
        {
            if (message.Send)
                Console.WriteLine($"OnNewMessage: {message.From.Username}: {message.Preview}", ConsoleColor.DarkYellow);
        }

        public void ShowDialog(string username)
        {
            List<Message> messageDialog = Messages
                .Where(x => x.To.Username.Equals(username) || x.From.Username.Equals(username))
                .Where(x => x.Send)
                .OrderBy(x => x.Created)
                .ToList();

            string str = $"Dialog with {username}";
            Console.WriteLine($"---{str}---");
            foreach (Message message in messageDialog)
            {
                Console.WriteLine($"{(message.From.Username.Equals(username) ? username : Username)}: {message.ReadMessage(this)}",
                    message.From.Username.Equals(username) ? ConsoleColor.Cyan : ConsoleColor.DarkYellow);
            }
            Console.WriteLine($"---{string.Concat(str.Select(x => "-"))}---");
        }
    }
}
