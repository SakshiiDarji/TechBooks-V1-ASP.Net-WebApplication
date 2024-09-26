using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using TechBooks.Models;
//using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;
using TechBooks.Data.ADO.Net;

namespace TechBooks.WebUI.ManageBooks
{
    public partial class Default : CommonBaseClass
    {
        private List<Category> liftOfcategories;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    liftOfcategories = CategoriesData.GetList(cn);
                    var listOfBooks = BooksData.GetList(cn);
                    dgBooks.DataSource = listOfBooks;
                    dgBooks.DataBind();
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }

        protected string GetCategoryDescription(object categoryId)
        {
            int id = Convert.ToInt32(categoryId);
            var category = liftOfcategories.FirstOrDefault(c => c.CategoryId == id);
            return category.Description;
        }

        protected void dgBooks_ItemCommand(object source, DataGridCommandEventArgs e)
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
                var book = new Book { BookId = bookId };
                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (BooksData.HasAuthors(book, cn))
                        throw new Exception("This Book cannot be removed because it has been associated with one or more authors. Remove all associations first.");
                    else
                        BooksData.Delete(book, cn);
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