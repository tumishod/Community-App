using CommunityApp_PRG_Project_.UserManagement;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommunityApp_PRG_Project_.EventManagement
{
    public class Event
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public List<string> RSVPs { get; set; } = new List<string>();

        public Event(string eventName, DateTime eventDate )
        {
            EventName = eventName;
            EventDate = eventDate;
        }

        public void AddRSVP(string username)
        {
            if (!RSVPs.Contains(username))
            {
                RSVPs.Add(username);
                Console.WriteLine($"{username} has RSVPed for {EventName}.");
                SaveRSVPToFile(username);
            }
            else
            {
                Console.WriteLine($"{username} has already RSVPed for {EventName}.");
            }
        }

        private void SaveRSVPToFile(string username)
        {
            using (StreamWriter sw = new StreamWriter("rsvps.txt", true))
            {
                sw.WriteLine($"{EventName},{EventDate},{username}");
            }
        }

        public TimeSpan GetTimeUntilEvent()
        {
            return EventDate - DateTime.Now;
        }

        public override string ToString()
        {
            return $"{EventName},{EventDate}";
        }

        public static Event FromString(string eventString)
        {
            var parts = eventString.Split(',');
            if (parts.Length >= 2)
            {
                string eventName = parts[0];
                if (DateTime.TryParse(parts[1], out DateTime eventDate))
                {
                    return new Event(eventName, eventDate);
                }
            }
            return null;
        }
    }
}
