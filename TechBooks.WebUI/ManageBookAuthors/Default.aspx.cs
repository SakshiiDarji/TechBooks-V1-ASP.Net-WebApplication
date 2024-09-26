using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;

namespace TechBooks.WebUI.ManageBookAuthors
{
    public partial class Default : CommonBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool notFound = false;
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    int bookId = Convert.ToInt32(Request.QueryString["BookId"]);
                    var book = BooksData.GetBook(bookId, cn);
                    if (book == null)
                        notFound = true;
                    else
                    {
                        Page.Title = $"Manage {book.Title} Authors";

                        if (!IsPostBack)
                        {
                            GetNonAssociatedAuthors(bookId, cn);
                            GetAssociatedAuthors(bookId, cn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }

            if (notFound)
                Response.Redirect("/NotFound.aspx?entity=Book&backUrl=/ManageBooks");
        }

        private void GetNonAssociatedAuthors(int bookId, SqlConnection cn)
        {
            var listOfNonAssociatedAuthors = AuthorBooksData.GetNonAssociatedAuthorList(bookId, cn);
            ddlAuthor.Items.Clear();
            ddlAuthor.Items.Add("(Select an author to add to this book)");
            foreach (var author in listOfNonAssociatedAuthors)
            {
                var item = new ListItem(author.Name, author.AuthorId.ToString());
                ddlAuthor.Items.Add(item);
            }

            if (listOfNonAssociatedAuthors.Count == 0)
            {
                pnlAuthorsToAdd.Visible = false;
                pnlNothingToAdd.Visible = true;
            }
            else
            {
                pnlAuthorsToAdd.Visible = true;
                pnlNothingToAdd.Visible = false;
            }

        }

        private void GetAssociatedAuthors(int bookId, SqlConnection cn)
        {
            var listOfAssociatedAuthors = AuthorBooksData.GetAssociatedAuthorList(bookId, cn);
            dgBookAuthors.DataSource = listOfAssociatedAuthors;
            dgBookAuthors.DataBind();

            if (listOfAssociatedAuthors.Count == 0)
            {
                pnlAssociations.Visible = false;
                pnlNoAssociation.Visible = true;
            }
            else
            {
                pnlAssociations.Visible = true;
                pnlNoAssociation.Visible = false;
            }
        }

        protected void btnAddAuthor_Click(object sender, EventArgs e)
        {
            try
            {
                int bookId = Convert.ToInt32(Request.QueryString["BookId"]);
                int authorId = Convert.ToInt32(ddlAuthor.SelectedValue);

                using (var cn = new SqlConnection(ConnectionString))
                {
                    AuthorBooksData.Insert(authorId, bookId, cn);
                    GetNonAssociatedAuthors(bookId, cn);
                    GetAssociatedAuthors(bookId, cn);
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }

        protected void dgBookAuthors_ItemCommand(object source, DataGridCommandEventArgs e)
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
                int bookId = Convert.ToInt32(Request.QueryString["BookId"]);
                using (var cn = new SqlConnection(ConnectionString))
                {
                    AuthorBooksData.Delete(authorId, bookId, cn);
                    GetNonAssociatedAuthors(bookId, cn);
                    GetAssociatedAuthors(bookId, cn);
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