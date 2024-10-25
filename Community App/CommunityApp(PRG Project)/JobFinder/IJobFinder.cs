using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityApp_PRG_Project_.JobFinder
{
    public interface IJobFinder
    {
        void AddEmployer(List<Employer> employers);
        void AddApplicant(List<Applicant> applicants);
        void AddJob(List<Job> jobs);
        void ListEmployers(List<Employer> employers);
        void ListApplicants(List<Applicant> applicants);
        void ListJobs(List<Job> jobs);
    }
}
