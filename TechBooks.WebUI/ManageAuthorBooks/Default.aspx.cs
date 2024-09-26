using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;

namespace TechBooks.WebUI.ManageAuthorBooks
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
                    int authorId = Convert.ToInt32(Request.QueryString["AuthorId"]);
                    var author = AuthorsData.GetAuthor(authorId, cn);
                    if (author == null)
                        notFound = true;
                    else
                    {
                        Page.Title = $"Manage {author.Name} Books";

                        if (!IsPostBack)
                        {
                            GetNonAssociatedBooks(authorId, cn);
                            GetAssociatedBooks(authorId, cn);
                        }
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

        private void GetNonAssociatedBooks(int authorId, SqlConnection cn)
        {
            var listOfNonAssociatedBooks = AuthorBooksData.GetNonAssociatedBookList(authorId, cn);
            ddlBook.Items.Clear();
            ddlBook.Items.Add("(Select a book to add to this author)");
            foreach (var book in listOfNonAssociatedBooks)
            {
                var item = new ListItem(book.Title, book.BookId.ToString());
                ddlBook.Items.Add(item);
            }

            if (listOfNonAssociatedBooks.Count == 0)
            {
                pnlBooksToAdd.Visible = false;
                pnlNothingToAdd.Visible = true;
            }
            else
            {
                pnlBooksToAdd.Visible = true;
                pnlNothingToAdd.Visible = false;
            }

        }

        private void GetAssociatedBooks(int authorId, SqlConnection cn)
        {
            var listOfAssociatedBooks = AuthorBooksData.GetAssociatedBookList(authorId, cn);
            dgAuthorBooks.DataSource = listOfAssociatedBooks;
            dgAuthorBooks.DataBind();

            if (listOfAssociatedBooks.Count == 0)
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

        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                int authorId = Convert.ToInt32(Request.QueryString["AuthorId"]);
                int bookId = Convert.ToInt32(ddlBook.SelectedValue);

                using (var cn = new SqlConnection(ConnectionString))
                {
                    AuthorBooksData.Insert(authorId, bookId, cn);
                    GetNonAssociatedBooks(authorId, cn);
                    GetAssociatedBooks(authorId, cn);
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }

        protected void dgAuthorBooks_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "RemoveBook")
            {
                int bookId = Convert.ToInt32(e.CommandArgument);
                Remove(bookId);
            }
        }

        void Remove(int bookId)
        {
            try
            {
                int authorId = Convert.ToInt32(Request.QueryString["AuthorId"]);
                using (var cn = new SqlConnection(ConnectionString))
                {
                    AuthorBooksData.Delete(authorId, bookId, cn);
                    GetNonAssociatedBooks(authorId, cn);
                    GetAssociatedBooks(authorId, cn);
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