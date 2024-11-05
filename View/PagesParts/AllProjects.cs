using Guna.UI2.AnimatorNS;
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
    public partial class AllProjects : UserControl
    {
        ProjectController projectController;
        public AllProjects()
        {
            InitializeComponent();
            projectController = new ProjectController();
            LoadProjects();
        }

        public string ProjectDesc { get; internal set; }
        public string ProjectTitle { get; internal set; }
        private void LoadProjects()
        {
            projectController.Load();

            if (projectController.Items.Count == 0)
            {
                MessageBox.Show("No projects were loaded.");
                return;
            }

            foreach (ProjectModel project in projectController.Items)
            {
                ProjectCard projectCard = new ProjectCard();
                projectCard.LoadData(project);
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
    }
}
