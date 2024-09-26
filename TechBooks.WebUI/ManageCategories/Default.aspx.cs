using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using TechBooks.Models;
using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;

namespace TechBooks.WebUI.ManageCategories
{
    public partial class Default : CommonBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (var cn = new SqlConnection(ConnectionString))
                {
                    var listOfCategories = CategoriesData.GetList(cn);
                    dgCategories.DataSource = listOfCategories;
                    dgCategories.DataBind();
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }
        }

        protected void dgCategories_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "RemoveCategory")
            {
                int categoryId = Convert.ToInt32(e.CommandArgument);
                Remove(categoryId);
            }
        }

        void Remove(int categoryId)
        {
            try
            {
                var category = new Category { CategoryId = categoryId };
                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (CategoriesData.HasBooks(category, cn))
                        throw new Exception("This Category cannot be removed because it has been associated with one or more books. Remove all associations first.");
                    else
                        CategoriesData.Delete(category, cn);
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