using System;
using System.Data.SqlClient;
using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;
using TechBooks.Models;

namespace TechBooks.WebUI.Login
{
    public partial class Register : CommonBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    string passwordHash = Security.ComputePasswordHash(txtNewEmail.Text, txtNewPassword1.Text);
                    var user = new User() { Email = txtNewEmail.Text, Password = passwordHash };

                    if (!UserData.UserIsUnique(user.Email, cn))
                    {
                        throw new Exception("This e-mail address is already registered");
                    }

                    UserData.Insert(user, cn);
                    txtNewEmail.Text = "";
                    txtNewPassword1.Text = "";
                    txtNewPassword2.Text = "";

                    Response.Redirect("Success.aspx");
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }
    }
}