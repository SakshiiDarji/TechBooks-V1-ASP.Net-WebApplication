using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using TechBooks.Models;
//using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;
using TechBooks.Data.ADO.Net;

namespace TechBooks.WebUI.ManageAuthors
{
    public partial class Default : CommonBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    var listOfAuthors = AuthorsData.GetList(cn);
                    dgAuthors.DataSource = listOfAuthors;
                    dgAuthors.DataBind();
                    
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }


        protected void dgAuthors_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "RemoveAuthor")
            {
                int authorId = Convert.ToInt32(e.CommandArgument);
                Remove(authorId);
            }
        }

        void Remove(int authorId)
        {
            try
            {
                var author = new Author { AuthorId = authorId };
                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (AuthorsData.HasBooks(author, cn))
                        throw new Exception("This Author cannot be removed because it has been associated with one or more books. Remove all associations first.");
                    else
                        AuthorsData.Delete(author, cn);
                }
                Page_Load(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }
    }
}