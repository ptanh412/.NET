using Guna.UI2.AnimatorNS;
using Guna.UI2.WinForms;
using SE_Project.Controller;
using SE_Project.Forms;
using SE_Project.Helpers;
using SE_Project.Model;
using SE_Project.View.Forms;
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
    public partial class AllProjects : UserControl
    {
        TaskController taskController;
        public event EventHandler RequestPanelBack;
        private int projectId;
        public AllProjects(int projectId)
        {
            InitializeComponent();
            taskController = new TaskController();
            LoadProjects();
            this.projectId = projectId;
        }

        public string ProjectDesc { get; internal set; }
        public string ProjectTitle { get; internal set; }
        private void LoadProjects()
        {
            taskController.Load();

            if (taskController.Items.Count == 0)
            {
                MessageBox.Show("No projects were loaded.");
                return;
            }

            foreach (TaskModel task in taskController.Items)
            {
                ProjectCard projectCard = new ProjectCard();
                projectCard.LoadData(task);
                AllProjectsPanel.Controls.Add(projectCard);
            }
        }


        private void AllProjects_Load(object sender, EventArgs e)
        {
            AllProjectsPanel.Controls.Clear();
            LoadProjects();
        }

        private void PanelProjectsAll_Paint(object sender, PaintEventArgs e)
        {

        }

        private void projectCard2_Load(object sender, EventArgs e)
        {

        }

        private void projectCard5_Load(object sender, EventArgs e)
        {

        }

        private void projectCard6_Load(object sender, EventArgs e)
        {

        }

        private void projectCard4_Load(object sender, EventArgs e)
        {

        }

        private void projectCard3_Load(object sender, EventArgs e)
        {

        }

        private void projectCard1_Load(object sender, EventArgs e)
        {

        }

        private void AllProjectsPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Add_Project_RequestPanelBack(object sender, EventArgs e)
        {
            // Logic to send panel2 to the back
            panel1.SendToBack();
        }
        private void addUserControl2(UserControl userControl)
        {
            panel1.Controls.Clear(); // Clear existing controls
            panel1.Controls.Add(userControl); // Add the new user control
            userControl.Dock = DockStyle.Fill;
            userControl.BringToFront(); // Ensure the user control is at the front of panel2
            panel1.BringToFront();  // This will bring the panel2 to the front, ensuring visibility
        }


        private void btn_AddTask_Click(object sender, EventArgs e)
        {

           
            AddTasks addProjects = new AddTasks(projectId);
            addUserControl2(addProjects);
            //addProjects.RequestPanelBack += Add_Project_RequestPanelBack;
        }
    }
}
