
using System.Collections.Generic;

namespace Georgi_Ganchev_employees.models
{
    public class Employee
    {
        private string id;
        public readonly List<Project> projects;
        public Employee(string id)
        {
            this.Id = id;
            this.projects = new List<Project>();
        }

        public string Id
        {
            get { return id; }
            private set { id = value; }
        }

        public void AddProjcet(Project project)
        {
            this.projects.Add(project);
        }
    }
}
