using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityApp_PRG_Project_.JobFinder
{
    public class Employer
    {
        string name;
        int contactNo;

        public Employer()
        {
            
        }

        public Employer(string name, int contactNo)
        {
            this.name = name;
            this.contactNo = contactNo;
        }

        public string Name { get => name; set => name = value; }
        public int ContactNo { get => contactNo; set => contactNo = value; }

        public override string ToString()
        {
            return ($"Employer Name: {Name}, Contact Number: {contactNo}");
        }
    }
}
