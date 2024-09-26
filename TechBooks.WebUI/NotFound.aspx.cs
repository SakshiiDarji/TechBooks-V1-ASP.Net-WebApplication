using System;
using System.Web.UI;

namespace TechBooks.WebUI
{
    public partial class NotFound : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["entity"]))
                Page.Title = Request.QueryString["entity"] + " not found!";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["backUrl"]))
                Response.Redirect(Request.QueryString["backUrl"]);
        }
    }
}