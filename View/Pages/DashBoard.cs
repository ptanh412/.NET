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

namespace SE_Project.Pages
{

    public partial class DashBoard : UserControl
    {
        ProjectController projectController;
        public DashBoard()
        {

            InitializeComponent();
            projectController = new ProjectController();
            projectController.Load();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            var projects = ((IController)projectController).Items
                                .Cast<ProjectModel>()
                                .ToList().Count();
            txt_completed.Text = projects.ToString();
            txt_inprogress.Text = projects.ToString();
            txt_total.Text = projects.ToString();

        }

        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void userControl11_Load_1(object sender, EventArgs e)
        {

        }
    }
}
