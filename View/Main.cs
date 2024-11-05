using SE_Project.Pages;
using SE_Project.PagesParts;
using System;
using System.Windows.Forms;

namespace SE_Project
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            DashBoard dashboard = new DashBoard();
            addUserControl(dashboard);
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Top;
            userControl.BringToFront();
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(userControl);

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DashBoard dashboard = new DashBoard();
            addUserControl(dashboard);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Projects projects = new Projects();
            addUserControl(projects);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ToDo toDo = new ToDo();
            addUserControl(toDo);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Starred starred = new Starred();
            addUserControl(starred);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                var result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
                return true; // Indicate that the key has been handled
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
