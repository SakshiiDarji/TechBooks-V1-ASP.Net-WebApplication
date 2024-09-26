using System;
using System.Web.Security;
using System.Web.UI;

namespace TechBooks.WebUI
{
    public partial class Site : System.Web.UI.MasterPage
    {

        public string DangerMessage
        {
            set
            {
                lblErrorMessage.Text = value;
                pnlDangerMessage.Visible = !string.IsNullOrEmpty(value);
            }
        }

        public string SuccessMessage
        {
            set
            {
                lblSuccessMessage.Text = value;
                pnlSuccessMessage.Visible = !string.IsNullOrEmpty(value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Title == "Home")
                lblTitle.Visible = false;
            else
                lblTitle.Text = Page.Title;

            if (Page.User.Identity.IsAuthenticated)        //this is in-built in web forms
            {
                pnlSignedIn.Visible = true;
                pnlSignedOut.Visible = false;
            }
            else
            {
                pnlSignedIn.Visible = false;
                pnlSignedOut.Visible = true;
            }

            DangerMessage = "";
            SuccessMessage = "";

        }

        protected void lkbSignOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();         // this is also in-build in web forms 
            Response.Redirect("/");
        }
    }
}