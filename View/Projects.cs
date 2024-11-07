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
        private int selectedRow;
        private int lastSelectedRow = -1;

        public Projects()
        {
            InitializeComponent();
            AllTask project_1 = new AllTask(selectedRow);
            projectController = new ProjectController();
            addUserControl(project_1);
            projectController.Load();
            LoadProjects();
            projectList.SelectionChanged += projectList_SelectionChanged;
            projectList.CellClick += projectList_CellClick;
            UpdateTaskView();
        }
        public int GetSelectedProjectId()
        {
            return selectedRow;
        }
        private void UpdateTaskView()
        {
            if (selectedRow != -1)
            {
                AllTask projectTasks = new AllTask(selectedRow);
                addUserControl(projectTasks);
            }
        }
        private bool IsRowValid(DataGridViewRow row)
        {
            return row != null &&
                   row.Cells["ID"].Value != null &&
                   !string.IsNullOrWhiteSpace(row.Cells["ID"].Value.ToString());
        }
        private void projectList_SelectionChanged(object sender, EventArgs e)
        {
            if (projectList.SelectedRows.Count > 0)
            {
                DataGridViewRow row = projectList.SelectedRows[0];
                if (IsRowValid(row))
                {
                    selectedRow = Convert.ToInt32(row.Cells["ID"].Value);
                    UpdateTaskView();
                }
            }
        }
        private void projectList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < projectList.Rows.Count)
            {
                DataGridViewRow row = projectList.Rows[e.RowIndex];
                if (IsRowValid(row))
                {
                    selectedRow = Convert.ToInt32(row.Cells["ID"].Value);
                    UpdateTaskView();
                }
            }
        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra nếu có hàng nào đang được chọn
            if (selectedRow == -1)
            {
                MessageBox.Show("Please select a project to delete.");
                return;
            }

            // Xác nhận xóa với người dùng
            DialogResult result = MessageBox.Show("Are you sure you want to delete this project?",
                                                  "Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Gọi ProjectController để xóa project khỏi cơ sở dữ liệu
                bool deleteSuccess = projectController.Delete(selectedRow);

                if (deleteSuccess)
                {
                    // Tìm và xóa hàng khỏi projectList
                    foreach (DataGridViewRow row in projectList.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["ID"].Value) == selectedRow)
                        {
                            projectList.Rows.Remove(row);
                            break;
                        }
                    }

                    selectedRow = -1;
                    UpdateTaskView();
                    MessageBox.Show("Project deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to delete project. Please try again.");
                }
            }
        }


        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            userControl.BringToFront();
            guna2Panel3.Controls.Clear();
            guna2Panel3.Controls.Add(userControl);

        }

        public void LoadProjects()
        {
            var projects = ((IController)projectController).Items
                                .Cast<ProjectModel>()
                                .ToList();
            projectList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            projectList.RowTemplate.Height = 30;

            projectList.DataSource = null;
            projectList.DataSource = projects.Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.CreatedAt,
                p.CreatorName,
                ParticipantsNames = string.Join(", ", p.Participants.Select(pt => pt.Name))
            }).ToList();

            if (projectList.Columns.Count > 0)
            {
                projectList.Columns["ID"].HeaderText = "ID";
                projectList.Columns["Name"].HeaderText = "Project Name";
                projectList.Columns["Description"].HeaderText = "Description";
                projectList.Columns["CreatedAt"].HeaderText = "Created At";
                projectList.Columns["CreatorName"].HeaderText = "Creator Name";
                projectList.Columns["ParticipantsNames"].HeaderText = "Participants";
            }

            projectList.ClearSelection();
            selectedRow = -1;
        }


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AllTask allProjects = new AllTask(selectedRow);
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

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }


    }
}
