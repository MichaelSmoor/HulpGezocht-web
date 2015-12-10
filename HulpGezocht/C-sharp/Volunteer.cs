using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulpGezocht
{
    public class Volunteer : User
    {
        //public string Email {get; private set;}
        public DateTime Dob {get; private set;}
        public string PhoneNumber {get; private set;}
        public  int ProfilePic {get; private set;}
        public  int Vog {get; private set;}
        public  string Bio {get; private set;}
        public bool driversLicense;

        // Volunteer zonder biografie
        public Volunteer(string email, string username, string gpassword, int gpermission, bool active, DateTime dob, string phoneNumber, int profilePic, int vog, string bio, bool driverslicense) : base(email, username, gpassword, gpermission, active)
        {
            //Naam, wachtwoord, permission aanpassen niet mogelijk nu
            //Email = email;
            Dob = dob;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            Vog = vog;
            Bio = bio;
            driversLicense = driverslicense;
        }

        // Volunteer met biografie
        public Volunteer(string email, string username, string gpassword, int gpermission, bool active, DateTime dob, string phoneNumber, int profilePic, int vog, bool driverslicense) : base(email, username, gpassword, gpermission, active)
        {
            //Naam, wachtwoord, permission aanpassen niet mogelijk nu
            //Email = email;
            Dob = dob;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            Vog = vog;
            Bio = "";
            driversLicense = driverslicense;
        }

        public bool EditProfile(string phoneNumber, string bio, bool driverslicense)
        {
            //Naam, wachtwoord, permission aanpassen niet mogelijk nu
            //To-do: check functie met bool toevoegen (let op Bio mag leeg zijn!)
            //Email = email;

            PhoneNumber = phoneNumber;
            Bio = bio;
            driversLicense = driverslicense;
            return true;
        }

        public bool EditProfilePic(int profilePic)
        {
            ProfilePic = profilePic;
            return true;
        }

        public bool EditProfileVog(int vog)
        {
            Vog = vog;
            return true;
        }

        public override string ToString()
        {
            string volunteerInfo = "Vrijwilliger: " + Email + " - " + Name + ", [" + Dob.ToString("dd/MM/yyyy") + "] [" + PhoneNumber + "] - Biografie: " + Bio;
            return volunteerInfo;
        }

    }
}
