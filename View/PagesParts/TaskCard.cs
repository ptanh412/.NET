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
        ProjectController controller;
        public TaskCard()
        {
            InitializeComponent();
            controller = new ProjectController();
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProjectCard_Load(object sender, EventArgs e)
        {

        }
        public void LoadData(TaskModel task)
        {
            if (task != null)
            {
                TaskTitle = task.Name;
                TasktDesc = task.Description;
                TaskAssigned = task.Assigned;
                TaskDueDate = task.Due_date.ToString();
                guna2ComboBox1.Text = task.Status;
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
    }
}
