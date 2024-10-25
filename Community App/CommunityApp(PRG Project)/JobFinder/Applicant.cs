using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityApp_PRG_Project_.JobFinder
{
    public class Applicant
    {
        private string name;
        private string email;
        private string cV;

        public Applicant()
        {
            
        }

        public Applicant(string name, string email, string cV)
        {
            this.name = name;
            this.email = email;
            this.cV = cV;
        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string CV { get => cV; set => cV = value; }

        public override string ToString()
        {
            return ($"Applicant: {Name}, Email: {Email}, CV: {CV}");
        }
    }
}
