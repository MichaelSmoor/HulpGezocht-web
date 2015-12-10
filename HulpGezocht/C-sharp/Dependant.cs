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
        public string Address { get; private set; }
        public string City { get; private set; }
        public bool DriversLicense { get; private set; }
        public bool HasCar { get; private set; }
        public string PublicTransport { get; private set; }
        public bool Flag { get; private set; }

        // Dependant met biografie
        public Dependant(string email, string username, string gpassword, int gpermission, bool active, DateTime dob, string phoneNumber, int profilePic, string bio, string address, string city, bool driversLicense, bool hasCar, string publicTransport, bool flag)
            : base(email, username, gpassword, gpermission, active)
        {
            //Email = email;
            Dob = dob;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            Bio = bio;
            Address = address;
            City = city;
            DriversLicense = driversLicense;
            HasCar = hasCar;
            PublicTransport = publicTransport;
            Flag = flag;
        }

        // dependant zonder biografie
        public Dependant(string email, string username, string gpassword, int gpermission, bool active, DateTime dob, string phoneNumber, int profilePic, string address, string city, bool driversLicense, bool hasCar, string publicTransport, bool flag)
            : base(email, username, gpassword, gpermission, active)
        {
            //Email = email;
            Dob = dob;
            PhoneNumber = phoneNumber;
            ProfilePic = profilePic;
            Address = address;
            City = city;
            DriversLicense = driversLicense;
            HasCar = hasCar;
            PublicTransport = publicTransport;
            Flag = flag;
            Bio = "";
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


        public override string ToString()
        {
            string dependantInfo = "Hulpbehoevende: " + Email + " - " + Name + ", [" + Dob.ToString("dd/MM/yyyy") + "] [" + PhoneNumber + "] - Biografie: " + Bio;
            return dependantInfo;
        }
    }
}
