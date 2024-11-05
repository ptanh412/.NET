using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Project.Model
{
    public class ProjectModel : IModel
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
        public int user_id;
        public int User_id
        {
            get
            {
                return this.user_id;
            }
            set
            {
                this.user_id = value;
            }
        }
        public string project_id;
        public string Project_id
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
        public ProjectModel()
        {
        }
        public ProjectModel(string name, string description, int user_id)
        {
            this.name = name;
            this.description = description;
            this.user_id = user_id;
        }   
    }
}
