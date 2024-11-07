
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

using System.Configuration;

namespace SE_Project.Helpers
{
    class DBHelper
    {
        static string conString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
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
        public static List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "SELECT id, name, username FROM userlist";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new UserModel
                                {
                                    ID = (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Username = reader["username"].ToString()
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error getting users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
            }

            return users;
        }
        public static bool CreateTask(string name, string description, int projectId, string status, DateTime dueDate, int assignedUserId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = "INSERT INTO tasks ( name, description, project_id, status, due_date, user_id) VALUES (@name, @description, @projectId, @status, @dueDate, @userId)";

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
                string query = "SELECT t.*, p.name AS project_name, u.name AS user_name FROM tasks t " +
                               "JOIN projects p ON t.project_id = p.id " +
                               "JOIN userlist u ON t.user_id = u.id";

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
                                    Id = (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Project_id = (int)reader["project_id"],
                                    ProjectName = reader["project_name"].ToString(),
                                    Status = reader["status"].ToString(),
                                    Due_date = (DateTime)reader["due_date"],
                                    User_id = (int)reader["user_id"],
                                    Assigned = reader["user_name"].ToString()
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
                                    User_id = reader.GetInt32(2), // user_id
                                    Description = reader.GetString(3), // description
                                    Project_id = reader.GetInt32(4), // project_id
                                    Status = reader.GetString(5), // status
                                    Due_date = reader.GetDateTime(6), // due_date
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



        public static bool CheckProjectExists(int projectId)
        {
            using (SqlConnection conn = new SqlConnection("YourConnectionString"))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Projects WHERE ProjectId = @ProjectId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static int CreateProject(string name, string description, int createdBy, List<int> participantIds)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Tạo project và lấy ID
                        string projectQuery = @"INSERT INTO projects (name, description, created_by) 
                                         VALUES (@name, @description, @createdBy);
                                         SELECT SCOPE_IDENTITY();";

                        int projectId;
                        using (SqlCommand cmd = new SqlCommand(projectQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@description", description);
                            cmd.Parameters.AddWithValue("@createdBy", createdBy);
                            projectId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Thêm người tạo vào project_user
                        string creatorParticipantQuery = @"INSERT INTO project_user (project_id, user_id)
                                                     VALUES (@projectId, @userId)";
                        using (SqlCommand cmd = new SqlCommand(creatorParticipantQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@projectId", projectId);
                            cmd.Parameters.AddWithValue("@userId", createdBy);
                            cmd.ExecuteNonQuery();
                        }

                        // Thêm những người tham gia khác
                        if (participantIds != null && participantIds.Count > 0)
                        {
                            string participantQuery = @"INSERT INTO project_user (project_id, user_id)
                                                  VALUES (@projectId, @userId)";
                            using (SqlCommand cmd = new SqlCommand(participantQuery, con, transaction))
                            {
                                foreach (int userId in participantIds.Where(id => id != createdBy))
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@projectId", projectId);
                                    cmd.Parameters.AddWithValue("@userId", userId);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                        return projectId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static List<ProjectModel> GetAllProjects()
        {
            Dictionary<int, ProjectModel> projectDict = new Dictionary<int, ProjectModel>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = @"SELECT p.id, p.name, p.description, p.created_by, p.created_at,
                                  creator.name as creator_name,
                                  pu.user_id,
                                  u.name as participant_name
                           FROM projects p
                           JOIN userlist creator ON p.created_by = creator.id
                           LEFT JOIN project_user pu ON p.id = pu.project_id
                           LEFT JOIN userlist u ON pu.user_id = u.id
                           ORDER BY p.id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int projectId = (int)reader["id"];

                            if (!projectDict.ContainsKey(projectId))
                            {
                                projectDict[projectId] = new ProjectModel
                                {
                                    Id = projectId,
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    CreatedBy = (int)reader["created_by"],
                                    CreatorName = reader["creator_name"].ToString(),
                                    CreatedAt = (DateTime)reader["created_at"],
                                    Participants = new List<UserModel>()
                                };
                            }

                            // Thêm người tham gia vào project
                            int userId = (int)reader["user_id"];
                            string participantName = reader["participant_name"].ToString();

                            // Kiểm tra xem người dùng đã được thêm vào danh sách chưa
                            if (!projectDict[projectId].Participants.Any(p => p.ID == userId))
                            {
                                projectDict[projectId].Participants.Add(new UserModel
                                {
                                    ID = userId,
                                    Name = participantName
                                });
                            }
                        }
                    }
                }
            }

            return projectDict.Values.ToList();
        }

        public static ProjectModel GetProjectById(int projectId)
        {
            ProjectModel project = null;

            using (SqlConnection con = new SqlConnection(conString))
            {
                string query = @"SELECT p.id, p.name, p.description, p.created_by, p.created_at,
                                  creator.name as creator_name,
                                  pu.user_id,
                                  u.name as participant_name
                           FROM projects p
                           JOIN userlist creator ON p.created_by = creator.id
                           LEFT JOIN project_user pu ON p.id = pu.project_id
                           LEFT JOIN userlist u ON pu.user_id = u.id
                           WHERE p.id = @projectId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@projectId", projectId);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (project == null)
                            {
                                project = new ProjectModel
                                {
                                    Id = (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    CreatedBy = (int)reader["created_by"],
                                    CreatorName = reader["creator_name"].ToString(),
                                    CreatedAt = (DateTime)reader["created_at"],
                                    Participants = new List<UserModel>()
                                };
                            }

                            int userId = (int)reader["user_id"];
                            string participantName = reader["participant_name"].ToString();

                            if (!project.Participants.Any(p => p.ID == userId))
                            {
                                project.Participants.Add(new UserModel
                                {
                                    ID = userId,
                                    Name = participantName
                                });
                            }
                        }
                    }
                }
            }

            return project;
        }

    }
}
