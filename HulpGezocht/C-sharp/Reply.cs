using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulpGezocht
{
    public class Reply
    {
        public int Id { get; private set; }
        public Topic CurrentTopic { get; private set; }
        public int TopicID { get; private set; }
        //public Review CurrentReview { get; private set; }
        public int ReviewID { get; private set; }
        public string Body { get; private set; }
        public DateTime DatePosted { get; private set; }
        public DateTime DateLastEdited { get; private set; }
        public bool Flag { get; private set; }
        public bool Active { get; private set; }
        public string Email { get; private set; }
        //public string Account { get; private set; }

        public Reply(int id, string account, Topic currentTopic/*int topicid*/, string body, DateTime datePosted)
        {
            //Id niet gebruikt
            Id = id;
            Flag = false;
            Email = account;
            //TopicID = topicid;
            CurrentTopic = currentTopic;
            Body = body;
            DatePosted = datePosted;
            Active = true;
        }

        public Reply(int id, string account, int topicid/*int topicid*/, string body, DateTime datePosted)
        {
            //Id niet gebruikt
            Id = id;
            Flag = false;
            Email = account;
            TopicID = topicid;
            //CurrentTopic = currentTopic;
            Body = body;
            DatePosted = datePosted;
            Active = true;
        }


        //public Reply(/*User account*/string account, /*Review currentReview*/int reviewid, string body, DateTime datePosted)
        //{
        //Account = account;
        //ReviewID = reviewid;
        //CurrentReview = currentReview;
        //Body = body;
        //DatePosted = datePosted;
        //Active = true;
        //}

        public bool Edit(string body, DateTime dateLastEdited, bool flag, bool active)
        {
            //To-do: check functie met bool toevoegen
            Body = body;
            DateLastEdited = dateLastEdited;
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
            string reply = Id + ". " + Email + ": [" + DatePosted + "] " + Body;
            return reply;
        }
    }
}
