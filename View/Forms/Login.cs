using SE_Project.Controller;
using SE_Project.Helpers;
using SE_Project.Model;
using SE_Project.View;
using System;
using System.Windows.Forms;

namespace SE_Project.Forms
{
    public partial class Login : Form, IView
    {
        private UserController userController;
        public Login()
        {
            InitializeComponent();
            userController = new UserController();
        }

        public void SetDataToText(object item)
        {
            if (item is UserModel user)
            {
                guna2TextBox1.Text = user.Username;
                guna2TextBox2.Text = user.Password;
            }
        }

        public object GetDataFromText()
        {
            return new UserModel
            {
                Username = guna2TextBox1.Text,
                Password = guna2TextBox2.Text
            };
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            var user = (UserModel)GetDataFromText();

            if (userController.Login(user))
            {
                this.Hide();
                new Main().Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin.");
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new SignUp().Show();
        }
        // Các phương thức khác giữ nguyên
    }
}
