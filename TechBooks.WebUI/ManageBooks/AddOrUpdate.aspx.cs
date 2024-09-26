using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechBooks.Models;
using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;

namespace TechBooks.WebUI.ManageBooks
{
    public partial class AddOrUpdate : CommonBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            GetCategories();

            if (Request.QueryString["BookId"] == null)
                Page.Title = "Add Book";
            else
            {
                Page.Title = "Update Book";
                btnSubmit.Text = "Update";
                GetBook();
            }
        }

        private void GetBook()
        {
            bool notFound = false;
            try
            {
                int bookId = Convert.ToInt32(Request.QueryString["BookId"]);
                using (var cn = new SqlConnection(ConnectionString))
                {
                    var book = BooksData.GetBook(bookId, cn);
                    if (book == null)
                        notFound = true;
                    else
                    {
                        txtTitle.Text = book.Title;
                        ddlCategory.SelectedValue = book.CategoryId.ToString();
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

        private void GetCategories()
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    var listOfCategories = CategoriesData.GetList(cn);
                    ddlCategory.Items.Add("(Select a Category)");
                    foreach (var category in listOfCategories)
                    {
                        var item = new ListItem(category.Description, category.CategoryId.ToString());
                        ddlCategory.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCategory.SelectedIndex == 0) throw new Exception("Please, select a Book Type");
                var book = new Book()
                {
                    Title = txtTitle.Text,
                    CategoryId = Convert.ToInt32(ddlCategory.SelectedValue)
                };

                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (Request.QueryString["BookId"] == null)
                        BooksData.Insert(book, cn);
                    else
                    {
                        book.BookId = Convert.ToInt32(Request.QueryString["BookId"]);
                        BooksData.Update(book, cn);
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