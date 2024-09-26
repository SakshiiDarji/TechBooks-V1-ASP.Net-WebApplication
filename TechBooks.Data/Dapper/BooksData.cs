using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechBooks.Models;

namespace TechBooks.Data.Dapper
{
    public static class BooksData
    {
        public static void Insert(Book book, SqlConnection cn)
        {
            string sql = "INSERT INTO Books (Title, CategoryId, CreationDate) VALUES (@Title, @CategoryId, @CreationDate)";

            cn.Execute(sql, new { book.Title, book.CategoryId, CreationDate = DateTime.Now });
        }


        public static void Update(Book book, SqlConnection cn)
        {
            string sql = "UPDATE Books SET Title = @Title, CategoryId = @CategoryId WHERE BookId = @BookId";

            cn.Execute(sql, new { book.BookId, book.Title, book.CategoryId });
        }

        public static Book GetBook(int bookId, SqlConnection cn)
        {
            string sql = "SELECT * FROM Books WHERE BookId = @BookId";

            return cn.QuerySingleOrDefault<Book>(sql, new { BookId = bookId });
        }

        public static List<Book> GetList(SqlConnection cn)
        {
            string sql = "SELECT * FROM Books";

            return cn.Query<Book>(sql).ToList();
        }

        public static void Delete(Book book, SqlConnection cn)
        {
            string sql = "DELETE FROM Books WHERE BookId = @BookId";

            cn.Execute(sql, new { book.BookId });
        }

        public static bool HasAuthors(Book book, SqlConnection cn)
        {
            string sql = "SELECT COUNT(*) FROM AuthorBooks WHERE BookId = @BookId";
            int count = cn.ExecuteScalar<int>(sql, new { book.BookId });
            return count > 0;
        }
    }
}
