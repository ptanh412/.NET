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
    public partial class ProjectCard : UserControl
    {
        ProjectController controller;
        public ProjectCard()
        {
            InitializeComponent();
            controller = new ProjectController();
        }
        public string ProjectTitle
        {
            get { return CardTitle.Text; }
            set { CardTitle.Text = value; }
        }
        public string ProjectDesc
        {
            get { return CardDesc.Text; }
            set { CardDesc.Text = value; }
        }
        public string ProjectCre
        {
            get { return CardCreatedBy.Text; }
            set { CardCreatedBy.Text = value; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProjectCard_Load(object sender, EventArgs e)
        {

        }
        public void LoadData(ProjectModel project)
        {
            if (project != null)
            {
                ProjectTitle = project.Name;
                ProjectDesc = project.Description;
                ProjectCre = project.User_id.ToString();
            }
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Operation Successful");
        }

        private void guna2ImageRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Operation Successful");
        }
    }
}
