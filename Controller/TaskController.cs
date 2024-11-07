using SE_Project.Helpers;
using SE_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Project.Controller
{
    internal class TaskController: IController
    {
        public TaskController()
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
            var task = model as TaskModel;
            if (task == null) return false;
            bool result = DBHelper.CreateTask(task.Name, task.Description, task.Project_id, task.Status, task.Due_date, task.User_id );
            if (result) items.Add(task);
            return result;
        }
        public bool Delete(IModel model)
        {
            return true;
        }

        public bool Delete(object id)
        {
            if (id == null) return false;

            bool result = DBHelper.DeleteTask((int)id);
            if (result) items.RemoveAll(p => ((TaskModel)p).Id == (int)id);
            return result;
        }

        // Sử dụng hàm GetTasksByProjectId ở đây
        public bool Load()
        {
            items.Clear();
            List<TaskModel> tasks = DBHelper.GetTasks();

            if (tasks == null) return false;

            foreach (var task in tasks)
            {
                var fullTasks = DBHelper.GetTasksByProjectId(task.Project_id);
                if (fullTasks != null)
                {
                    foreach (var fullTask in fullTasks)
                    {
                        items.Add((IModel)fullTask);
                    }
                }
            }

            return true;
        }

        public bool Load(object id)
        {
            items.Clear();
            List<TaskModel> tasks = DBHelper.GetTasks();

            if (tasks == null) return false;
            //var fullTasks = DBHelper.GetTasksByProjectId();
            //if (fullTasks != null)
            //{
            //    foreach (var fullTask in fullTasks)
            //    {
            //        items.Add((IModel)fullTask);
            //    }
            //}

            // Thêm tất cả các task vào danh sách items
            foreach (var task in tasks)
            {
                items.Add((IModel)task);
            }

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
        public bool IsProjectExists(int projectId)
        {
            // Thêm phương thức kiểm tra project tồn tại
            return DBHelper.GetProjectWithUserNameById(projectId) != null;
        }
    }
}
