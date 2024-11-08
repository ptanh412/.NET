﻿using System;
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
        private List<IModel> items;

        public ProjectController()
        {
            items = new List<IModel>(); // Khởi tạo danh sách dự án
        }

        public List<IModel> Items
        {
            get { return items; }
        }

        public bool Create(IModel model)
        {
            if (model is ProjectModel project)
            {
                try
                {
                    // Tạo project và thêm người tham gia
                    int projectId = DBHelper.CreateProject(
                        project.Name,
                        project.Description,
                        project.CreatedBy,
                        project.Participants?.Select(p => p.ID).ToList()
                    );

                    if (projectId > 0)
                    {
                        project.Id = projectId;
                        items.Add(project);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    MessageBox.Show("Lỗi khi tạo dự án: " + ex.Message);
                }
            }
            return false;
        }
        public bool Delete(IModel model)
        {
            if (model is ProjectModel project)
            {
                return Delete(project.Id);
            }
            return false;
        }

        public bool Delete(object id)
        {
            //if (id == null) return false;

            //bool result = DBHelper.DeleteProject((int)id);
            //if (result)
            //{
            //    items.RemoveAll(p => ((ProjectModel)p).Id == (int)id);
            //}
            return false;
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
            try
            {
                var projects = DBHelper.GetAllProjects();
                if (projects != null)
                {
                    items.Clear();
                    items.AddRange(projects);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách dự án: " + ex.Message);
            }
            return false;
        }

        // Hàm để lấy dữ liệu dự án cùng với tên người dùng
        public DataTable GetProjectDataWithUserNames()
        {
            // Gọi hàm từ DBHelper để lấy dữ liệu dự án kèm tên người dùng
            return DBHelper.GetProjectWithUserName();
        }

        public bool Load(object id)
        {
            if (id == null) return false;

            try
            {
                var project = DBHelper.GetProjectById((int)id);
                if (project != null)
                {
                    items.Clear();
                    items.Add(project);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin dự án: " + ex.Message);
            }
            return false;
        }

        public IModel Read(object id)
        {
            //if (id == null) return null;

            //// Lấy dự án cùng với danh sách người dùng liên kết
            //return DBHelper.GetProjectWithUsersById((int)id;
            return null;
        }

        public bool Update(IModel model)
        {
            //if (model is ProjectModel project)
            //{
            //    bool result = DBHelper.UpdateProject(project.Id, project.Name, project.Description);
            //    if (result)
            //    {
            //        int index = items.FindIndex(p => ((ProjectModel)p).Id == project.Id);
            //        if (index >= 0)
            //        {
            //            items[index] = project;
            //        }
            //    }
            //    return result;
            //}
            return false;
        }

        public bool IsExist(object id)
        {
            return items.Exists(p => ((ProjectModel)p).Id == (int)id);
        }

        public bool IsExist(IModel model)
        {
            if (model is ProjectModel project)
            {
                return items.Exists(p => ((ProjectModel)p).Id == project.Id);
            }
            return false;
        }
    }
}
