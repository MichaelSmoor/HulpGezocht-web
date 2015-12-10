using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HulpGezocht
{
    public class Portal
    {
        public User CurrentUser;
        public User ChattingWith;
        public List<ChatMessage> ChatMessages;
        public Topic CurrentTopic;
        public User SelectedUser;
        [System.ComponentModel.Bindable(true)]
        public List<Reply> replies;
        public Dictionary<int, Reply> ReplyNumber;

        public Portal()
        {
            ChattingWith = new User("bert", "owjofiwjoofowjofwjoefijoweifongebkend");
            ChatMessages = new List<ChatMessage>();
            replies = new List<Reply>();
            ReplyNumber = new Dictionary<int, Reply>();   
                    
        }
              
        #region Portal
        public bool CreateAccount(int permission, string name, string password, string email, string phonenr, DateTime date, string profilepic, string bio, string vog, bool driverslicense)
        {
            //dependant
            if (permission == 1)
            {
                Dependant dependant = new Dependant(email, name, password, 1, true,date, phonenr, profilepic, bio);
                if (Database.InsertUser(dependant))
                {
                    CurrentUser = dependant;
                    return true;
                }
            }
            //volunteer
            else if (permission == 2)
            {
                Volunteer volunteer = new Volunteer(email, name, password, 2, true, date, phonenr, profilepic, vog, bio, driverslicense);
                if (Database.InsertUser(volunteer))
                {
                    CurrentUser = volunteer;
                    return true;
                }
            }
            return false;
        }

        public void LoginVerified(string email)
        {
            CurrentUser = Database.GetUser(email);
        }

        public string GetPassword(string email)
        {
            return Database.GetPassword(email);
        }
        #endregion

        #region Forum
        public List<Topic> GetQuestions()
        {
            return Database.GetQuestions();
        }

        public List<Topic> GetQuestions(string email)
        {
            return Database.GetQuestions(email);
        }

        public void InsertTopic(string header, string body, string location, bool urgency, string transport, int travelTime, DateTime dateHelpNeededStart, DateTime dateHelpNeededEnd)
        {
            Topic topic = new Topic(Database.GetNextIDFromTable("topic"), CurrentUser.Email, header, body, location, urgency, transport, travelTime, dateHelpNeededStart, dateHelpNeededEnd, DateTime.Now);
            Database.InsertTopic(topic);
        }

        #endregion

        #region Calender
        /*
        public bool SendAppointmentRequest(string mailReceiver, DateTime date, bool atHome, string titel, string transport, int timeLapseMin)
        {
            if (Database.FindUser(mailReceiver) != null)
            {
                string adress = Database.GetHomeInformation(CurrentUser.Email);

                Database.SendAppointment(mailReceiver, date, adress, titel, transport, timeLapseMin);
                return true;
            }
            else
            {

                return false;
            }
        }

        public bool SendAppointmentRequest(string mailReceiver, DateTime date, string location, string titel, string transport, int timeLapseMin)
        {
            if (Database.FindUser(mailReceiver) != null)
            {

                Database.SendAppointment(mailReceiver, date, location, titel, transport, timeLapseMin);
                return true;
            }
            else
            {

                return false;
            }

        }*/

        public List<Appointment> GetAppointmentsOnDate(List<Appointment> lst, DateTime date)
        {
            List<Appointment> lstOnDate = new List<Appointment>();
            foreach (Appointment app in lst)
            {
                if (Math.Floor((app.DateHelpNeeded1 - date).TotalDays) == 0)
                {
                    lstOnDate.Add(app);
                }
            }
            return lstOnDate;
        }
        /*
        public List<Appointment> GetAppointments(string email, DateTime date)
        {
            List<Appointment> allAppointments = new List<Appointment>();
            List<Appointment> appointmentsOnDate = new List<Appointment>();
            allAppointments = Database.GetAllAppointmentsFrom(email);
            foreach (Appointment app in allAppointments)
            {
                if (app.DateHelpNeeded1.Year == date.Year && app.DateHelpNeeded1.Month == date.Month && app.DateHelpNeeded1.Day == date.Day)
                {
                    appointmentsOnDate.Add(app);
                }
            }
            return appointmentsOnDate;
        }*/

        public List<Appointment> GetAppointments(string email)
        {
            return Database.GetAllAppointmentsFrom(email);
        }

        public bool InsertAppointment(string sender, string header, DateTime date1, string location, int travelTime, string transport, string receiver)
        {
            if (date1 < DateTime.Now)
            {
                return false;
            }
            else
            {
                Appointment appointment = new Appointment(Database.GetNextIDFromTable("appointment"), sender, header, date1, location, travelTime, transport, receiver);
                Database.InsertAppointment(appointment);
                return true;
            }
        }
        #endregion

        #region Profile
        // select flags
        public int GetProfileFlags(string email)
        {
            return Database.GetProfileFlags(email);
        }

        // update flags
        public void FlagProfile(string email)
        {
            Database.FlagProfile(email);
        }

        // update profile
        public void UpdateProfile(string phonenumber, string biography, string email, bool driverslicense)
        {
            if ((CurrentUser as Volunteer).EditProfile(phonenumber, biography, driverslicense))
                Database.UpdateProfile(phonenumber, biography, email, driverslicense);
        }

        public void UpdateProfile(string phonenumber, string biography, string email)
        {
            if ((CurrentUser as Dependant).EditProfile(phonenumber, biography))
                Database.UpdateProfile(phonenumber, biography, email);
        }

        // update profilepic
        public void UpdateProfilePic(string profilepic, string email)
        {
            if (CurrentUser is Dependant)
            {
                if ((CurrentUser as Dependant).EditProfilePic(profilepic))
                    Database.UpdateProfilePic(profilepic, email);
            }
            else if (CurrentUser is Volunteer)
            {
                if ((CurrentUser as Volunteer).EditProfilePic(profilepic))
                    Database.UpdateProfilePic(profilepic, email);
            }
        }

        // update profile vog
        public void UpdateProfileVog(string vog, string email)
        {
            if (CurrentUser is Volunteer)
            {
                if ((CurrentUser as Volunteer).EditProfileVog(vog))
                    Database.UpdateProfileVog(vog, email);
            }
        }

        #endregion

        #region Chat
        public List<User> ChatOverview()
        {
            List<User> names = new List<User>();
            foreach (string email in Database.GetChatOverview(CurrentUser.Email))
            {
                User user = Database.NameGetter(email);
                bool alreadyin = false;
                foreach (User temp in names)
                {
                    if (temp.Email == user.Email)
                    {
                        alreadyin = true;
                    }
                }
                if (!alreadyin)
                {
                    names.Add(user);
                }

            }
            return names;
        }

        public bool SendMessage(string text)
        {
            if (text != null && text != "")
            {
                while (text.Contains("\n"))
                {
                    text = text.Replace("\n", " ");
                }
                ChatMessage message = new ChatMessage(Database.GetNextIDFromTable("message"), text, CurrentUser.Email, ChattingWith.Email, DateTime.Now);
                Database.InsertMessage(message);
                Database.InsertMessageRecipient(message);
                return true;
            }
            return false;
        }

        public bool RefreshChatMessages()
        {
            int highestId = 1;
            if (ChatMessages != null)
            {
                foreach (ChatMessage msg in ChatMessages)
                {
                    if (msg.Id > highestId)
                    {
                        highestId = msg.Id;
                    }
                }
            }
            List<ChatMessage> lst = Database.GetLastMessages(CurrentUser.Email, ChattingWith.Email, highestId);
            if (lst != null)
            {
                if (ChatMessages == null)
                {
                    ChatMessages = lst;
                }
                else
                {
                    foreach (ChatMessage msg in lst)
                    {
                        if (!ChatMessages.Contains(msg))
                        {
                            ChatMessages.Add(msg);
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Review
        public List<Review> GetReviews()
        {
            //Functie om alle reviews op te halen
            return Database.GetReviews(SelectedUser.Email);
        }

        #endregion



        /*public bool DeclineRequest(int id)
        {
            if (Database.GetAllAppointmentsFrom(id) != null)
            {
                Database.DeleteAppointment(id);
            }
            return true;
        }*/

        public string FindUser(string email)
        {

            return Database.FindUser(email);
        }

        public User GetUser(string email)
        {
            return Database.GetUser(email);
        }      

        public void AppointmentDelete(Appointment appointment)
        {
            Database.DeleteAppointment(appointment);
        }

        public void InsertReview(string body, int rating)
        {
            Review review = new Review(Database.GetNextIDFromTable("review"), SelectedUser.Email, body, rating, DateTime.Now);
            Database.InsertReview(review);
        }

        public bool InsertReply(string body)
        {
            return Database.InsertReply(new Reply(Database.GetNextIDFromTable("reply"), CurrentUser.Email, CurrentTopic, body, DateTime.Now));
        }

        public void GetReplies()
        {
            replies = Database.GetReplies(CurrentTopic);
            replies.Sort(delegate (Reply r1, Reply r2) { return r1.DatePosted.CompareTo(r2.DatePosted); });
        }

        #region Admin
        public List<User> GetFlaggedUsers(bool flaggedchk, bool activechk)
        {
            return Database.GetFlaggedUsers(flaggedchk, activechk);
        }

        public List<Topic> GetFlaggedTopics(bool flaggedchk, bool activechk)
        {
            return Database.GetFlaggedTopics(flaggedchk, activechk);
        }

        public List<Reply> GetFlaggedReplies(bool flaggedchk, bool activechk)
        {
            return Database.GetFlaggedReplies(flaggedchk, activechk);
        }

        public List<Review> GetFlaggedReviews(bool flaggedchk, bool activechk)
        {
            return Database.GetFlaggedReviews(flaggedchk, activechk);
        }

        public void UnactivateProfile(User user, bool activateOrDisable)
        {
            Database.UnactivateProfile(user, activateOrDisable);
        }

        public void UnactivateTopic(Topic topic, bool activateOrDisable)
        {
            Database.UnactivateTopic(topic, activateOrDisable);
        }

        public void UnactivateReply(Reply reply, bool activateOrDisable)
        {
            Database.UnactivateReply(reply, activateOrDisable);
        }

        public void UnactivateReview(Review review, bool activateOrDisable)
        {
            Database.UnactivateReview(review, activateOrDisable);
        }
        #endregion

    }
}
