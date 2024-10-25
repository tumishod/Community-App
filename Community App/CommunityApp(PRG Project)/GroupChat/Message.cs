using System;

namespace CommunityApp_PRG_Project_.GroupChat
{
    public class Message
    {
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public Message(string sender, string content)
        {
            Sender = sender;
            Content = content;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Timestamp}: {Sender}: {Content}";
        }
    }
}
 