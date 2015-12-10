using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HulpGezocht
{
    public class Review
    {
        public int Id { get; private set; }
        public string Account { get; private set; }
        public string Body { get; private set; }
        public DateTime DatePosted { get; private set; }
        public bool Flag { get; private set; }
        public int Rating { get; private set; }
        public bool Active { get; private set; }      

        public Review(int id, string account, string body, int rating, DateTime posted)
        {
            //Id niet gebruikt, nu tijdelijk wel
            Id = id;
            Body = body;
            DatePosted = posted;
            Flag = false;
            Rating = rating;
            Active = true;
            Account = account;
        }


        public bool Edit(string body, bool flag, int rating, bool active)
        {
            //To-do: check functie met bool toevoegen
            Body = body;
            Flag = flag;
            Rating = rating;
            Active = active;
            return true;
        }

        public void SetFlag(bool flag)
        {
            Flag = flag;
        }

        public override string ToString()
        {
            string rating = "";
            for (int i = 0; i < Rating; i++)
            {
                rating += "*";
            }
            return DatePosted + " - " + Body + " - " + rating;
        }
    }
}
