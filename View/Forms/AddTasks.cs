using SE_Project.Controller;
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

namespace SE_Project.View.Forms
{
    public partial class AddTasks : UserControl
    {
        TaskController taskController = new TaskController();
        private int projectId;
        public AddTasks(int projectId)
        {
            InitializeComponent();
            this.projectId = projectId;
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void AddTasks_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

            try
            {
                MessageBox.Show(projectId.ToString());
                var project = new TaskModel
                {
                    Name = guna2TextBox1.Text,
                    Description = guna2TextBox2.Text,
                    User_id = Session.UserId,
                    Assigned = Session.Name,
                    Status = "todo",
                    Due_date = dateTimePicker1.Value,
                    Project_id = projectId
                }; 
                bool isSuccessful = taskController.Create(project);
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
    }
}
