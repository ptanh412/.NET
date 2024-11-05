using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Project.Model
{
    public class TaskModel: IModel
    {
        public int id;
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public string description;
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        public int project_id;
        public int Project_id
        {
            get
            {
                return this.project_id;
            }
            set
            {
                this.project_id = value;
            }
        }
        public int user_id;
        public int User_id
        {
            get { return this.user_id; }
            set { this.user_id = value; }
        }
        public string status;
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        public DateTime due_date;
        public DateTime Due_date
        {
            get { return this.due_date; }
            set { this.due_date = value; }
        }
        public string assigned;
        public string Assigned
        {
            get { return this.assigned; }
            set { this.assigned = value; }
        }
        public TaskModel(string name, string description, int project_id, int user_id, string status, DateTime due_date)
        {
            this.name = name;
            this.description = description;
            this.project_id = project_id;
            this.user_id = user_id;
            this.status = status;
            this.due_date = due_date;
        }
        public TaskModel() { }
    }
}
