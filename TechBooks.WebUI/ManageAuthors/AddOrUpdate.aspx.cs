using System;
using System.Data.SqlClient;
using System.Web.UI;
using TechBooks.Models;
using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;

namespace TechBooks.WebUI.ManageAuthors
{
    public partial class AddOrUpdate : CommonBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (Request.QueryString["AuthorId"] == null)
                Page.Title = "Add Author";
            else
            {
                Page.Title = "Update Author";
                btnSubmit.Text = "Update";
                GetAuthor();
            }
        }

        private void GetAuthor()
        {
            bool notFound = false;
            try
            {
                int authorId = Convert.ToInt32(Request.QueryString["AuthorId"]);
                using (var cn = new SqlConnection(ConnectionString))
                {
                    var author = AuthorsData.GetAuthor(authorId, cn);
                    if (author == null)
                        notFound = true;
                    else
                    {
                        txtName.Text = author.Name;
                        txtEmail.Text = author.Email;
                    }
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }

            if (notFound)
                Response.Redirect("/NotFound.aspx?entity=Author&backUrl=/ManageAuthors");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var author = new Author()
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                };

                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (Request.QueryString["AuthorId"] == null)
                        AuthorsData.Insert(author, cn);
                    else
                    {
                        author.AuthorId = Convert.ToInt32(Request.QueryString["AuthorId"]);
                        AuthorsData.Update(author, cn);
                    }
                }

                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }
    }
}