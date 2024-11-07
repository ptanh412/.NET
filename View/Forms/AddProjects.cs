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
        UserController userController = new UserController();
        private List<UserModel> allUsers;
        private List<UserModel> selectedUsers;

        public event EventHandler RequestPanelBack;
        public AddProjects()
        {
            InitializeComponent();
            this.guna2ImageButton1.Click += guna2ImageButton1_Click;
            CheckedListBox checkedListUsers = new CheckedListBox
            {
                Name = "checkedListUsers",
                Location = new Point(57, 360),
                Size = new Size(215, 52),
            };
            this.Controls.Add(checkedListUsers);
            LoadUsers();
        }
        private void LoadUsers()
        {
            try
            {
                // Lấy danh sách tất cả người dùng trừ người đang đăng nhập
                allUsers = DBHelper.GetAllUsers().Where(u => u.ID != Session.UserId).ToList();
                var checkedListUsers = (CheckedListBox)this.Controls["checkedListUsers"];
                checkedListUsers.Items.Clear();

                foreach (var user in allUsers)
                {
                    checkedListUsers.Items.Add(user.Name); // Thêm tên người dùng vào CheckedListBox
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetDataToText(object item)
        {
            if (item is ProjectModel project)
            {
                txt_title.Text = project.Name;
                txt_des.Text = project.Description;
            }
        }

        public object GetDataFromText()
        {
            var checkedListUsers = (CheckedListBox)this.Controls["checkedListUsers"];
            List<UserModel> selectedParticipants = new List<UserModel>();

            // Lấy danh sách người dùng được chọn
            for (int i = 0; i < checkedListUsers.Items.Count; i++)
            {
                if (checkedListUsers.GetItemChecked(i))
                {
                    selectedParticipants.Add(allUsers[i]);
                }
            }

            return new ProjectModel
            {
                Name = txt_title.Text,
                Description = txt_des.Text,
                CreatedBy = Session.UserId,
                Participants = selectedParticipants
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
                // Validate input
                if (string.IsNullOrWhiteSpace(txt_title.Text))
                {
                    MessageBox.Show("Please enter project name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var project = (ProjectModel)GetDataFromText();
                bool isSuccessful = projectController.Create(project);

                if (isSuccessful)
                {
                    MessageBox.Show("Project added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_title.Clear();
                    txt_des.Clear();
                    var checkedListUsers = (CheckedListBox)this.Controls["checkedListUsers"];
                    for (int i = 0; i < checkedListUsers.Items.Count; i++)
                    {
                        checkedListUsers.SetItemChecked(i, false);
                    }
                    RequestPanelBack?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("An error occurred while adding the project", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_title_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_des_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
