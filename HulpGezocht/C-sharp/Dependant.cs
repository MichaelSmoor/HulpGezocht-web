using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulpGezocht
{
    public class Dependant : User
    {
        //public string Email { get; private set; }
        public DateTime Dob { get; private set; }
        public string PhoneNumber { get; private set; }
        public int ProfilePic { get; private set; }
        public string Bio { get; private set; }

        // Dependant met biografie
        public Dependant(string email, string username, string gpassword, int gpermission, bool active, DateTime dob, string phoneNumber, int profilePic, string bio)
            : base(email, username, gpassword, gpermission, active)
        {
            //Email = email;
            Dob = dob;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            Bio = bio;
        }

        // dependant zonder biografie
        public Dependant(string email, string username, string gpassword, int gpermission, bool active, DateTime dob, string phoneNumber, int profilePic)
            : base(email, username, gpassword, gpermission, active)
        {
            //Email = email;
            Dob = dob;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            Bio = "";
        }

        public bool EditProfile(string phoneNumber, string bio)
        {

            //Naam, wachtwoord, permission aanpassen niet mogelijk nu
            //To-do: check functie met bool toevoegen (let op Bio mag leeg zijn!)
            //Email = email;
            PhoneNumber = phoneNumber;
            Bio = bio;
            
            return true;
        }

        public bool EditProfilePic(int profilePic)
        {
            ProfilePic = profilePic;
            return true;
        }


        public override string ToString()
        {
            string dependantInfo = "Hulpbehoevende: " + Email + " - " + Name + ", [" + Dob.ToString("dd/MM/yyyy") + "] [" + PhoneNumber + "] - Biografie: " + Bio;
            return dependantInfo;
        }
    }
}
