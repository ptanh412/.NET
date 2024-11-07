using Guna.UI2.WinForms;
using SE_Project.Controller;
using SE_Project.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SE_Project.Forms
{
    public partial class DeleteProjects : UserControl
    {
        DBHelper db; // Instantiate the DBHelper class
        public event EventHandler RequestPanelBack;
        public event EventHandler ProjectDeleted;
        public DeleteProjects()
        {
            InitializeComponent();
            db = new DBHelper();
            this.guna2ImageButton1.Click += guna2ImageButton1_Click;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Dispose(); // Use Dispose if you want to completely remove the UserControl
            RequestPanelBack?.Invoke(this, EventArgs.Empty);
        }

        private void DeleteProjects_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string projectName = guna2TextBox1.Text.Trim();

            if (string.IsNullOrEmpty(projectName))
            {
                MessageBox.Show("Vui lòng nhập tên dự án.");
                return;
            }

            // Gọi phương thức Delete từ ProjectController
            ProjectController controller = new ProjectController();
            bool result = controller.DeleteByName(projectName);

            if (result)
            {
                MessageBox.Show("Dự án đã được xóa thành công.");
                ProjectDeleted?.Invoke(this, EventArgs.Empty); // Gọi sự kiện ProjectDeleted
            }
            else
            {
                MessageBox.Show("Không thể xóa dự án. Vui lòng kiểm tra lại tên dự án.");
            }
        }

        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        {
            this.Dispose(); // Use Dispose if you want to completely remove the UserControl
            RequestPanelBack?.Invoke(this, EventArgs.Empty);
        }
    }
}
