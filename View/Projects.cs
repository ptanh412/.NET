using SE_Project.Forms;
using SE_Project.PagesParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SE_Project.Model;
using SE_Project.Controller;

namespace SE_Project
{
    public partial class Projects : UserControl
    {
        ProjectController projectController;
        public Projects()
        {
            InitializeComponent();
            AllProjects project_1 = new AllProjects();
            projectController = new ProjectController();
            addUserControl(project_1);
            projectController.Load();
            LoadProjects();
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            userControl.BringToFront();
            guna2Panel3.Controls.Clear();
            guna2Panel3.Controls.Add(userControl);

        }

        private void LoadProjects()
        {
            var projects = ((IController)projectController).Items
                                .Cast<ProjectModel>()
                                .ToList();
            projectList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            projectList.RowTemplate.Height = 30;

            projectList.DataSource = null;

            projectList.DataSource = projects;

            if (projectList.Columns.Count > 0)
            {
                projectList.Columns["Id"].HeaderText = "ID";
                projectList.Columns["Name"].HeaderText = "Project Name";
                projectList.Columns["Description"].HeaderText = "Description";
                projectList.Columns["User_id"].HeaderText = "User ID";
            }

            projectList.ClearSelection();

        }


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AllProjects allProjects = new AllProjects();
            addUserControl(allProjects);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ProjectsCompleted projectsCompleted = new ProjectsCompleted();
            addUserControl(projectsCompleted);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ProjectsInProgress projectsInProgress = new ProjectsInProgress();
            addUserControl(projectsInProgress);
        }

        private void addUserControl2(UserControl userControl)
        {
            guna2Panel2.Controls.Clear(); // Clear existing controls
            guna2Panel2.Controls.Add(userControl); // Add the new user control
            userControl.Dock = DockStyle.Fill;
            userControl.BringToFront(); // Ensure the user control is at the front of panel2
            guna2Panel2.BringToFront();  // This will bring the panel2 to the front, ensuring visibility
        }

        private void Add_Project_RequestPanelBack(object sender, EventArgs e)
        {
            // Logic to send panel2 to the back
            guna2Panel2.SendToBack();
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            AddProjects addProjects = new AddProjects();
            addUserControl2(addProjects);
            addProjects.RequestPanelBack += Add_Project_RequestPanelBack;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            DeleteProjects deleteProjects = new DeleteProjects();
            addUserControl2(deleteProjects);
            deleteProjects.RequestPanelBack += Add_Project_RequestPanelBack;
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void projectList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            AddProjects addProjects = new AddProjects();
            addUserControl2(addProjects);
            addProjects.RequestPanelBack += Add_Project_RequestPanelBack;
        }

        private void projectList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
