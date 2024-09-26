using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechBooks.Models;

namespace TechBooks.Data.Dapper
{
    public static class AuthorsData
    {
        public static void Insert(Author author, SqlConnection cn)
        {
            int count = cn.ExecuteScalar<int>("SELECT COUNT(*) FROM Authors WHERE Email = @Email", new { author.Email });
            if (count > 0)
                throw new Exception("This email address has already been used.");

            cn.Execute("INSERT INTO Authors (Name, Email, CreationDate) VALUES (@Name, @Email, @CreationDate)", new { author.Name, author.Email, CreationDate = DateTime.Now });
        }

        public static void Update(Author author, SqlConnection cn)
        {
            int count = cn.ExecuteScalar<int>("SELECT COUNT(*) FROM Authors WHERE AuthorId <> @AuthorId AND Email = @Email", new { author.AuthorId, author.Email });
            if (count > 0)
                throw new Exception("This email address has already been used.");

            cn.Execute("UPDATE Authors SET Name=@Name, Email=@Email WHERE AuthorId=@AuthorId", new { author.AuthorId, author.Name, author.Email });
        }

        public static Author GetAuthor(int authorId, SqlConnection cn)
        {
            return cn.QuerySingleOrDefault<Author>("SELECT * FROM Authors WHERE AuthorId = @AuthorId", new { authorId });
        }

        public static List<Author> GetList(SqlConnection cn)
        {
            return cn.Query<Author>("SELECT * FROM Authors").ToList();
        }

        public static void Delete(Author author, SqlConnection cn)
        {
            cn.Execute("DELETE Authors WHERE AuthorId=@AuthorId", new { author.AuthorId });
        }

        public static bool HasBooks(Author author, SqlConnection cn)
        {
            int count = cn.ExecuteScalar<int>("SELECT COUNT(*) FROM AuthorBooks WHERE AuthorId = @AuthorId", new { author.AuthorId });
            return count > 0;
        }
    }
}
