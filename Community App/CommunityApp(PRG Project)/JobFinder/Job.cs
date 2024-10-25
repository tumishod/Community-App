using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityApp_PRG_Project_.JobFinder
{
    public class Job
    {
        private string title;
        private string description;
        private string empName;

        public Job()
        {
            
        }

        public Job(string title, string description, string employerName)
        {
            this.title = title;
            this.description = description;
            this.empName = employerName;
        }

        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public string EmployerName { get => empName; set => empName = value; }

        public override string ToString()
        {
            return ($"Job:{title}, Description:{description}, Employer:{empName}");
        }
    }
}
