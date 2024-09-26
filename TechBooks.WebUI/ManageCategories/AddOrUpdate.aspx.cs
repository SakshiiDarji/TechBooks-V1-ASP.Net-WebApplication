using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using TechBooks.Models;
using TechBooks.Data.ADO.Net;
//using TechBooks.Data.Dapper;

namespace TechBooks.WebUI.ManageCategories
{
    public partial class AddOrUpdate : CommonBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (Request.QueryString["CategoryId"] == null)
                Page.Title = "Add Category";
            else
            {
                Page.Title = "Update Category";
                btnSubmit.Text = "Update";
                GetBookType();
            }
        }

        private void GetBookType()
        {
            bool notFound = false;
            try
            {
                int categoryId = Convert.ToInt32(Request.QueryString["CategoryId"]);
                using (var cn = new SqlConnection(ConnectionString))
                {
                    var category = CategoriesData.GetCategory(categoryId, cn);
                    if (category == null)
                        notFound = true;
                    else
                    {
                        txtDescription.Text = category.Description;
                    }
                }
            }
            catch (Exception ex)
            {
                (Master as Site).DangerMessage = ex.Message;
            }

            if (notFound)
                Response.Redirect("/NotFound.aspx?entity=Category&backUrl=/ManageCategories");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var category = new Category() { Description = txtDescription.Text };
                using (var cn = new SqlConnection(ConnectionString))
                {
                    if (Request.QueryString["CategoryId"] == null)
                        CategoriesData.Insert(category, cn);
                    else
                    {
                        category.CategoryId = Convert.ToInt32(Request.QueryString["CategoryId"]);
                        CategoriesData.Update(category, cn);
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