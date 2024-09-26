using System.Data.SqlClient;
using TechBooks.Models;
using System.Data;
using System;
using System.Collections.Generic;

namespace TechBooks.Data.ADO.Net
{
    public static class AuthorsData
    {
        public static void Insert(Author author, SqlConnection cn)
        {
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Authors WHERE Email=@Email", cn))
            {
                cmd.Parameters.AddWithValue("Email", author.Email);
                if ((int)cmd.ExecuteScalar() > 0)
                    throw new Exception("This email address has already been used.");
            }

            using (var cmd = new SqlCommand("INSERT INTO Authors (Name, Email, CreationDate) VALUES (@Name, @Email, @CreationDate)", cn))
            {
                cmd.Parameters.AddWithValue("Name", author.Name);
                cmd.Parameters.AddWithValue("Email", author.Email);
                cmd.Parameters.AddWithValue("CreationDate", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Author author, SqlConnection cn)
        {
            if (cn.State == ConnectionState.Closed) cn.Open();

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Authors WHERE AuthorId<>@AuthorId AND Email=@Email", cn))
            {
                cmd.Parameters.AddWithValue("AuthorId", author.AuthorId);
                cmd.Parameters.AddWithValue("Email", author.Email);
                if ((int)cmd.ExecuteScalar() > 0)
                    throw new Exception("This email address has already been used.");
            }

            using (var cmd = new SqlCommand("UPDATE Authors SET Name=@Name, Email=@Email WHERE AuthorId=@AuthorId", cn))
            {
                cmd.Parameters.AddWithValue("AuthorId", author.AuthorId);
                cmd.Parameters.AddWithValue("Name", author.Name);
                cmd.Parameters.AddWithValue("Email", author.Email);
                cmd.ExecuteNonQuery();
            }
        }

        public static Author GetAuthor(int authorId, SqlConnection cn)
        {
            Author result = null;
            using (var cmd = new SqlCommand("SELECT * FROM Authors WHERE AuthorId=@AuthorId", cn))
            {
                cmd.Parameters.AddWithValue("AuthorId", authorId);

                if (cn.State == ConnectionState.Closed) cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result = new Author()
                        {
                            AuthorId = Convert.ToInt32(dr["AuthorId"]),
                            Name = Convert.ToString(dr["Name"]),
                            Email = Convert.ToString(dr["Email"])
                        };
                        dr.Close();
                    }
                }
            }
            return result;
        }

        public static List<Author> GetList(SqlConnection cn)
        {
            var result = new List<Author>();
            using (var cmd = new SqlCommand("SELECT * FROM Authors", cn))
            {
                if (cn.State == ConnectionState.Closed) cn.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var author = new Author()
                    {
                        AuthorId = Convert.ToInt32(dr["AuthorId"]),
                        Name = Convert.ToString(dr["Name"]),
                        Email = Convert.ToString(dr["Email"])
                    };
                    result.Add(author);
                }
                dr.Close();
            }
            return result;
        }

        public static void Delete(Author author, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("DELETE Authors WHERE AuthorId=@AuthorId", cn))
            {
                cmd.Parameters.AddWithValue("AuthorId", author.AuthorId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool HasBooks(Author author, SqlConnection cn)
        {
            bool result = false;
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM AuthorBooks WHERE AuthorId=@AuthorId", cn))
            {
                cmd.Parameters.AddWithValue("AuthorId", author.AuthorId);
                if (cn.State == ConnectionState.Closed) cn.Open();
                result = (int)cmd.ExecuteScalar() > 0;
            }
            return result;
        }



    }

}
