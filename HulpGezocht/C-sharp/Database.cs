using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Windows.Forms;

namespace HulpGezocht
{
    public static class Database
    {
        static string connectionstring = "Data Source=192.168.20.3:1521/orcl; User Id=SYSTEM; Password=hulpgezocht";
        //static string connectionstring = "Data source=localhost; User id=system; password=ytcazk";
        static OracleConnection connection;
        static OracleCommand cmd = new OracleCommand() { InitialLONGFetchSize = -1 };
        static OracleDataReader reader;
        static string query = string.Empty;
        static int permission = 4;

        #region Portal
        public static string GetPassword(string email)
        {
            string getPassword;
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT gpassword FROM guser WHERE email = :pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;

                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    getPassword = reader.GetString(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
            return getPassword;
        }

        public static bool InsertUser(Volunteer user)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "INSERT INTO guser (email, username, gpassword, gpermission, dob, phonenumber, profilepic, vog, biography)" +
                    "VALUES ( :pemail, :pusername, :ppassword, :ppermission, :pdob, :pphonenumber, :pprofilepic, :pvog, :pbiography);";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", user.Email);
                    cmd.Parameters.Add("username", user.Name);
                    cmd.Parameters.Add("password", user.Password);
                    cmd.Parameters.Add("permission", user.Permission);
                    cmd.Parameters.Add("dob", user.Dob);
                    cmd.Parameters.Add("phonenumber", user.PhoneNumber);
                    cmd.Parameters.Add("profilepic", user.ProfilePic);
                    cmd.Parameters.Add("vog", user.Vog);
                    cmd.Parameters.Add("biography", user.Bio);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public static bool InsertUser(Dependant user)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "INSERT INTO guser (email, username, gpassword, gpermission, dob, phonenumber, profilepic, biography) " +
                    "VALUES ( :pemail, :pusername, :ppassword, :ppermission, :pdob, :pphonenumber, :pprofilepic, :pbiography)";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", user.Email);
                    cmd.Parameters.Add("username", user.Name);
                    cmd.Parameters.Add("password", user.Password);
                    cmd.Parameters.Add("permission", user.Permission);
                    cmd.Parameters.Add("dob", user.Dob);
                    cmd.Parameters.Add("phonenumber", user.PhoneNumber);
                    cmd.Parameters.Add("profilepic", user.ProfilePic);
                    cmd.Parameters.Add("biography", user.Bio);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region Forum
        public static List<Topic> GetQuestions(string email)
        {
            List<Topic> topics;


            // als gebruiker gast of vrijwilliger is, haal alle vragen op uit database
            topics = new List<Topic>();

            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT topicid, email, topicheader, topicbody, topiclocation, urgency, transport, traveltime, datehelpneededstart, datehelpneededend, dateposted  FROM topic WHERE email=:pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;

                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        bool urgent = reader.GetInt32(5) == 1;
                        //int id, string account, string header, string body, string location, int urgency, string transport, int travelTime, DateTime dateHelpNeeded
                        Topic topic = new Topic(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), urgent, reader.GetString(6), reader.GetInt32(7), reader.GetDateTime(8), reader.GetDateTime(9), reader.GetDateTime(10));
                        topics.Add(topic);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }

            return topics;
        }

        public static List<Topic> GetQuestions()
        {
            List<Topic> topics;

            // als gebruiker gast of vrijwilliger is, haal alle vragen op uit database
            topics = new List<Topic>();

            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT topicid, email, topicheader, topicbody, topiclocation, urgency, transport, traveltime, datehelpneededstart, datehelpneededend, dateposted  FROM topic WHERE active = 1";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();

                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bool urgent = reader.GetInt32(5) == 1;
                        //int id, string account, string header, string body, string location, int urgency, string transport, int travelTime, DateTime dateHelpNeeded
                        Topic topic = new Topic(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), urgent, reader.GetString(6), reader.GetInt32(7), reader.GetDateTime(8), reader.GetDateTime(9), reader.GetDateTime(10));
                        topics.Add(topic);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }

            return topics;
        }

        //public static void InsertTopic(string email, string header, string body, string location, bool urgency, string transport, int traveltime, DateTime datehelpneeded, DateTime dateposted)
        public static void InsertTopic(Topic topic)
        {
            int topicId = GetNextIDFromTable("topic");
            if (topicId < 0)
            {
                return;
            }
            DateTime date1 = new DateTime(topic.DateHelpNeededStart.Year, topic.DateHelpNeededStart.Month, topic.DateHelpNeededStart.Day, topic.DateHelpNeededStart.Hour, topic.DateHelpNeededStart.Minute, 00);
            DateTime date2 = new DateTime(topic.DateHelpNeededEnd.Year, topic.DateHelpNeededEnd.Month, topic.DateHelpNeededEnd.Day, topic.DateHelpNeededEnd.Hour, topic.DateHelpNeededEnd.Minute, 00);
            using (connection = new OracleConnection(connectionstring))
            {
                int urgencyInt = topic.Urgency ? 1 : 0;
                query = "INSERT INTO topic(topicid, email, topicheader, topicbody, topiclocation, urgency, transport, traveltime, datehelpneededstart, datehelpneededend, datelastedited, dateposted)" +
                                      " VALUES(" + topicId + ", :pemail, :pheader, :pbody, :plocation, :purgencyInt, :ptransport, :ptraveltime, :pdatehelpneeded1, :pdatehelpneeded2, :pdateposted, :pdateposted)";
                cmd.CommandText = query;
                cmd.Connection = connection;

                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", topic.Account);
                    cmd.Parameters.Add("header", topic.Header);
                    cmd.Parameters.Add("body", topic.Body);
                    cmd.Parameters.Add("location", topic.Location);
                    cmd.Parameters.Add("urgencyInt", urgencyInt);
                    cmd.Parameters.Add("transport", topic.Transport);
                    cmd.Parameters.Add("traveltime", topic.TravelTime);
                    cmd.Parameters.Add("datehelpneeded1", date1);
                    cmd.Parameters.Add("datehelpneeded2", date2);
                    cmd.Parameters.Add("dateposted", topic.DatePosted);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static List<Reply> GetReplies(Topic topic)
        {
            List<Reply> replies = new List<Reply>();

            // als gebruiker gast of vrijwilliger is, haal alle vragen op uit database

            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT replyid, email, topicid, replybody, dateposted FROM reply WHERE topicid=:ptopicid";
                cmd.CommandText = query;
                cmd.Connection = connection;

                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("topicid", topic.Id);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //user = GetUser(reader.GetString(1));
                        //int replyid, string email, int topicid, string body, DateTime dateposted
                        replies.Add(new Reply(reader.GetInt32(0), reader.GetString(1), topic, reader.GetString(3), reader.GetDateTime(4)));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return replies;
                }
            }
            return replies;

        }

        public static bool InsertReply(Reply reply)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "INSERT INTO reply (replyid, email, topicid, replybody, dateposted) VALUES('" + reply.Id + "', '" + reply.Email + "', '" + reply.CurrentTopic.Id + "', :pbody, :pdateposted)";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("body", reply.Body);
                    cmd.Parameters.Add("dateposted", reply.DatePosted);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }


        #endregion

        #region Calender
        public static List<Appointment> GetAllAppointmentsFrom(string email)
        {
            List<Appointment> list = new List<Appointment>();
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT a.*, receiver FROM appointment a, appointmentrecipient r WHERE (a.sender = :pemail OR receiver = :pemail) AND datehelpneeded >= sysdate AND active = 1 AND a.APPOINTMENTID = r.APPOINTMENTID";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Appointment(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetString(8)));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return list;
        }

        public static void InsertAppointment(Appointment appointment)
        {
            DateTime dateOne = new DateTime(appointment.DateHelpNeeded1.Year, appointment.DateHelpNeeded1.Month, appointment.DateHelpNeeded1.Day, appointment.DateHelpNeeded1.Hour, appointment.DateHelpNeeded1.Minute, 00);
            //string dateOne = "'" + date1.Day + "/" + date1.Month + "/" + date1.Year + " " + date1.Hour + ":" + date1.Minute + ", 'DD/MM/YYYY HH24:MI'"; 
            //string dateTwo = "'" + date2.Day + "/" + date2.Month + "/" + date2.Year + " " + date2.Hour + ":" + date2.Minute + ", 'DD/MM/YYYY HH24:MI'";

            using (connection = new OracleConnection(connectionstring))
            {
                query = "INSERT INTO appointment(appointmentid, sender, appointmentheader, datehelpneeded, appointmentlocation, traveltime, transport)" +
                                      " VALUES(" + appointment.Id + ", :psender, :pzheader, :pdate1, :pzlocation, :ptravelTime, :ptransport)";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("sender", appointment.Sender);
                    cmd.Parameters.Add("zheader", appointment.Header);
                    cmd.Parameters.Add("date1", dateOne);
                    cmd.Parameters.Add("zlocation", appointment.Location);
                    cmd.Parameters.Add("traveltime", appointment.TravelTime);
                    cmd.Parameters.Add("transport", appointment.Transport);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("receiver", appointment.Receiver);
                    query = "INSERT INTO appointmentrecipient(appointmentid, receiver)" +
                                                          " VALUES(" + appointment.Id + ", :preceiver)";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void DeleteAppointment(Appointment appointment)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE APPOINTMENT SET ACTIVE = 0 WHERE APPOINTMENTID ="+ appointment.Id;
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region Profile
        // Get flags
        public static int GetProfileFlags(string email)
        {
            int getFlags;
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT flag FROM guser WHERE email = :pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    getFlags = reader.GetInt32(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }
            return getFlags;
        }

        // Update flags
        public static void FlagProfile(string email)
        {
            int flags = GetProfileFlags(email);
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE guser SET flag = 1 WHERE email=:pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // profile update
        public static void UpdateProfile(string phonenumber, string biography, string email, bool driverslicense)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE guser SET  phonenumber=:pphonenumber, biography=:pbiography, driverslicense=:pdriverslicense WHERE email=:pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("phonenumber", phonenumber);                  
                    cmd.Parameters.Add("biography", biography);
                    cmd.Parameters.Add("driverslicense", driverslicense);
                    cmd.Parameters.Add("email", email);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void UpdateProfile(string phonenumber, string biography, string email)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE guser SET  phonenumber=:pphonenumber, biography=:pbiography WHERE email=:pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("phonenumber", phonenumber);
                    cmd.Parameters.Add("biography", biography);
                    cmd.Parameters.Add("email", email);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // profilepic update
        public static void UpdateProfilePic(string profilepic, string email)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE guser SET profilepic=:pprofilepic WHERE email=:pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();


                    cmd.Parameters.Add("profilepic", profilepic);
                    cmd.Parameters.Add("email", email);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // profileVOG update
        public static void UpdateProfileVog(string vog, string email)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE guser SET vog=:pvog WHERE email=:pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("vog", vog);
                    cmd.Parameters.Add("email", email);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region Chat

        public static List<ChatMessage> GetLastMessages(string emailcurrentuser, string emailcontact, int lastmessageid)
        {
            List<ChatMessage> messages = new List<ChatMessage>();
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT m.messageid, text, sender, receiver, dateposted FROM message m LEFT JOIN messagerecipient mr ON m.messageid = mr.messageid " +
                        " WHERE ((sender = :psender AND receiver = :preceiver) OR (sender = :preceiver AND receiver = :psender))" +
                        " AND m.messageid > " + lastmessageid;
                /*query = "SELECT text FROM message WHERE sender = :psender" +
                    " AND messageid IN (SELECT messageid FROM messagerecipient WHERE receiver = :preciever)" +
                    " AND rownum <= 10;"; oude manier*/
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("sender", emailcurrentuser);
                    cmd.Parameters.Add("reciever", emailcontact);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader.GetString(2) == emailcurrentuser)
                        {
                            
                            messages.Add(new ChatMessage(reader.GetInt32(0), reader.GetString(1), emailcurrentuser, emailcontact, reader.GetDateTime(4)));
                        }
                        else
                        {
                            messages.Add(new ChatMessage(reader.GetInt32(0), reader.GetString(1), emailcontact, emailcurrentuser, reader.GetDateTime(4)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
            return messages;
        }

        public static List<string> GetChatOverview(string email)
        {
            List<string> users = new List<string>();
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT sender, receiver FROM message m, messagerecipient mr WHERE (sender = :pemail OR receiver = :pemail) AND m.messageid = mr.messageid GROUP BY sender, receiver";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(0) == email)
                        {
                            users.Add(reader.GetString(1));
                        }
                        else
                        {
                            users.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
            return users;
        }

        //vrijwilliger@email.com
        //hulpbehoevende@email.com

        public static void InsertMessage(ChatMessage message)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "INSERT INTO message (messageid, sender, text, dateposted)" +
                    " VALUES(:pmessageid, :psender, :ptext, :pdateposted)";
                cmd.Connection = connection;
                cmd.CommandText = query;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("messageid", message.Id);
                    cmd.Parameters.Add("sender", message.Sender);
                    cmd.Parameters.Add("text", message.Text);
                    cmd.Parameters.Add("dateposted", message.DatePosted);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void InsertMessageRecipient(ChatMessage message)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "INSERT INTO messagerecipient(messageid, receiver) VALUES(:pmessageid, :preceiver)";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("messageid", message.Id);
                    cmd.Parameters.Add("receiver", message.Receiver);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region Review

        // Een lijst met reviews van de gebruiker ophalen
        public static List<Review> GetReviews(string email)
        {
            List<Review> reviews = new List<Review>();
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT reviewid, receiver, reviewbody, dateposted, rating FROM review WHERE receiver='"+email+"' AND active = 1";
                cmd.CommandText = query;
                cmd.Connection = connection;
                Review rev = null;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        rev = new Review(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4), reader.GetDateTime(3));
                        reviews.Add(rev);
                    }
                    return reviews;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }

        // Een review schrijven
        public static void InsertReview(Review review)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "INSERT INTO review (reviewid, receiver, reviewbody, dateposted, rating) VALUES('" + review.Id + "', '" + review.Account + "', :pbody, :pdateposted, " + review.Rating + ")";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("body", review.Body);
                    cmd.Parameters.Add("dateposted", review.DatePosted);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        #region Admin
        public static List<User> GetFlaggedUsers(bool flaggedchk, bool activechk)
        {
            List<User> flaggedusers = new List<User>();
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT email, username, gpassword, gpermission, dob, phonenumber, profilepic, vog, biography, driverslicense, active FROM guser WHERE flag = "+Convert.ToInt32(flaggedchk)+" AND active="+Convert.ToInt32(activechk);
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bool active = reader.GetInt32(10) == 1;
                        bool driverslicense = reader.GetInt32(9) == 1;
                        int permission = reader.GetInt32(3);
                        switch (permission)
                        {
                            case 1:
                                flaggedusers.Add(new Dependant(reader.GetString(0), reader.GetString(1), reader.GetString(2), permission, active, reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetString(8)));
                                break;
                            case 2:
                                flaggedusers.Add(new Volunteer(reader.GetString(0), reader.GetString(1), reader.GetString(2), permission, active, reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), driverslicense));
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                return flaggedusers;
            }
        }

        public static List<Topic> GetFlaggedTopics(bool flaggedchk, bool activechk)
        {
            List<Topic> flaggedtopics = new List<Topic>();
            using (connection = new OracleConnection(connectionstring))
            {
                query = query = "SELECT topicid, email, topicheader, topicbody, topiclocation, urgency, transport, traveltime, datehelpneededstart, datehelpneededend, dateposted  FROM topic WHERE flag = " + Convert.ToInt32(flaggedchk) + " AND active=" + Convert.ToInt32(activechk);
                ;
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bool urgent = reader.GetInt32(5) == 1;
                        Topic flaggedtopic = new Topic(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), urgent, reader.GetString(6), reader.GetInt32(7), reader.GetDateTime(8), reader.GetDateTime(9), reader.GetDateTime(10));
                        flaggedtopics.Add(flaggedtopic);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                return flaggedtopics;
            }

        }

        public static List<Reply> GetFlaggedReplies(bool flaggedchk, bool activechk)
        {
            List<Reply> flaggedreplies = new List<Reply>();
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT replyid, email, topicid, replybody, dateposted FROM reply WHERE flag = " + Convert.ToInt32(flaggedchk) + " AND active=" + Convert.ToInt32(activechk);
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Reply flaggedreply = new Reply(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetDateTime(4));
                        flaggedreplies.Add(flaggedreply);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                return flaggedreplies;
            }
        }

        public static List<Review> GetFlaggedReviews(bool flaggedchk, bool activechk)
        {
            List<Review> flaggedreviews = new List<Review>();
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT reviewid, receiver, reviewbody, dateposted, rating FROM review WHERE flag = " + Convert.ToInt32(flaggedchk) + " AND active=" + Convert.ToInt32(activechk);
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Review flaggedreview = new Review(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(4), reader.GetDateTime(3));
                        flaggedreviews.Add(flaggedreview);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                return flaggedreviews;
            }
        }

        public static void UnactivateProfile(User user, bool activateOrDisable)
        {
            bool active = activateOrDisable;
            bool flag = !activateOrDisable;
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE guser SET active = "+Convert.ToInt32(active)+", flag = "+ Convert.ToInt32(flag) + " WHERE email=:pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", user.Email);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void UnactivateTopic(Topic topic, bool activateOrDisable)
        {
            bool active = activateOrDisable;
            bool flag = !activateOrDisable;
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE topic SET active = " + Convert.ToInt32(active) + ", flag = " + Convert.ToInt32(flag) + " WHERE topicid=:ptopicid";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("topicid", topic.Id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        public static void UnactivateReply(Reply reply, bool activateOrDisable)
        {
            bool active = activateOrDisable;
            bool flag = !activateOrDisable;
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE reply SET active = " + Convert.ToInt32(active) + ", flag = " + Convert.ToInt32(flag) + " WHERE replyid=:preplyid";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("replyid", reply.Id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        public static void UnactivateReview(Review review, bool activateOrDisable)
        {
            bool active = activateOrDisable;
            bool flag = !activateOrDisable;
            using (connection = new OracleConnection(connectionstring))
            {
                query = "UPDATE guser SET active = " + Convert.ToInt32(active) + ", flag = " + Convert.ToInt32(flag) + " WHERE reviewid=:previewid";
                cmd.CommandText = query;
                cmd.Connection = connection;
                connection.Open();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("reviewid", review.Id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        #endregion


        public static string FindUser(string email)
        {
            string user = "";
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT email, username, phonenumber, dob, biography, profilepic, active, vog FROM guser WHERE email = :pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    user = reader.GetString(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return user;
        }

        public static User GetUser(string email)
        {
            User userToReturn;
            permission = GetPermissionLevel(email);
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT email, username, gpassword, gpermission, dob, phonenumber, profilepic, vog, biography, driverslicense, active FROM guser WHERE email = :pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);                   
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    bool active = reader.GetInt32(10) == 1;
                    if (permission == 0)
                    {
                        userToReturn = new Administrator(reader.GetString(0), reader.GetString(1), reader.GetInt32(3));
                    }
                    else if (permission == 1)
                    {
                        userToReturn = new Dependant(reader.GetString(0), reader.GetString(1), reader.GetString(2), permission, active, reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetString(8));
                    }
                    else if (permission == 2)
                    {
                        bool driverslicense = reader.GetInt32(9) == 1;
                        userToReturn = new Volunteer(reader.GetString(0), reader.GetString(1), reader.GetString(2), permission, active, reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), driverslicense);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
            return userToReturn;
        }

        /*
        public static void SendAppointment(string mailReceiver, DateTime date, string adress, string titel, string transport, int timeLapseMin)
        {
            
            SendAppointment
            INSERT INTO appointment (sender, appointmentheader, datehelpneeded, appointmentlocation) values('sender', 'title', 'date', 'location');

        }*/


        public static int GetPermissionLevel(string email)
        {
            permission = 3;
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT gpermission FROM guser WHERE email = :pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    permission = reader.GetInt32(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return permission;
        }     

        public static int GetNextIDFromTable(string table)
        {
            int nextid;
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT MAX(" + table + "id) FROM " + table;
                cmd.CommandText = query;
                cmd.Connection = connection;              
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    nextid = reader.GetInt32(0);
                }
                catch (Exception ex)
                {
                    nextid = -2;
                    MessageBox.Show(ex.Message);
                }
            }
            return nextid + 1;
        }

        public static User NameGetter(string email)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT username FROM guser WHERE email = :pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", email);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new User(reader.GetString(0), email);
                    }
                    else
                    { 
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }



        /*
        //Test met db hoe de waarde van een lege query wordt teruggegeven
        public static bool UserExists(User user)
        {
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT email FROM guser WHERE email = :pemail";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("email", user.Email);
                    cmd.ExecuteNonQuery();
                    reader.Read();
                    reader.GetString(0);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }*/

        /* wordt niet meer gebruikt
        public static Message GetLast10Message(string emailcurrentuser, string emailcontact)
        {
            Message message;
            using (connection = new OracleConnection(connectionstring))
            {
                query = "SELECT text, sender, receiver FROM message m, messagerecipient mr WHERE (sender = :psender AND receiver = :preceiver) OR (sender = :preceiver AND receiver = :psender)" +
                       " AND m.messageid = (SELECT max(messageid) FROM message);";
                cmd.CommandText = query;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("sender", emailcurrentuser);
                    cmd.Parameters.Add("reciever", emailcontact);
                    reader = cmd.ExecuteReader();
                    reader.Read();

                    while (reader.Read())
                    {
                        message = new Message(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }

            return message;
        }*/
    }
}
