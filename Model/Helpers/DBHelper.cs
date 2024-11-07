
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using SE_Project.Model;
using System.Data;


namespace SE_Project.Helpers
{
    class DBHelper
    {
        //DB context
        static string conString = @"Data Source=DESKTOP-S8VA2O5;Initial Catalog=TaskSphere1;Integrated Security=True;";
        public static bool RegisterUser(string username, string password, string name)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string hashedPassword = HashPassword(password);
                string query = "INSERT INTO userlist (username, password, name) VALUES (@username, @password, @name)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@name", name);
                    try
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        // Phương thức đăng nhập
        public static bool Login(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "SELECT id, password, name FROM userlist WHERE username = @username";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string hashedPassword = reader["password"].ToString();
                                if (VerifyPassword(password, hashedPassword))
                                {
                                    // Lưu thông tin người dùng vào Session
                                    Session.UserId = Convert.ToInt32(reader["id"]);
                                    Session.Username = username;
                                    Session.Name = reader["name"].ToString();
                                    return true; // Đăng nhập thành công
                                }
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        // Xử lý lỗi SQL nếu cần thiết
                        return false; // Trả về false nếu có lỗi
                    }
                }
            }
            return false; // Trả về false nếu không tìm thấy người dùng hoặc mật khẩu không khớp
        }



        // Phương thức hỗ trợ để mã hóa mật khẩu
        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Phương thức hỗ trợ để xác minh mật khẩu
        private static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == hashedPassword;
        }
        public static bool CreateTask(string name, string description, int projectId, string status, DateTime dueDate, int assignedUserId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "INSERT INTO tasks (name, description, project_id, status, due_date, user_id) VALUES (@name, @description, @projectId, @status, @dueDate, @userId)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@projectId", projectId);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@dueDate", dueDate);
                    cmd.Parameters.AddWithValue("@userId", assignedUserId);

                    try
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public static List<TaskModel> GetTasks()
        {
            List<TaskModel> tasks = new List<TaskModel>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "SELECT * FROM tasks";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tasks.Add(new TaskModel
                                {
                                    Id= (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Project_id = (int)reader["project_id"],
                                    Status = reader["status"].ToString(),
                                    Due_date = (DateTime)reader["due_date"],
                                    User_id = (int)reader["user_id"]
                                });
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                    }
                }
            }
            return tasks;
        }

        public static List<TaskModel> GetTasksByProjectId(int projectId)
        {
            List<TaskModel> tasks = new List<TaskModel>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, name, user_id, description, project_id, status, due_date, created_at FROM tasks WHERE project_id = @ProjectId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProjectId", projectId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TaskModel task = new TaskModel
                                {
                                    Id = reader.GetInt32(0), // id
                                    Name = reader.GetString(1), // name
                                    UserId = reader.GetInt32(2), // user_id
                                    Description = reader.GetString(3), // description
                                    ProjectId = reader.GetInt32(4), // project_id
                                    Status = reader.GetString(5), // status
                                    DueDate = reader.GetDateTime(6), // due_date
                                    CreatedAt = reader.GetDateTime(7) // created_at
                                };

                                tasks.Add(task);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return tasks;
        }

        public static bool UpdateTask(int taskId, string name, string description, int projectId, string status, DateTime dueDate, int assignedUserId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE tasks SET name = @name, description = @description, project_id = @projectId, status = @status, due_date = @dueDate, user_id = @userId WHERE id = @taskId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@projectId", projectId);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@dueDate", dueDate);
                    cmd.Parameters.AddWithValue("@userId", assignedUserId);

                    try
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public static bool DeleteTask(int taskId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "DELETE FROM tasks WHERE id = @taskId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);

                    try
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public static bool UpdateTaskStatus(int taskId, string status)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE tasks SET status = @status WHERE id = @taskId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@status", status);

                    try
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public static bool CreateProject(string name, string description, int userId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "INSERT INTO projects (name, description, user_id) VALUES (@name, @description, @userId)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    try
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public static List<ProjectModel> GetProjects()
        {
            List<ProjectModel> projects = new List<ProjectModel>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "SELECT * FROM projects";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                projects.Add(new ProjectModel
                                {
                                    Id = (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    User_id = (int)reader["user_id"]
                                });
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                    }
                }
            }
            return projects;
        }

        public static ProjectModel GetProjectById(int projectId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "SELECT * FROM projects WHERE id = @projectId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@projectId", projectId);

                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ProjectModel
                                {
                                    Id = (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    User_id = (int)reader["user_id"]
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return null;
                    }
                }
            }
        }

        public static DataTable GetProjectWithUserName()
        {
            DataTable resultTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(conString))
            {
                string query = @"
            SELECT 
                p.id AS ProjectId, 
                p.name AS ProjectName, 
                p.description AS ProjectDescription, 
                u.username AS CreatedBy, 
                COUNT(DISTINCT t.id) AS TaskCounted,  -- Đếm số lượng task riêng biệt
                COUNT(DISTINCT t.user_id) AS AssignedCount  -- Đếm số người phân công task riêng biệt
            FROM 
                projects p
            LEFT JOIN 
                userlist u ON p.user_id = u.id
            LEFT JOIN 
                tasks t ON t.project_id = p.id
            GROUP BY 
                p.id, p.name, p.description, u.username";  // Sử dụng GROUP BY như trong câu truy vấn của bạn

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(resultTable);
                    }
                }
            }

            return resultTable;
        }

        public static ProjectModel GetProjectWithUserNameById(int projectId)
        {
            ProjectModel project = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                
            string query = @"SELECT p.id, p.name, p.description, p.user_id, u.name AS user_name 
                         FROM projects p
                     JOIN userlist u ON p.user_id = u.id
                         WHERE p.id = @ProjectId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            project = new ProjectModel
                            {
                                Id = (int)reader["id"],
                                Name = reader["name"].ToString(),
                                Description = reader["description"].ToString(),
                                User_id = (int)reader["user_id"],
                                Created_by = reader["user_name"].ToString()
                            };
                        }
                    }
                }
            }

            // Nếu bạn muốn lấy tên người dùng, có thể thực hiện truy vấn riêng biệt
            if (project != null)
            {
                string userNameQuery = "SELECT name FROM userlist WHERE id = @UserId";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(userNameQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", project.UserId);
                        con.Open();

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // Nếu tìm thấy, cập nhật tên người dùng vào project (hoặc trả về theo nhu cầu)
                            string userName = result.ToString();
                            // Có thể trả về tên này tại đây nếu cần thiết
                        }
                    }
                }
            }

            return project;
        }

        public static bool UpdateProject(int projectId, string name, string description, int userId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "UPDATE projects SET name = @name, description = @description, user_id = @userId WHERE id = @projectId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@projectId", projectId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    try
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public static bool DeleteProject(int projectId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "DELETE FROM projects WHERE id = @projectId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@projectId", projectId);

                    try
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static List<TaskModel> GetTasksByProjectId(int projectId)
        {
            List<TaskModel> tasks = new List<TaskModel>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "SELECT t.*, u.name AS user_name FROM tasks t JOIN userlist u ON t.user_id = u.id WHERE project_id = @ProjectId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);

                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tasks.Add(new TaskModel
                        {
                                    Id = (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Project_id = (int)reader["project_id"],
                                    Status = reader["status"].ToString(),
                                    Due_date = (DateTime)reader["due_date"],
                                    User_id = (int)reader["user_id"],
                                    Assigned = reader["user_name"].ToString(),
                                });
                        }
                        else
                        {
                            return -1; // Trả về -1 nếu không tìm thấy dự án
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                        return -1;
                    }
                }
            }
            return tasks;
        }


        //public static List<TaskModel> GetTasksByProjectId(int projectId)
        //{
        //    List<TaskModel> tasks = new List<TaskModel>();

        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        string query = "SELECT t.*, u.name AS user_name FROM tasks t JOIN userlist u ON t.user_id = u.id WHERE project_id = @ProjectId";

        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Parameters.AddWithValue("@ProjectId", projectId);

        //            try
        //            {
        //                con.Open();
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        tasks.Add(new TaskModel
        //                        {
        //                            Id = (int)reader["id"],
        //                            Name = reader["name"].ToString(),
        //                            Description = reader["description"].ToString(),
        //                            Project_id = (int)reader["project_id"],
        //                            Status = reader["status"].ToString(),
        //                            Due_date = (DateTime)reader["due_date"],
        //                            User_id = (int)reader["user_id"],
        //                            Assigned = reader["user_name"].ToString(),
        //                        });
        //                    }
        //                }
        //            }
        //            catch (SqlException ex)
        //            {
        //                MessageBox.Show("Lỗi SQL: " + ex.Message);
        //            }
        //        }
        //    }
        //    return tasks;
        //}

    }
}
