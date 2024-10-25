using System;
using System.Collections.Generic;

namespace CommunityApp_PRG_Project_.EventManagement
{
    public class EventManager
    {
        private EventCalendar eventCalendar;

        public delegate void EventAddedHandler(Event newEvent);
        public event EventAddedHandler EventAdded;

        public EventManager(EventCalendar calendar)
        {
            eventCalendar = calendar;
        }

        public void EventMenu()
        {
            
            Console.WriteLine("===== Event Management =====");
            Console.WriteLine("1. Create Event");
            Console.WriteLine("2. List Events");
            Console.WriteLine("3. RSVP to Event");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Choose an option: ");

            int eventOption;
            if (int.TryParse(Console.ReadLine(), out eventOption))
            {
                switch (eventOption)
                {
                    case 1:
                        CreateEvent();
                        break;

                    case 2:
                        eventCalendar.ListEvents();
                        break;

                    case 3:
                        RSVPToEvent();
                        break;

                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }

            EventMenu();
        }

        private void CreateEvent()
        {
            Console.Clear();
            Console.WriteLine("===== Add an Event =====");
            Console.Write("Enter event name: ");
            string eventName = Console.ReadLine();

            Console.Write("Enter event date and time (yyyy-mm-dd hh:mm): ");
            DateTime eventDate;
            if (DateTime.TryParse(Console.ReadLine(), out eventDate))
            {
                Event newEvent = new Event(eventName, eventDate);
                eventCalendar.AddEvent(newEvent);
                OnEventAdded(newEvent);
            }
            else
            {
                Console.WriteLine("Invalid date and time format.");
            }
        }

        private void RSVPToEvent()
        {
            Console.Clear();
            Console.WriteLine("===== RSVP to an Event =====");

            if (eventCalendar.Events.Count == 0)
            {
                Console.WriteLine("There are no events available to RSVP.");
                return;
            }

            for (int i = 0; i < eventCalendar.Events.Count; i++)
            {
                var e = eventCalendar.Events[i];
                Console.WriteLine($"{i + 1}. {e.EventName} - {e.EventDate} ({e.GetTimeUntilEvent().Days} days, {e.GetTimeUntilEvent().Hours} hours remaining)");
            }

            Console.Write("Enter the number of the event you want to RSVP to: ");
            int eventIndex;
            if (int.TryParse(Console.ReadLine(), out eventIndex) && eventIndex > 0 && eventIndex <= eventCalendar.Events.Count)
            {
                Event selectedEvent = eventCalendar.Events[eventIndex - 1];
                Console.Write("Enter your username: ");
                string username = Console.ReadLine();
                selectedEvent.AddRSVP(username);
            }
            else
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        }

        protected virtual void OnEventAdded(Event newEvent)
        {
            EventAdded?.Invoke(newEvent);
        }
    }
}
