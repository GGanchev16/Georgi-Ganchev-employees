

using System;

namespace Georgi_Ganchev_employees.models
{
    public class Project
    {
        private string id;
        private DateTime startDate;
        private DateTime endDate;

        public Project(string Id, DateTime startDate, DateTime endDate)
        {
            this.Id = Id;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public DateTime EndDate
        {
            get { return this.endDate; }
            private set
            {
                if (value < this.StartDate)
                {
                    throw new ArgumentException("Project start date is after the end date!");
                }
                this.endDate = value;
            }
        }

        public DateTime StartDate
        {
            get { return this.startDate; }
            private set
            {
                this.startDate = value;
            }
        }

        public string Id
        {
            get { return this.id; }
            private set { this.id = value; }
        }

    }
}
