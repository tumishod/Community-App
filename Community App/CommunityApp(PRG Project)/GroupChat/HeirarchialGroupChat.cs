using System;
using System.Collections.Generic;

namespace CommunityApp_PRG_Project_.GroupChat
{
    public class HierarchicalGroupChat : ChatGroup
    {
        private Dictionary<string, List<string>> groupChats;

        public HierarchicalGroupChat()
        {
            groupChats = new Dictionary<string, List<string>>();
        }

        public void AddGroup(string groupName)
        {
            if (!groupChats.ContainsKey(groupName))
            {
                groupChats[groupName] = new List<string>();
                Console.WriteLine($"Group '{groupName}' created.");
            }
            else
            {
                Console.WriteLine($"Group '{groupName}' already exists.");
            }
        }

        public void SendMessageToGroup(string groupName, string message)
        {
            if (groupChats.ContainsKey(groupName))
            {
                string formattedMessage = $"{DateTime.Now}: {message}";
                groupChats[groupName].Add(formattedMessage);
                Console.WriteLine($"Message sent to group '{groupName}'.");
                AppendMessageToFile($"{groupName}: {formattedMessage}");
            }
            else
            {
                Console.WriteLine($"Group '{groupName}' does not exist.");
            }
        }

        public void ListGroups()
        {
            if (groupChats.Count == 0)
            {
                Console.WriteLine("No groups available.");
                return;
            }

            Console.WriteLine("Available Groups:");
            foreach (var group in groupChats)
            {
                Console.WriteLine($"- {group.Key}");
            }
        }
    }
}
