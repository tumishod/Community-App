using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CommunityApp_PRG_Project_.JobFinder
{
    public class JobFinderService : IJobFinder
    {
        private const string EmployerFilePath = "employers.txt";
        private const string ApplicantFilePath = "applicants.txt";
        private const string JobFilePath = "jobs.txt";

        public void AddEmployer(List<Employer> employers)
        {
            Console.Clear();
            Console.WriteLine("======Adding Employer======");
            Console.Write("Enter Employer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Contact Number: ");
            int contactNo = int.Parse(Console.ReadLine());

            Employer employer = new Employer
            {
                Name = name,
                ContactNo = contactNo
            };
            employers.Add(employer);
            SaveEmployerToFile(employer);

            Console.Clear();
            Console.WriteLine("*******Employer added successfully.*******");
            Thread.Sleep(1500);
            Console.Clear();
        }

        public void AddApplicant(List<Applicant> applicants)
        {
            Console.Clear();
            Console.WriteLine("======Adding Applicant======");
            Console.Write("Enter Applicant Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Applicant Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Resume Information: ");
            string resume = Console.ReadLine();

            string CV = null;
            Applicant applicant = new Applicant
            {
                Name = name,
                Email = email,
                CV = CV,
            };
            applicants.Add(applicant);
            SaveApplicantToFile(applicant);

            Console.Clear();
            Console.WriteLine("*******Applicant added successfully.*******");
            Thread.Sleep(1500);
            Console.Clear();
        }

        public void AddJob(List<Job> jobs)
        {
            Console.Clear();
            Console.WriteLine("======Listing A Job======");
            Console.Write("Enter Job Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Job Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Employer Name: ");
            string employerName = Console.ReadLine();

            Job job = new Job
            {
                Title = title,
                Description = description,
                EmployerName = employerName
            };

            jobs.Add(job);
            SaveJobToFile(job);

            Console.Clear();
            Console.WriteLine("*******Job added successfully.*******");
            Thread.Sleep(1500);
            Console.Clear();
        }

        public void ListEmployers(List<Employer> employers)
        {
            Console.Clear();
            Console.WriteLine("======List Of Employers======");
            if (employers == null || employers.Count == 0)
            {
                employers = LoadEmployersFromFile();
            }

            if (employers != null && employers.Count > 0)
            {
                foreach (var employer in employers)
                {
                    Console.WriteLine(employer);
                }
            }
            else
            {
                Console.WriteLine("******No employers found******");
                Thread.Sleep(1500);
            }
        }

        public void ListApplicants(List<Applicant> applicants)
        {
            Console.Clear();
            Console.WriteLine("======List Of Applicants======");
            if (applicants == null || applicants.Count == 0)
            {
                applicants = LoadApplicantsFromFile();
            }

            if (applicants != null && applicants.Count > 0)
            {
                foreach (var applicant in applicants)
                {
                    Console.WriteLine(applicant);
                }
            }
            else
            {
                Console.WriteLine("******No applicants found******");
                Thread.Sleep(1500);
            }
        }

        public void ListJobs(List<Job> jobs)
        {
            Console.Clear();
            Console.WriteLine("======List Of Jobs======");
            if (jobs == null || jobs.Count == 0)
            {
                jobs = LoadJobsFromFile();
            }

            if (jobs != null && jobs.Count > 0)
            {
                foreach (var job in jobs)
                {
                    Console.WriteLine(job);
                }
            }
            else
            {
                Console.WriteLine("******No job listings found******");
                Thread.Sleep(1500);
            }
        }

        private void SaveEmployerToFile(Employer employer)
        {
            using (StreamWriter sw = new StreamWriter(EmployerFilePath, true))
            {
                sw.WriteLine($"{employer.Name},{employer.ContactNo}");
            }
        }

        private void SaveApplicantToFile(Applicant applicant)
        {
            using (StreamWriter sw = new StreamWriter(ApplicantFilePath, true))
            {
                sw.WriteLine($"{applicant.Name},{applicant.Email},{applicant.CV}");
            }
        }

        private void SaveJobToFile(Job job)
        {
            using (StreamWriter sw = new StreamWriter(JobFilePath, true))
            {
                sw.WriteLine($"{job.Title},{job.Description},{job.EmployerName}");
            }
        }

        private List<Employer> LoadEmployersFromFile()
        {
            List<Employer> employers = new List<Employer>();

            if (File.Exists(EmployerFilePath))
            {
                string[] lines = File.ReadAllLines(EmployerFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        employers.Add(new Employer
                        {
                            Name = parts[0],
                            ContactNo = int.Parse(parts[1])
                        });
                    }
                }
            }

            return employers;
        }

        private List<Applicant> LoadApplicantsFromFile()
        {
            List<Applicant> applicants = new List<Applicant>();

            if (File.Exists(ApplicantFilePath))
            {
                string[] lines = File.ReadAllLines(ApplicantFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        applicants.Add(new Applicant
                        {
                            Name = parts[0],
                            Email = parts[1],
                            CV = parts[2]
                        });
                    }
                }
            }

            return applicants;
        }

        private List<Job> LoadJobsFromFile()
        {
            List<Job> jobs = new List<Job>();

            if (File.Exists(JobFilePath))
            {
                string[] lines = File.ReadAllLines(JobFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        jobs.Add(new Job
                        {
                            Title = parts[0],
                            Description = parts[1],
                            EmployerName = parts[2]
                        });
                    }
                }
            }

            return jobs;
        }
    }
}
