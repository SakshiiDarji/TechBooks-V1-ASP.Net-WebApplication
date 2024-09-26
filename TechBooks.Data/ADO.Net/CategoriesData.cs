using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TechBooks.Models;

namespace TechBooks.Data.ADO.Net
{
    public static class CategoriesData
    {
        public static void Insert(Category category, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("INSERT INTO Categories (Description, CreationDate) VALUES (@Description, @CreationDate)", cn))
            {
                cmd.Parameters.AddWithValue("Description", category.Description);
                cmd.Parameters.AddWithValue("CreationDate", DateTime.Now);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Category category, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("UPDATE Categories SET Description=@Description WHERE CategoryId=@CategoryId", cn))
            {
                cmd.Parameters.AddWithValue("CategoryId", category.CategoryId);
                cmd.Parameters.AddWithValue("Description", category.Description);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static Category GetCategory(int categoryId, SqlConnection cn)
        {
            Category result = null;
            using (var cmd = new SqlCommand("SELECT * FROM Categories WHERE CategoryId=@CategoryId", cn))
            {
                cmd.Parameters.AddWithValue("CategoryId", categoryId);
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result = new Category()
                        {
                            CategoryId = Convert.ToInt32(dr["CategoryId"]),
                            Description = Convert.ToString(dr["Description"])
                        };
                        dr.Close();
                    }
                }
            }
            return result;
        }

        public static List<Category> GetList(SqlConnection cn)
        {
            var result = new List<Category>();
            using (var cmd = new SqlCommand("SELECT * FROM Categories", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var category = new Category()
                    {
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        Description = Convert.ToString(dr["Description"])
                    };
                    result.Add(category);
                }
                dr.Close();
            }
            return result;
        }

        public static void Delete(Category category, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("DELETE Categories WHERE CategoryId=@CategoryId", cn))
            {
                cmd.Parameters.AddWithValue("CategoryId", category.CategoryId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool HasBooks(Category category, SqlConnection cn)
        {
            bool result = false;
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Books WHERE CategoryId=@CategoryId", cn))
            {
                cmd.Parameters.AddWithValue("CategoryId", category.CategoryId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                result = (int)cmd.ExecuteScalar() > 0;
            }
            return result;
        }
    }
}
