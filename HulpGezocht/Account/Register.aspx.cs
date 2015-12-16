using System;
using System.Linq;
using System.Web;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using HulpGezocht.Models;

namespace HulpGezocht.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VisibilityChange();
        }

        public void CreateUser_Click(object sender, EventArgs e)
        {
        }

        public void VisibilityChange(object sender, EventArgs e)
        {
            VisibilityChange();
        }

        public void VisibilityChange()
        {
            if (Rol.SelectedValue == "Vrijwilliger")
            {
                VOGLabel.Visible = true;
                VOGLabel.Enabled = true;

                VOGUpload.Visible = true;
                VOGUpload.Enabled = true;

                VOGValidator.Enabled = true;

                ProfilePicValidator.Enabled = true;
            }
            else
            {
                VOGLabel.Visible = false;
                VOGLabel.Enabled = false;

                VOGUpload.Visible = false;
                VOGUpload.Enabled = false;

                VOGValidator.Enabled = false;

                ProfilePicValidator.Enabled = false;
            }
        }

        public void RegisterButton_Click (object sender, EventArgs e)
        {
            if (ProfilePicUpload.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(ProfilePicUpload.FileName);
                    ProfilePicUpload.SaveAs(Server.MapPath("~/") + filename);
                    
                }
                catch (Exception ex)
                {
                    //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }

            if (VOGUpload.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(VOGUpload.FileName);
                    VOGUpload.SaveAs(Server.MapPath("~/") + filename);
                    //StatusLabel.Text = "Upload status: File uploaded!";
                }
                catch (Exception ex)
                {
                    //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }
    }
}