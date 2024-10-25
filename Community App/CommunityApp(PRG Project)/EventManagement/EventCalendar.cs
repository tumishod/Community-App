using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace CommunityApp_PRG_Project_.EventManagement
{
    public class EventCalendar
    {
        private const string EventFilePath = "events.txt";
        private const string RSVPFilePath = "rsvps.txt";

        public List<Event> Events { get; private set; } = new List<Event>();

        public void LoadEventsFromFile()
        {
            if (File.Exists(EventFilePath))
            {
                Events.Clear();
                string[] lines = File.ReadAllLines(EventFilePath);
                foreach (var line in lines)
                {
                    var loadedEvent = Event.FromString(line);
                    if (loadedEvent != null)
                    {
                        Events.Add(loadedEvent);
                    }
                }
            }
        }

        public void SaveEventToFile(Event newEvent)
        {
            using (StreamWriter sw = new StreamWriter(EventFilePath, true))
            {
                sw.WriteLine(newEvent.ToString());
            }
        }

        public void LoadRSVPsFromFile()
        {
            if (File.Exists(RSVPFilePath))
            {
                string[] lines = File.ReadAllLines(RSVPFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        var eventName = parts[0];
                        var eventDate = DateTime.Parse(parts[1]);
                        var username = parts[2];

                        var existingEvent = Events.FirstOrDefault(e => e.EventName == eventName && e.EventDate == eventDate);
                        if (existingEvent != null)
                        {
                            existingEvent.RSVPs.Add(username);
                        }
                    }
                }
            }
        }

        public void AddEvent(Event newEvent)
        {
            Events.Add(newEvent);
            SaveEventToFile(newEvent);
            Console.WriteLine($"Event '{newEvent.EventName}' added for {newEvent.EventDate}.");
        }

        public void RemoveEvent(string eventName)
        {
            var eventToRemove = Events.FirstOrDefault(e => e.EventName == eventName);
            if (eventToRemove != null)
            {
                Events.Remove(eventToRemove);
                Console.WriteLine($"Event '{eventName}' removed.");
            }
            else
            {
                Console.WriteLine($"Event '{eventName}' not found.");
            }
        }

        public Event GetEvent(string eventName)
        {
            return Events.FirstOrDefault(e => e.EventName == eventName);
        }

        public void ListEvents()
        {
            Console.Clear();
            Console.WriteLine("======List Of Events======");
          
            if (Events.Count == 0)
            {
                Console.WriteLine("No upcoming events.");
                return;
            }

            foreach (var e in Events)
            {
                var timeUntilEvent = e.GetTimeUntilEvent();
                Console.WriteLine($"{e.EventName} - {e.EventDate} ({timeUntilEvent.Days} days, {timeUntilEvent.Hours} hours remaining)");
            }
           
        }
    }
}
