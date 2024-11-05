using Guna.UI2.WinForms;
using SE_Project.Controller;
using SE_Project.Helpers;
using SE_Project.Model;
using SE_Project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Project.Forms
{
    public partial class AddProjects : UserControl, IView
    {
        ProjectController projectController = new ProjectController();

        public event EventHandler RequestPanelBack;
        public AddProjects()
        {
            InitializeComponent();
            
            this.guna2ImageButton1.Click += guna2ImageButton1_Click;
        }
        public void SetDataToText(object item)
        {
            if (item is ProjectModel project)
            {
                guna2TextBox1.Text = project.Name;
                guna2TextBox2.Text = project.Description;
            }
        }

        public object GetDataFromText()
        {
            return new ProjectModel
            {
                Name = guna2TextBox1.Text,
                Description = guna2TextBox2.Text
            };
        }
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Dispose(); // Use Dispose if you want to completely remove the UserControl
            RequestPanelBack?.Invoke(this, EventArgs.Empty);

        }

        private void AddProjects_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var project = new ProjectModel
                {
                    Name = guna2TextBox1.Text,
                    Description = guna2TextBox2.Text,
                    User_id = Session.UserId,
                };
                bool isSuccessful = projectController.Create(project);
                if (isSuccessful)
                {
                    MessageBox.Show("Project added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("An error occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
