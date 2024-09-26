using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TechBooks.Models;

namespace TechBooks.Data.ADO.Net
{
    public static class AuthorBooksData
    {
        public static List<Book> GetNonAssociatedBookList(int authorId, SqlConnection cn)
        {
            var result = new List<Book>();
            using (var cmd = new SqlCommand("SELECT * FROM Books " +
                                            "WHERE BookId NOT IN ( " +
                                            "   SELECT DISTINCT BookId " +
                                            "   FROM AuthorBooks " +
                                            "   WHERE AuthorId=@AuthorId" +
                                            ")", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.Parameters.AddWithValue("AuthorId", authorId);
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

        public static List<Book> GetAssociatedBookList(int authorId, SqlConnection cn)
        {
            var result = new List<Book>();
            using (var cmd = new SqlCommand("SELECT * FROM Books " +
                                            "WHERE BookId IN ( " +
                                            "   SELECT DISTINCT BookId " +
                                            "   FROM AuthorBooks " +
                                            "   WHERE AuthorId=@AuthorId" +
                                            ")", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.Parameters.AddWithValue("AuthorId", authorId);
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

        public static List<Author> GetNonAssociatedAuthorList(int bookId, SqlConnection cn)
        {
            var result = new List<Author>();
            using (var cmd = new SqlCommand("SELECT * FROM Authors " +
                                            "WHERE AuthorId NOT IN ( " +
                                            "   SELECT DISTINCT AuthorID " +
                                            "   FROM AuthorBooks " +
                                            "   WHERE BookId=@BookId" +
                                            ")", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.Parameters.AddWithValue("BookId", bookId);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var author = new Author()
                    {
                        AuthorId = Convert.ToInt32(dr["AuthorId"]),
                        Name = Convert.ToString(dr["Name"])
                    };
                    result.Add(author);
                }
                dr.Close();
            }
            return result;
        }

        public static List<Author> GetAssociatedAuthorList(int bookId, SqlConnection cn)
        {
            var result = new List<Author>();
            using (var cmd = new SqlCommand("SELECT * FROM Authors " +
                                            "WHERE AuthorId IN ( " +
                                            "   SELECT DISTINCT AuthorID " +
                                            "   FROM AuthorBooks " +
                                            "   WHERE BookId=@BookId" +
                                            ")", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.Parameters.AddWithValue("BookId", bookId);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var author = new Author()
                    {
                        AuthorId = Convert.ToInt32(dr["AuthorId"]),
                        Name = Convert.ToString(dr["Name"])
                    };
                    result.Add(author);
                }
                dr.Close();
            }
            return result;
        }

        public static void Insert(int authorId, int bookId, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("INSERT INTO AuthorBooks (AuthorId, BookId, CreationDate) VALUES (@AuthorId, @BookId, @CreationDate)", cn))
            {
                cmd.Parameters.AddWithValue("AuthorId", authorId);
                cmd.Parameters.AddWithValue("BookId", bookId);
                cmd.Parameters.AddWithValue("CreationDate", DateTime.Now);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int authorId, int bookId, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("DELETE AuthorBooks WHERE AuthorId=@AuthorId AND BookId=@BookId", cn))
            {
                cmd.Parameters.AddWithValue("AuthorId", authorId);
                cmd.Parameters.AddWithValue("BookId", bookId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
