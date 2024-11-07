using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Project.Model
{
    public class TaskModel: IModel
    {
        public int Id { get; set; } // ID của task
        public string Name { get; set; } // Tên của task
        public int User_id { get; set; } // ID của user liên quan
        public string Description { get; set; } // Mô tả task
        public int Project_id { get; set; } // ID của dự án liên quan
        public string Status { get; set; } // Trạng thái của task (todo, inprogress, completed, starred)
        public DateTime Due_date { get; set; } // Ngày hết hạn của task
        public DateTime CreatedAt { get; set; } // Ngày tạo task
        public string ProjectName  { get; set; } // Dự án liên quan
        public string Assigned { get; set; } // User liên quan
    }
}
