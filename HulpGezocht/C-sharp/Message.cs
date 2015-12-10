using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulpGezocht
{
    public class ChatMessage
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public DateTime DatePosted { get; private set; }
        public string Sender { get; private set; }
        public string Receiver { get; private set; }

        public ChatMessage(int id, string text, string sender, string receiver, DateTime dateposted)
        {
            Id = id;
            DatePosted = dateposted;
            Sender = sender;
            Receiver = receiver;
            Text = text;
        }

        /*public override string ToString()
        {           
            return Database.NameGetter(Sender).Name + "\n" + DatePosted.Day.ToString("00") + "/" + DatePosted.Month.ToString("00") + " " + DatePosted.Hour.ToString("00") + ":" + DatePosted.Minute.ToString("00") + "    " + Text;
        }*/
    }
}
