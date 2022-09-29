using System;

namespace EventsAndDelegatesMessendger
{
    public class Message
    {
        private readonly string text;

        public Account From { get; set; }
        public Account To { get; set; }
        public bool Read { get; set; }
        public bool Send { get; set; }
        public DateTime Created { get; private set; } 

        public string Preview
        {
            get
            {
                return $"{text.Substring(0, 10)}...";
            }
        }

        public Message(Account from, Account to, string text)
        {
            From = from;
            To = to;
            this.text = text;
            Read = false;
            Created = DateTime.Now;
        }

        public string ReadMessage(Account account)
        {
            Read = true;
            return (To == account || From == account) ? text : string.Empty;
        }
    }
}
