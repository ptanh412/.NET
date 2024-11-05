using SE_Project.Controller;
using SE_Project.Helpers;
using SE_Project.Model;
using SE_Project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Project.Forms
{
    public partial class SignUp : Form, IView
    {
        UserController userController = new UserController();
        public SignUp()
        {
            InitializeComponent();
        }
        public void SetDataToText(object item)
        {
            if (item is UserModel user)
            {
                guna2TextBox1.Text = user.Username;
                guna2TextBox2.Text = user.Password;
                txt_Name.Text = user.Name;
            }
        }

        public object GetDataFromText()
        {
            return new UserModel
            {
                Username = guna2TextBox1.Text,
                Password = guna2TextBox2.Text,
                Name = txt_Name.Text
            };
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            var user = (UserModel)GetDataFromText();

            if (ValidatePassword(user.Password))
            {

                var isValid = userController.Create(user);
                MessageBox.Show(user.Password, isValid.ToString());
                if (isValid)
                {
                    MessageBox.Show("Đăng ký thành công!");
                    this.Hide();
                    new Login().Show();
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại. Vui lòng thử lại.");
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự, chứa ít nhất một chữ hoa và một ký tự đặc biệt.");
            }
        }

        private bool ValidatePassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new Login().Show();
        }


    }
}
