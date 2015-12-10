using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulpGezocht
{
    public class Guest : User
    {
        public Guest(string name, string password, int permission) : base(name, password, permission)
        {
            //Naam, wachtwoord, permission aanpassen niet mogelijk 
        }
    }
}
