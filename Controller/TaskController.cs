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

            // Kiểm tra project_id có tồn tại trước khi tạo task
            if (!IsProjectExists(task.Project_id))
            {
                return false;
            }

            bool result = DBHelper.CreateTask(
                task.Name,
                task.Description,
                task.Project_id,
                task.Status,
                task.Due_date,
                task.User_id
            );

            if (result) items.Add(task);
            return result;
        }
        public bool Delete(IModel model)
        {
            var task = model as TaskModel;
            if (task == null) return false;

            return Delete(task.Id);
        }

        public bool Delete(object id)
        {
            if (id == null) return false;

            bool result = DBHelper.DeleteTask((int)id);
            if (result) items.RemoveAll(p => ((TaskModel)p).Id == (int)id);
            return result;
        }

        public bool Load()
        {
            items.Clear();
            List<TaskModel> tasks = DBHelper.GetTasks();

            if (tasks == null) return false;

            foreach (var task in tasks)
            {
                items.Add(task);
            }
            return true;
        }

        public bool Load(object projectId)
        {
            items.Clear();
            List<TaskModel> tasks = DBHelper.GetTasksByProjectId((int)projectId);

            if (tasks == null) return false;

            foreach (var task in tasks)
            {
                items.Add(task);
            }
            return true;
        }


        public IModel Read(object id)
        {
            var task = DBHelper.GetTasks().Find(t => t.Id == (int)id);
            return task;
        }

        public bool Update(IModel model)
        {
            var task = model as TaskModel;
            if (task == null) return false;

            bool result = DBHelper.UpdateTask(
                task.Id,
                task.Name,
                task.Description,
                task.Project_id,
                task.Status,
                task.Due_date,
                task.User_id
            );

            if (result)
            {
                var existingTask = items.Find(p => ((TaskModel)p).Id == task.Id) as TaskModel;
                if (existingTask != null)
                {
                    existingTask.Name = task.Name;
                    existingTask.Description = task.Description;
                    existingTask.Project_id = task.Project_id;
                    existingTask.Status = task.Status;
                    existingTask.Due_date = task.Due_date;
                    existingTask.User_id = task.User_id;
                }
            }
            return result;
        }

        public bool IsExist(object id)
        {
            return items.Exists(p => ((TaskModel)p).Id == (int)id);
        }

        public bool IsExist(IModel model)
        {
            var task = model as TaskModel;
            if (task == null) return false;

            return IsExist(task.Id);
        }

        public bool IsProjectExists(int projectId)
        {
            // Giả sử có một phương thức kiểm tra tồn tại của project
            // Bạn có thể triển khai trong DBHelper
            return DBHelper.CheckProjectExists(projectId);
        }
    }
}
