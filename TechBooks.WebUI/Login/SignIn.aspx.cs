using System;
using System.Data.SqlClient;
using System.Web.Security;
using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;
using TechBooks.Models;

namespace TechBooks.WebUI.Login
{
    public partial class SignIn : CommonBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["RegisterSuccess"] == "1")
                (Master as Site).SuccessMessage = "Your user has been successfully been created";
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    string passwordHash = Security.ComputePasswordHash(txtEmail.Text, txtPassword.Text);
                    var user = new User() { Email = txtEmail.Text, Password = passwordHash };

                    if (!UserData.UserAndPasswordAreValid(user, cn))
                    {
                        throw new Exception("Invalid e-mail or password");
                    }
                    else
                        FormsAuthentication.RedirectFromLoginPage(txtEmail.Text, true);
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }
    }
}
