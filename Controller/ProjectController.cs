using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool Load()
        {
            items.Clear();
            List<ProjectModel> projects = DBHelper.GetProjects();

            if (projects == null) return false;

            items.AddRange(projects);
            return true;
        }

        public bool Load(object id)
        {
            return true;
        }

        public IModel Read(object id)
        {
            if (id == null) return null;

            ProjectModel project = DBHelper.GetProjectById((int)id);
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

