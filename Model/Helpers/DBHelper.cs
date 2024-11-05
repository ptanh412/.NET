
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using SE_Project.Model;

namespace SE_Project.Helpers
{
    class DBHelper
    {
        static string conString = @"Data Source=thanhtruong\SQLEXPRESS;Initial Catalog=TaskManagement;Integrated Security=True;";
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
                string query = "INSERT INTO tasks (name, description, project_id, status, due_date, asuser_id) VALUES (@name, @description, @projectId, @status, @dueDate, @userId)";

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
                string query = "SELECT * FROM tasks WHERE project_id = @ProjectId";

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


        //public string InsertRow(string query)
        //{
        //    try
        //    {
        //        if (connect())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = query;
        //            int result = cmd.ExecuteNonQuery();

        //            if (result > 0)
        //            {
        //                return "Row inserted successfully.";
        //            }
        //            else
        //            {
        //                return "No rows were inserted.";
        //            }
        //        }
        //        else
        //        {
        //            return "Failed to connect to database.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error: " + ex.Message;
        //    }
        //    finally
        //    {
        //        disconnect();
        //    }
        //}

        //public string DeleteRow(string query)
        //{
        //    try
        //    {
        //        if (connect())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = query;
        //            int result = cmd.ExecuteNonQuery();
        //            return result > 0 ? "Row deleted successfully." : "No rows were deleted.";
        //        }
        //        else
        //        {
        //            return "Failed to connect to database.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Error: " + ex.Message;
        //    }
        //    finally
        //    {
        //        disconnect();
        //    }
        //}

        //public int GetTotalRowCount(string tableName)
        //{
        //    string query = $"SELECT COUNT(*) FROM {tableName};";
        //    try
        //    {
        //        if (connect())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = query;
        //            int rowCount = Convert.ToInt32(cmd.ExecuteScalar()); // ExecuteScalar used for single value
        //            return rowCount; // Return the row count directly
        //        }
        //        else
        //        {
        //            throw new Exception("Failed to connect to database.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Database operation failed: " + ex.Message);
        //    }
        //    finally
        //    {
        //        disconnect();
        //    }
        //}


        //public string getButtons(string query, FlowLayoutPanel panel)
        //{
        //    string ret = "";

        //    try
        //    {
        //        cmd.Connection = conn;
        //        cmd.CommandText = query.ToLower();
        //        connect();

        //        reader = cmd.ExecuteReader();

        //        string id, name, description;

        //        while (reader.Read())
        //        {
        //            id = reader[0].ToString();
        //            name = reader[1].ToString();
        //            description = reader[2].ToString();

        //            //btnProduct btn = new btnProduct();
        //            ProjectCard btn = new ProjectCard();

        //            btn.ProjectTitle = name;
        //            btn.ProjectDesc = description;

        //            if (name != string.Empty)
        //            {
        //                panel.Controls.Add(btn);
        //            }

        //            ret = "Data Fetched Successfully.. :)";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ret = ex.Message;
        //        throw;
        //    }
        //    finally
        //    {
        //        disconnect();
        //    }
        //    return ret;
        //}




        //public bool userAuth(string username, string password)
        //{
        //    string query = "SELECT COUNT(*) FROM userlist WHERE username = @username AND password = @password;";
        //    try
        //    {
        //        if (connect())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandText = query;
        //            cmd.Parameters.AddWithValue("@username", username);
        //            cmd.Parameters.AddWithValue("@password", password);

        //            int result = Convert.ToInt32(cmd.ExecuteScalar()); // ExecuteScalar used for single value
        //            return result > 0; // If count is more than 0, credentials are correct
        //        }
        //        else
        //        {
        //            throw new Exception("Failed to connect to the database.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Database operation failed: " + ex.Message);
        //    }
        //    finally
        //    {
        //        disconnect();
        //        cmd.Parameters.Clear(); // Clear parameters to prevent issues on subsequent calls
        //    }
        //}
    }
}
