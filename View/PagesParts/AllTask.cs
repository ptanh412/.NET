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
    public partial class AllTask : UserControl
    {
        TaskController taskController;
        public event EventHandler RequestPanelBack;
        private int projectId;
        public AllTask(int projectId)
        {
            InitializeComponent();
            taskController = new TaskController();
            LoadProjects();
            LoadTasksForProject();
            this.projectId = projectId;
            AllProjectsPanel.BringToFront();
        }

        public string ProjectDesc { get; internal set; }
        public string ProjectTitle { get; internal set; }
        private void LoadTasksForProject()
        {
            try
            {
                AllProjectsPanel.Controls.Clear();

                // Sửa lại phương thức Load để chỉ lấy task của project hiện tại
                var tasks = DBHelper.GetTasksByProjectId(projectId);

                if (tasks == null || tasks.Count == 0)
                {
                    Label noTasksLabel = new Label
                    {
                        Text = "No tasks found for this project",
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Padding = new Padding(0, 20, 0, 0)
                    };
                    AllProjectsPanel.Controls.Add(noTasksLabel);
                    return;
                }

                foreach (TaskModel task in tasks)
                {
                    TaskCard taskCard = new TaskCard();
                    taskCard.LoadData(task);
                    AllProjectsPanel.Controls.Add(taskCard);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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
                TaskCard projectCard = new TaskCard();
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
        }
        private void addUserControl2(UserControl userControl)
        {

            userControl.Dock = DockStyle.Fill;
            userControl.BringToFront(); // Ensure the user control is at the front of panel2
        }


        //private void addFormControl(Form formControl)
        //{
        //    formControl.TopLevel = false; // Set form as non-top level
        //    formControl.FormBorderStyle = FormBorderStyle.None; // Remove form border
        //    formControl.Dock = DockStyle.Fill; // Dock form to fill panel
        //    formControl.Show(); // Show form
        //}

        private void btn_AddTask_Click(object sender, EventArgs e)
        {
            if (projectId <= 0)
            {
                MessageBox.Show("Invalid project selected", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Tạo instance của AddTasks form
                AddTasks addTaskForm = new AddTasks(projectId)
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    TopLevel = true,
                    FormBorderStyle = FormBorderStyle.FixedDialog, // hoặc FormBorderStyle style khác tùy bạn
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                // Đăng ký event handler
                addTaskForm.TaskAdded += OnTaskAdded;

                // Xử lý khi form đóng
                addTaskForm.FormClosed += (s, args) =>
                {
                    LoadTasksForProject(); // Refresh task list khi form đóng
                };

                // Show form dưới dạng dialog
                addTaskForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing add task form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler khi task mới được thêm
        private void OnTaskAdded(object sender, EventArgs e)
        {
            LoadTasksForProject(); // Refresh danh sách task
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
