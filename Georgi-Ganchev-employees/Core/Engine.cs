using Georgi_Ganchev_employees.Core.Contracts;
using Georgi_Ganchev_employees.IO;
using Georgi_Ganchev_employees.IO.Contracts;
using Georgi_Ganchev_employees.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Georgi_Ganchev_employees.Core
{
    class Engine : IEngine
    {
        private IWriter writer;
        private FileReaderExampleInputFile reader;
        public Engine()
        {
            writer = new ConsoleWriter();
            reader = new FileReaderExampleInputFile();
        }
        public void Run()
        {
            try
            {
                List<string> lines = reader.ReadAllLines();

                List<Employee> employees = new List<Employee>();

                InputModeration(lines, employees);

                Dictionary<string[], int> employeesAndProjectDays = new Dictionary<string[], int>();
                GetEmployeesWithCommonProject(employees, employeesAndProjectDays);

                foreach (var kvp in employeesAndProjectDays)
                {
                    writer.WriteLine($"Employees: {String.Join(", ", kvp.Key)} -- Days: {kvp.Value}");
                }

                var result = employeesAndProjectDays.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

                writer.WriteLine($"Expected Result: {String.Join(" and ", result)}");
            }
            catch (Exception e)
            {
                writer.WriteLine(e.Message);
            }

        }

        private static void GetEmployeesWithCommonProject(List<Employee> employees, Dictionary<string[], int> employeesAndProjectDays)
        {
            for (int i = 0; i < employees.Count(); i++)
            {
                Employee employeeOne = employees[i];
                foreach (var employeeOneProject in employeeOne.projects)
                {
                    string projectOneId = employeeOneProject.Id;
                    for (int j = i; j < employees.Count(); j++)
                    {
                        Employee employeeTwo = employees[j];
                        if (employeeOne.Id == employeeTwo.Id)
                        {
                            continue;
                        }
                        if (employeeTwo.projects.Any(x => x.Id == projectOneId))
                        {
                            string[] employeeIds = new string[2] { employeeOne.Id, employeeTwo.Id };
                            var projectTwo = employeeTwo.projects.First(x => x.Id == projectOneId);

                            if (!employeesAndProjectDays.Keys.Any(x => (x.Contains(employeeIds[0]) && (x.Contains(employeeIds[1])))))
                            {
                                employeesAndProjectDays[employeeIds] = 0;
                            }

                            var key = employeesAndProjectDays.Keys.First(x => (x.Contains(employeeIds[0]) && x.Contains(employeeIds[1])));
                            
                            employeesAndProjectDays[key] += GetDaysOverlapped(employeeOneProject.StartDate, employeeOneProject.EndDate, projectTwo.StartDate, projectTwo.EndDate);
                        }
                    }
                }
            }
        }

        private static int GetDaysOverlapped(DateTime employeeOneStartDate, DateTime employeeOneEndDate, DateTime employeeTwoStartDate, DateTime employeeTwoEndDate)
        {
            TimeSpan time = new TimeSpan();
            if (employeeOneStartDate <= employeeTwoStartDate && employeeTwoStartDate <= employeeOneEndDate && employeeOneEndDate < employeeTwoEndDate)
            {
                time = employeeOneEndDate - employeeTwoStartDate;
            }
            else if (employeeTwoStartDate <= employeeOneStartDate && employeeOneStartDate <= employeeTwoEndDate && employeeTwoEndDate < employeeOneEndDate)
            {
                time = employeeTwoEndDate - employeeOneStartDate;
            }
            else if (employeeOneStartDate <= employeeTwoStartDate && employeeTwoEndDate <= employeeOneEndDate && employeeTwoStartDate < employeeTwoEndDate)
            {
                time = employeeTwoEndDate - employeeTwoStartDate;
            }
            else if (employeeTwoStartDate <= employeeOneStartDate && employeeOneEndDate <= employeeTwoEndDate && employeeOneStartDate < employeeOneEndDate)
            {
                time = employeeOneEndDate - employeeOneStartDate;
            }
            else
            {
                return 0;
            }
            return time.Days;
        }
        private static void InputModeration(List<string> lines, List<Employee> employees)
        {
            foreach (var line in lines)
            {
                string[] tokens = line.Split(", ");

                string employeeID = tokens[0];
                string projectID = tokens[1];
                DateTime startDate = DateTime.Parse(tokens[2]);
                DateTime endDate = tokens[3] == "NULL" ? DateTime.Now : DateTime.Parse(tokens[3]);

                if (!employees.Any(x => x.Id == employeeID))
                {
                    Employee employee = new Employee(employeeID);
                    employees.Add(employee);
                }

                Project project = new Project(projectID, startDate, endDate);
                employees.FirstOrDefault(x => x.Id == employeeID).AddProjcet(project);
            }
        }
    }
}
