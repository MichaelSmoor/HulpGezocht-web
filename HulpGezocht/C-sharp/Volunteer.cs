using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

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
        public string Address { get; private set; }
        public string City { get; private set; }
        public bool DriversLicense { get; private set; }
        public bool HasCar { get; private set; }
        public string PublicTransport { get; private set; }
        public bool Flag { get; private set; }


        // Volunteer zonder biografie
        public Volunteer(string email, string username, string gpassword, int gpermission, bool active, DateTime dob, string phoneNumber, int profilePic, int vog, string bio, string address, string city, bool driversLicense, bool hasCar, string publicTransport, bool flag) : base(email, username, gpassword, gpermission, active)
        {
            //Naam, wachtwoord, permission aanpassen niet mogelijk nu
            //Email = email;
            Dob = dob;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            Vog = vog;
            Bio = bio;
            Address = address;
            City = city;
            DriversLicense = driversLicense;
            HasCar = hasCar;
            PublicTransport = publicTransport;
            Flag = flag;
        }

        // Volunteer met biografie
        public Volunteer(string email, string username, string gpassword, int gpermission, bool active, DateTime dob, string phoneNumber, int profilePic, int vog, string address, string city, bool driversLicense, bool hasCar, string publicTransport, bool flag) : base(email, username, gpassword, gpermission, active)
        {
            //Naam, wachtwoord, permission aanpassen niet mogelijk nu
            //Email = email;
            Dob = dob;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            Vog = vog;
            Bio = "";
            Address = address;
            City = city;
            DriversLicense = driversLicense;
            HasCar = hasCar;
            PublicTransport = publicTransport;
            Flag = flag;
        }

        public bool EditProfile(string phoneNumber, string bio, string address, string city, bool driversLicense, bool hasCar, string publicTransport)
        {
            //Naam, wachtwoord, permission aanpassen niet mogelijk nu
            //To-do: check functie met bool toevoegen (let op Bio mag leeg zijn!)
            //Email = email;

            PhoneNumber = phoneNumber;
            Bio = bio;
            Address = address;
            City = city;
            DriversLicense = driversLicense;
            HasCar = hasCar;
            PublicTransport = publicTransport;
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
