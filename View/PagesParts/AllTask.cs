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

                // Kiểm tra xem có projectId hay không và lấy tên project tương ứng
                string projectName = null;
                if (projectId > 0)
                {
                    var project = DBHelper.GetProjectById(projectId);
                    projectName = project?.Name ?? "Unknown Project"; // Lấy tên project nếu có projectId
                }

                // Sử dụng TaskController để load tasks theo projectId
                bool tasksLoaded = (projectId > 0) ? taskController.Load(projectId) : taskController.Load();

                if (!tasksLoaded || taskController.Items.Count == 0)
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

                // Thêm các task vào AllProjectsPanel và truyền projectName vào từng task
                foreach (TaskModel task in taskController.Items)
                {
                    TaskCard taskCard = new TaskCard();

                    // Nếu không có projectId (tức là đang load tất cả các task), lấy tên project cho từng task
                    string taskProjectName = projectId > 0 ? projectName : DBHelper.GetProjectById(task.Project_id)?.Name;

                    // Truyền task và projectName (taskProjectName) vào LoadData
                    taskCard.LoadData(task, taskProjectName);
                    AllProjectsPanel.Controls.Add(taskCard);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AllProjects_Load(object sender, EventArgs e)
        {
            AllProjectsPanel.Controls.Clear();
            LoadTasksForProject();
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
        }
        private void addUserControl2(UserControl userControl)
        {

            userControl.Dock = DockStyle.Fill;
            userControl.BringToFront(); // Ensure the user control is at the front of panel2
        }


        private void btn_AddTask_Click(object sender, EventArgs e)
        {
            if (projectId <= 0)
            {
                MessageBox.Show("Invalid project selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddTasks addTaskForm = new AddTasks(projectId)
            {
                StartPosition = FormStartPosition.CenterScreen,
                TopLevel = true,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            addTaskForm.TaskAdded += OnTaskAdded;

            addTaskForm.FormClosed += (s, args) => LoadTasksForProject(); // Refresh danh sách task khi form đóng
            addTaskForm.ShowDialog();
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
