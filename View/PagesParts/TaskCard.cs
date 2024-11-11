using Guna.UI2.WinForms;
using SE_Project.Controller;
using SE_Project.Helpers;
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

namespace SE_Project.PagesParts
{
    public partial class TaskCard : UserControl
    {
        TaskController controller;
        public TaskCard()
        {
            InitializeComponent();
            controller = new TaskController();
        }
        public string TaskTitle
        {
            get { return CardTitle.Text; }
            set { CardTitle.Text = value; }
        }
        public string TasktDesc
        {
            get { return CardDesc.Text; }
            set { CardDesc.Text = value; }
        }
        public string TaskAssigned
        {
            get { return CardAssigned.Text; }
            set { CardAssigned.Text = value; }
        }
        public string TaskDueDate
        {
            get { return CardDueDate.Text; }
            set { CardDueDate.Text = value; }
        }
        public string ProjectName
        {
            get { return CardProjectName.Text; }
            set { CardProjectName.Text = value; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProjectCard_Load(object sender, EventArgs e)
        {

        }
        public void LoadData(TaskModel task, string projectName = null)
        {
            if (task != null)
            {
                TaskTitle = task.Name ?? "No title"; // Kiểm tra null để tránh lỗi
                TasktDesc = task.Description ?? "No description";
                TaskAssigned = task.Assigned ?? "Not assigned";
                TaskDueDate = task.Due_date.ToString("dd/MM/yyyy"); // Định dạng ngày tháng
                guna2ComboBox1.Text = task.Status ?? "Not set"; // Kiểm tra nếu Status null
                if (!string.IsNullOrEmpty(projectName))
                {
                    ProjectName = projectName;
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(guna2ComboBox1.SelectedItem.ToString());
            //MessageBox.Show("Operation Successful");
        }

        private void guna2ImageRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Operation Successful");
        }

        private void btn_UpdateTask_Click(object sender, EventArgs e)
        {

        }
    }
}
