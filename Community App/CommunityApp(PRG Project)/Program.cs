using CommunityApp_PRG_Project_.EventManagement;
using CommunityApp_PRG_Project_.GroupChat;
using CommunityApp_PRG_Project_.JobFinder;
using CommunityApp_PRG_Project_.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CommunityApp_PRG_Project_
{
    internal class Program
    {
        enum MainMenu
        {
            Events = 1,
            Job_Finder,
            City_Group_Chat,
            Exit
        }

        static List<Employer> employers = new List<Employer>();
        static List<Applicant> applicants = new List<Applicant>();
        static List<Job> jobs = new List<Job>();

        static IJobFinder jobFinderService = new JobFinderService();

        public static void Menu(EventManager eventManager)
        {
            Console.Clear();
            DisplayAsciiArt();

            Console.WriteLine("===== Main Menu =====");
            Console.WriteLine("To select an option below, type in the corresponding number provided");

            foreach (MainMenu MenuOption in Enum.GetValues(typeof(MainMenu)))
            {
                string splitName = MenuOption.ToString().Replace('_', ' ');
                Console.WriteLine("To access the {0} function, press {1}", splitName, (int)MenuOption);
            }
            redo:
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        eventManager.EventMenu();
                        break;
                    case 2:
                        Console.Clear();
                        JobFinderMenu();
                        break;
                    case 3:
                        StartGroupChat();
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        Thread.Sleep(2000);
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        goto redo;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
            Menu(eventManager);
        }

        static void StartGroupChat()
        {
            HierarchicalGroupChat groupChat = new HierarchicalGroupChat();
            groupChat.AddGroup("City Group");
            groupChat.StartChat();
        }

        static void DisplayAsciiArt()
        {
            Console.WriteLine(@"
    __  ___         ______                                      _ __       
   /  |/  /_  __   / ____/___  ____ ___  ____ ___  __  ______  (_) /___  __
  / /|_/ / / / /  / /   / __ \/ __ `__ \/ __ `__ \/ / / / __ \/ / __/ / / /
 / /  / / /_/ /  / /___/ /_/ / / / / / / / / / / / /_/ / / / / / /_/ /_/ / 
/_/  /_/\__, /   \____/\____/_/ /_/ /_/_/ /_/ /_/\__,_/_/ /_/_/\__/\__, /  
       /____/                                                     /____/   
 ");
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            List<User> people = new List<User>{ };

            Console.WriteLine("Welcome to the My Community App");
            Console.WriteLine("To sign in press 1 OR to login press 2");
        redo:
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        var signedUser = User.SignUp();
                        User newUser = new User(signedUser.Item1, signedUser.Item2);
                        people.Add(newUser);
                        Console.Clear();
                        Console.WriteLine("******** Sign Up Successful ********");
                        Thread.Sleep(2000);
                        Console.Clear();
                        Login(people);
                        break;

                    case 2:
                        Login(people);
                        break;

                    default:
                        Console.WriteLine("Enter either 1 or 2, not spelt out, just the number");
                        goto redo;
                }
                
            }
            else
            {
                Console.WriteLine("Enter either 1 or 2, not spelt out, just the number");
                goto redo;
            }
            EventCalendar calendar = new EventCalendar();
            calendar.LoadEventsFromFile();
            calendar.LoadRSVPsFromFile();
            EventManager eventManager = new EventManager(calendar);

            eventManager.EventAdded += OnEventAdded;
            Menu(eventManager);
        }

        static (string, string) Login(List<User> people)
        {
            try
            {
                return User.Login(people);
            }
            catch (InvalidLoginException ex)
            {
                Console.WriteLine(ex.Message);
                return Login(people); // Retry login
            }
        }

        static void OnEventAdded(Event newEvent)
        {
            Console.WriteLine($"[Notification] New event added: {newEvent.EventName} on {newEvent.EventDate}");
        }

        static void JobFinderMenu()
        {
            
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("======Job Portal Menu======");
                Console.WriteLine("1. Add Employer");
                Console.WriteLine("2. Add Applicant");
                Console.WriteLine("3. Add Job");
                Console.WriteLine("4. List Employers");
                Console.WriteLine("5. List Applicants");
                Console.WriteLine("6. List Jobs");
                Console.WriteLine("7. Exit");
                redo:
                Console.Write("Choose an option: ");
                string pick = Console.ReadLine();

                switch (pick)
                {
                    case "1":
                        jobFinderService.AddEmployer(employers);
                        break;
                    case "2":
                        jobFinderService.AddApplicant(applicants);
                        break;
                    case "3":
                        jobFinderService.AddJob(jobs);
                        break;
                    case "4":
                        jobFinderService.ListEmployers(employers);
                        break;
                    case "5":
                        jobFinderService.ListApplicants(applicants);
                        break;
                    case "6":
                        jobFinderService.ListJobs(jobs);
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        goto redo;
                }
            }
        }
    }
}
