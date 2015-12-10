using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulpGezocht
{
    public class Topic
    {
        public int Id { get; private set; }
        public string Header { get; private set; }
        public string Body { get; private set; }
        public string Location { get; private set; }
        public bool Urgency { get; private set; }
        public string Transport { get; private set; }
        public int TravelTime { get; private set; }
        public DateTime DateHelpNeededStart { get; private set; }
        public DateTime DateHelpNeededEnd { get; private set; }
        public DateTime DateLastEdited { get; private set; }
        public DateTime DatePosted { get; private set; }
        public bool Flag { get; private set; }
        public bool Active { get; private set; }
        public string Account { get; private set; }


        public Topic(int id, string account, string header, string body, string location, bool urgency, string transport, int travelTime, DateTime dateHelpNeededStart, DateTime dateHelpNeededEnd, DateTime dateposted)
        {
            Id = id;
            Account = account;
            Header = header;
            Body = body;
            Location = location;
            Urgency = urgency;
            Transport = transport;
            TravelTime = travelTime;
            DateHelpNeededStart = dateHelpNeededStart;
            DateHelpNeededEnd = dateHelpNeededEnd;
            DatePosted = dateposted;
            DateLastEdited = dateposted;
            Flag = false;
            Active = true;
        }

        public Topic()
        {

        }

        public bool Edit(string header, string body, string location, bool urgency, string transport, int travelTime, DateTime dateHelpNeededStart, DateTime dateHelpNeededEnd, bool flag, bool active)
        {
            //To-do: check functie met bool toevoegen
            Header = header;
            Body = body;
            Location = location;
            Urgency = urgency;
            Transport = transport;
            TravelTime = travelTime;
            DateHelpNeededStart = dateHelpNeededStart;
            DateHelpNeededEnd = dateHelpNeededEnd;
            Flag = flag;
            Active = active;
            return true;
        }

        public void SetFlag(bool flag)
        {
            Flag = flag;
        }

        public override string ToString()
        {
            //nog aanpassen: twee data bij afspraak
            string ccalc = string.Empty;
            if ((DateHelpNeededStart.DayOfYear - DateTime.Today.DayOfYear) < 0)
            {
                ccalc = "hulpdatum is al geweest";
            }
            else
            {
                ccalc = (DateHelpNeededStart.DayOfYear - DateTime.Today.DayOfYear).ToString() + " dagen";
            }

            if (Urgency)
            {
                return "[URGENT] " + Header + " -  Voor over: " + ccalc;
            }
            return Header + " - Voor over: " + ccalc;
        }



    }

}
