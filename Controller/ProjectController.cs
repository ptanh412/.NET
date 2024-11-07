using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SE_Project.Helpers;
using SE_Project.Model;

namespace SE_Project.Controller
{
    internal class ProjectController : IController
    {
        public ProjectController()
        {
            items = new List<IModel>(); // Khởi tạo danh sách ở đây
        }
        private List<IModel> items;
        public List<IModel> Items
        {
            get
            {
                return this.items;
            }

        }

        public bool Create(IModel model)
        {
            var project = model as ProjectModel;
            if (project == null) return false;
            bool result = DBHelper.CreateProject(project.Name, project.Description, project.User_id);
            if (result) items.Add(project);

            return result;
        }
        public bool Delete(IModel model)
        {
            return true;
        }

        public bool Delete(object id)
        {
            if (id == null) return false;

            bool result = DBHelper.DeleteProject((int)id);
            if (result) items.RemoveAll(p => ((ProjectModel)p).Id == (int)id);
            return result;
        }

        public bool DeleteByName(string projectName)
        {
            if (string.IsNullOrEmpty(projectName)) return false;

            // Tìm ID của dự án dựa trên tên dự án
            int projectId = DBHelper.GetProjectIdByName(projectName);

            // Kiểm tra nếu không tìm thấy ID (ví dụ tên dự án không tồn tại trong cơ sở dữ liệu)
            if (projectId == -1)
            {
                MessageBox.Show("Dự án không tồn tại.");
                return false;
            }

            // Gọi phương thức DeleteProject để xóa dự án theo ID
            bool result = DBHelper.DeleteProject(projectId);
            if (result)
            {
                // Nếu xóa thành công, loại bỏ dự án khỏi danh sách items
                items.RemoveAll(p => ((ProjectModel)p).Id == projectId);
            }
            return result;
        }

        public bool Load()
        {
            items.Clear();
            List<ProjectModel> projects = DBHelper.GetProjects();

            if (projects == null) return false;

            foreach (var project in projects)
            {
                // Lấy thông tin dự án cùng với tên người dùng
                var fullProject = DBHelper.GetProjectWithUserNameById(project.Id);
                if (fullProject != null)
                {
                    items.Add(fullProject); // Thêm dự án đã có tên người dùng vào danh sách
                }
            }
            return true;
        }

        // Hàm để lấy dữ liệu dự án cùng với tên người dùng
        public DataTable GetProjectDataWithUserNames()
        {
            // Gọi hàm từ DBHelper để lấy dữ liệu dự án kèm tên người dùng
            return DBHelper.GetProjectWithUserName();
        }

        public bool Load(object id)
        {
            return true;
        }

        public IModel Read(object id)
        {
            if (id == null) return null;

            ProjectModel project = DBHelper.GetProjectWithUserNameById((int)id);
            return project;
        }

        public bool Update(IModel model)
        {
            var project = model as ProjectModel;
            if (project == null) return false;

            bool result = DBHelper.UpdateProject(project.Id, project.Name, project.Description, project.User_id);
            if (result)
            {
                int index = items.FindIndex(p => ((ProjectModel)p).Id == project.Id);
                if (index >= 0) items[index] = project;
            }

            return result;
        }
        public bool IsExist(Object id)
        {
            return true;
        }
        public bool IsExist(IModel model)
        {
            return true;
        }
    }
}

