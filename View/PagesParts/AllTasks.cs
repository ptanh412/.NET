using SE_Project.Controller;
using SE_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Project.View.PagesParts
{
    public partial class AllTasks : UserControl
    {
        private int projectId;

        public AllTasks()
        {
            InitializeComponent();

            // Đảm bảo rằng panel chứa task có thể cuộn khi vượt quá chiều cao
            pnAllCardTask.Dock = DockStyle.Fill; // Đảm bảo Panel chiếm toàn bộ diện tích
            pnAllCardTask.AutoScroll = true; // Bật thanh cuộn khi nội dung vượt quá kích thước Panel
        }

        // Phương thức để cập nhật danh sách task trong AllTaskView
        public void UpdateTaskView(List<TaskModel> tasks)
        {
            // Xóa các CardTask cũ nếu có
            pnAllCardTask.Controls.Clear();

            // Duyệt qua các task và tạo CardTask cho mỗi task
            foreach (var task in tasks)
            {
                // Tạo một CardTask mới cho mỗi task
                TaskCard taskCard = new TaskCard(task);

                // Thêm CardTask vào panel
                pnAllCardTask.Controls.Add(taskCard);
            }
        }
    }
}

