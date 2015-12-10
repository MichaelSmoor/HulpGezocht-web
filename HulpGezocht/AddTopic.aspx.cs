using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HulpGezocht
{
    public partial class AddTopic : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateOptions();
        }

        public void DateOptionChange(object sender, EventArgs e)
        {
            DateOptions();
        }

        private void DateOptions()
        {
            if (DatumOpties.SelectedValue == "Specifieke datum")
            {
                Datum1Label.Text = "Datum";

                Datum2Label.Visible = false;
                Datum2.Visible = false;
                Datum2Validator.Enabled = false;
            }

            else
            {
                Datum1Label.Text = "Datum #1";

                Datum2Label.Visible = true;
                Datum2.Visible = true;
                Datum2Validator.Enabled = true;
            }
        }


        public void AddTopic_Click(object sender, EventArgs e)
        {
        }
    }
}