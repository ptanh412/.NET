using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Project.Model
{
    public class TaskModel: IModel
    {
       public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Due_date { get; set; }
        public int Project_id { get; set; }
        public string ProjectName { get; set; }
        public int User_id { get; set; }
        public string Assigned { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }

    }
}
