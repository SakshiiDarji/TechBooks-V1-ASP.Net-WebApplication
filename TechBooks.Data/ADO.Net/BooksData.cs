using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TechBooks.Models;

namespace TechBooks.Data.ADO.Net
{
    public static class BooksData
    {
        public static void Insert(Book book, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("INSERT INTO Books (Title, CategoryId, CreationDate) VALUES (@Title, @CategoryId, @CreationDate)", cn))
            {
                cmd.Parameters.AddWithValue("Title", book.Title);
                cmd.Parameters.AddWithValue("CategoryId", book.CategoryId);
                cmd.Parameters.AddWithValue("CreationDate", DateTime.Now);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Book book, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("UPDATE Books SET Title=@Title, CategoryId=@CategoryId WHERE BookId=@BookId", cn))
            {
                cmd.Parameters.AddWithValue("BookId", book.BookId);
                cmd.Parameters.AddWithValue("Title", book.Title);
                cmd.Parameters.AddWithValue("CategoryId", book.CategoryId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static Book GetBook(int bookId, SqlConnection cn)
        {
            Book result = null;
            using (var cmd = new SqlCommand("SELECT * FROM Books WHERE BookId=@BookId", cn))
            {
                cmd.Parameters.AddWithValue("BookId", bookId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result = new Book()
                        {
                            BookId = Convert.ToInt32(dr["BookId"]),
                            Title = Convert.ToString(dr["Title"]),
                            CategoryId = Convert.ToInt32(dr["CategoryId"])
                        };
                        dr.Close();
                    }
                }
            }
            return result;
        }

        public static List<Book> GetList(SqlConnection cn)
        {
            var result = new List<Book>();
            using (var cmd = new SqlCommand("SELECT * FROM Books", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var book = new Book()
                    {
                        BookId = Convert.ToInt32(dr["BookId"]),
                        Title = Convert.ToString(dr["Title"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"])
                    };
                    result.Add(book);
                }
                dr.Close();
            }
            return result;
        }

        public static void Delete(Book book, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("DELETE Books WHERE BookId=@BookId", cn))
            {
                cmd.Parameters.AddWithValue("BookId", book.BookId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool HasAuthors(Book book, SqlConnection cn)
        {
            bool result = false;
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM AuthorBooks WHERE BookId=@BookId", cn))
            {
                cmd.Parameters.AddWithValue("BookId", book.BookId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                result = (int)cmd.ExecuteScalar() > 0;
            }
            return result;
        }
    }
}
