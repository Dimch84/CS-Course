using System;

namespace EventsAndDelegatesMessendger
{
    public class OnSendEventArgs : EventArgs
    {
        public string Text { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public OnSendEventArgs(string text, string from, string to)
        {
            Text = text;
            From = from;
            To = to;
        }
    }
}
