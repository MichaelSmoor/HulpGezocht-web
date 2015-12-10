using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulpGezocht
{
    public class User
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public int Permission { get; private set; }
        public bool Active { get; private set; }
        public string Email { get; private set; }

        public User(string email, string username, string gpassword, int gpermission)
        {
            Email = email;
            Name = username;
            Password = gpassword;
            Permission = gpermission;
            Active = true;
        }

        public User(string username, string gpassword, int gpermission)
        {
            Name = username;
            Password = gpassword;
            Permission = gpermission;
            Active = true;
        }

        public User(string email, string username, string gpassword, int gpermission, bool active)
        {
            Email = email;
            Name = username;
            Password = gpassword;
            Permission = gpermission;
            Active = active;
        }

        public User(string username, string gpassword, int gpermission, bool active)
        {
            Name = username;
            Password = gpassword;
            Permission = gpermission;
            Active = active;
        }


        public User(string username, string email)
        {
            Name = username;
            Email = email;
        }

        public bool LoginCheck(string name, string password)
        {
            if(Active)
            {
                if(Name == name && Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Edit(string name, string password, int permission, bool active)
        {
            //To-do: check functie met bool toevoegen
            Name = name;
            Password = password;
            Permission = permission;
            Active = active;
            return true;
        }

        public User()
        {
            Permission = 3;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
