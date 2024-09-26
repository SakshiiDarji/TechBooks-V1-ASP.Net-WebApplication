using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechBooks.Models;

namespace TechBooks.Data.Dapper
{
    public static class CategoriesData
    {
        public static void Insert(Category category, SqlConnection cn)
        {
            string sql = "INSERT INTO Categories (Description, CreationDate) VALUES (@Description, @CreationDate)";

            cn.Execute(sql, new { category.Description, CreationDate = DateTime.Now });
        }

        public static void Update(Category category, SqlConnection cn)
        {
            string sql = "UPDATE Categories SET Description = @Description WHERE CategoryId = @CategoryId";

            cn.Execute(sql, new { category.CategoryId, category.Description });
        }

        public static Category GetCategory(int categoryId, SqlConnection cn)
        {
            string sql = "SELECT * FROM Categories WHERE CategoryId = @CategoryId";

            return cn.QuerySingleOrDefault<Category>(sql, new { CategoryId = categoryId });
        }

        public static List<Category> GetList(SqlConnection cn)
        {
            string sql = "SELECT * FROM Categories";

            return cn.Query<Category>(sql).ToList();
        }

        public static void Delete(Category category, SqlConnection cn)
        {
            string sql = "DELETE FROM Categories WHERE CategoryId = @CategoryId";

            cn.Execute(sql, new { category.CategoryId });
        }

        public static bool HasBooks(Category category, SqlConnection cn)
        {
            string sql = "SELECT COUNT(*) FROM Books WHERE CategoryId=@CategoryId";
            int count = cn.ExecuteScalar<int>(sql, new { category.CategoryId });
            return count > 0;
        }
    }
}
