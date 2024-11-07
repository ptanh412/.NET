using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Project.Model
{
    public class UserModel : IModel
    {
        public string username;
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        private int id;
        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public UserModel()
        {
            id = 0;
            password = "";
            name = "";
        }
        public UserModel(string id, string name, string password, string username)
        {
            ID = int.Parse(id);
            Name = name;
            Password = password;
            Username = username;

        }
    }
}
