using System;
using System.IO;
using System.Threading;

namespace CommunityApp_PRG_Project_.GroupChat
{
    public class ChatGroup
    {
        private const string ChatFilePath = "chatlog.txt";
        private const int PollingInterval = 1000; // milliseconds

        public void StartChat()
        {
            Console.Clear();
            Console.WriteLine("===== City Group Chat =====");
            Console.WriteLine("Starting chat... Type 'exit' to leave the chat.");

            // Start a new thread to read the chat log
            Thread readerThread = new Thread(ReadChatLog);
            readerThread.IsBackground = true; // Ends when the main application ends
            readerThread.Start();

            // Main thread for user input
            while (true)
            {
                string message = Console.ReadLine();
                if (message.ToLower() == "exit")
                    break;

                AppendMessageToFile(message);
            }
        }

        private void ReadChatLog()
        {
            long lastFileLength = 0;

            while (true)
            {
                if (File.Exists(ChatFilePath))
                {
                    long currentLength = new FileInfo(ChatFilePath).Length;
                    if (currentLength > lastFileLength)
                    {
                        using (FileStream fs = new FileStream(ChatFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            fs.Seek(lastFileLength, SeekOrigin.Begin);
                            using (StreamReader reader = new StreamReader(fs))
                            {
                                string newMessage;
                                while ((newMessage = reader.ReadLine()) != null)
                                {
                                    Console.WriteLine(newMessage);
                                }
                            }
                        }

                        lastFileLength = currentLength;
                    }
                }

                Thread.Sleep(PollingInterval);
            }
        }

        protected void AppendMessageToFile(string message)
        {
            using (StreamWriter sw = new StreamWriter(ChatFilePath, true))
            {
                sw.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
