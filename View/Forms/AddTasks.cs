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

namespace SE_Project.View.Forms
{
    public partial class AddTasks : Form, IView
    {
        TaskController taskController;
        private int projectId;
        public event EventHandler TaskAdded;
        private List<UserModel> allUsers;
        private List<UserModel> selectedUsers;
        public AddTasks(int projectId)
        {
            InitializeComponent();
            this.projectId = projectId;
            this.taskController = new TaskController();
            this.Text = "Add New Task";
            this.Size = new Size(400, 600);

            // Khởi tạo Guna2ComboBox và thêm vào form
            LoadUsers();  // Tải danh sách người dùng
        }

        private void LoadUsers()
        {
            try
            {
                // Lấy danh sách người dùng chỉ thuộc về dự án hiện tại
                allUsers = DBHelper.GetUsersByProjectId(projectId);

                // Thêm người dùng vào Guna2ComboBox
                guna2ComboBox1.Items.Clear();
                foreach (var user in allUsers)
                {
                    guna2ComboBox1.Items.Add(user.Name); // Thêm tên người dùng vào ComboBox
                }

                // Chọn người dùng đầu tiên (nếu có)
                if (guna2ComboBox1.Items.Count > 0)
                {
                    guna2ComboBox1.SelectedIndex = 0; // Chọn mặc định người đầu tiên
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void SetDataToText(object item)
        {
            if (item is TaskModel task)
            {
                guna2TextBox1.Text = task.Name;
                guna2TextBox2.Text = task.Description;
                dateTimePicker1.Value = task.Due_date;
                // Set giá trị của ComboBox cho người đã được gán trong Task
                var user = allUsers.FirstOrDefault(u => u.ID == task.User_id);
                if (user != null && guna2ComboBox1.Items.Contains(user.Name))
                {
                    guna2ComboBox1.SelectedItem = user.Name;
                }

            }
        }

        public object GetDataFromText()
        {
            // Lấy người dùng được chọn từ Guna2ComboBox
            UserModel selectedParticipant = allUsers.FirstOrDefault(u => u.Name == guna2ComboBox1.SelectedItem?.ToString());
            // Trả về TaskModel với người dùng được chọn
            return new TaskModel
            {
                Name = guna2TextBox1.Text,
                Description = guna2TextBox2.Text,
                Due_date = dateTimePicker1.Value,
                Project_id = projectId,
                Status = "todo",
                User_id = selectedParticipant != null ? selectedParticipant.ID : 0 // Gán ID người dùng được chọn (hoặc 0 nếu không chọn)
            };
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
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
                {
                    MessageBox.Show("Task name is required!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UserModel selectedUser = allUsers.FirstOrDefault(u => u.Name == guna2ComboBox1.SelectedItem?.ToString());
                if (selectedUser == null)
                {
                    MessageBox.Show("Please select a user to assign the task!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var task = (TaskModel)GetDataFromText();
                if (task == null)
                {
                    MessageBox.Show("Failed to retrieve task data. Please check inputs.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string debugInfo = $"Debug Info:\n" +
                                   $"Task Name: {task.Name}\n" +
                                   $"Description: {task.Description}\n" +
                                   $"Project ID: {task.Project_id}\n" +
                                   $"Status: {task.Status}\n" +
                                   $"Due Date: {task.Due_date}\n" +
                                   $"User ID: {task.User_id}";

                MessageBox.Show(debugInfo, "Debug Info");
                bool result = taskController.Create(task);
                if (result)
                {
                    MessageBox.Show("Task added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TaskAdded?.Invoke(this, EventArgs.Empty);

                    guna2TextBox1.Clear();
                    guna2TextBox2.Clear();
                    dateTimePicker1.Value = DateTime.Now;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add task!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
