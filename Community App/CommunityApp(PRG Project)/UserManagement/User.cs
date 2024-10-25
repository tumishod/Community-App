using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace CommunityApp_PRG_Project_.UserManagement
{
    // Custom exception for invalid login attempts
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException(string message) : base(message) { }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void ShowDetails()
        {
            Console.WriteLine($"Username: {Username}, Password: {Password}");
        }

        public static (string, string) SignUp()
        {
            Console.WriteLine("===== Sign up =====");
            Console.Write("Create a username: ");
            string username = Console.ReadLine();

        passrepeat:
            Console.Write("Create password: ");
            string precheck_password = Console.ReadLine();
            Console.Write("Repeat your password: ");
            string repeat_password = Console.ReadLine();

            if (precheck_password == repeat_password)
            {
                string encryptedPassword = EncryptPassword(precheck_password); // Encrypt the password before saving
                SaveUserToFile(username, encryptedPassword);
                return (username, encryptedPassword);
            }
            else
            {
                Console.WriteLine("Passwords don't match, ensure you input the same password.");
                goto passrepeat;
            }
        }

        private static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static void SaveUserToFile(string username, string encryptedPassword)
        {
            using (StreamWriter sw = new StreamWriter("login.txt", true)) // Append mode
            {
                sw.WriteLine(username);
                sw.WriteLine(encryptedPassword);
            }
        }

        public static (string, string) Login(List<User> people)
        {
            // Load users from the file
            List<User> usersFromFile = LoadUsersFromFile();
            if (usersFromFile != null)
            {
                people.AddRange(usersFromFile);
            }

            Console.WriteLine("===== Login =====");

        redo_user:
            Console.Write("Enter your username: ");
            string username_check = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password_check = Console.ReadLine();

            // Encrypt the entered password to compare with the stored encrypted passwords - FOR THE LOVE OF GOD DO NOT TOUCH THIS CODE
            string encryptedPasswordCheck = EncryptPassword(password_check);

            User foundUser = people.FirstOrDefault(user => user.Username == username_check);

            if (foundUser != null && foundUser.Password == encryptedPasswordCheck)
            {
                Console.Clear();
                Console.WriteLine("******** Login Successful ********");
                Thread.Sleep(2000);
                Console.Clear();
                return (username_check, encryptedPasswordCheck);
            }
            else
            {
                Console.WriteLine("Incorrect username or password, ensure you input the correct details.");
                goto redo_user;
            }
        }

        private static List<User> LoadUsersFromFile()
        {
            List<User> users = new List<User>();

            if (File.Exists("login.txt"))
            {
                using (StreamReader sr = new StreamReader("login.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string username = sr.ReadLine();
                        string password = sr.ReadLine();
                        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                        {
                            users.Add(new User(username, password));
                        }
                    }
                }
            }
            return users;
        }
    }
}
