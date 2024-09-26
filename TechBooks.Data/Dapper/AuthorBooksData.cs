using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechBooks.Models;

namespace TechBooks.Data.Dapper
{
    public static class AuthorBooksData
    {
        public static List<Book> GetNonAssociatedBookList(int authorId, SqlConnection cn)
        {
            string sql = @"
                SELECT * FROM Books
                WHERE BookId NOT IN (
                    SELECT DISTINCT BookId
                    FROM AuthorBooks
                    WHERE AuthorId = @AuthorId
                )";
            return cn.Query<Book>(sql, new { authorId }).ToList();
        }

        public static List<Book> GetAssociatedBookList(int authorId, SqlConnection cn)
        {
            string sql = @"
                SELECT * FROM Books
                WHERE BookId IN (
                    SELECT DISTINCT BookId
                    FROM AuthorBooks
                    WHERE AuthorId = @AuthorId
                )";
            return cn.Query<Book>(sql, new { authorId }).ToList();
        }

        public static List<Author> GetNonAssociatedAuthorList(int bookId, SqlConnection cn)
        {
            string sql = @"
                SELECT * FROM Authors
                WHERE AuthorId NOT IN (
                    SELECT DISTINCT AuthorId
                    FROM AuthorBooks
                    WHERE BookId = @BookId
                )";
            return cn.Query<Author>(sql, new { bookId }).ToList();
        }

        public static List<Author> GetAssociatedAuthorList(int bookId, SqlConnection cn)
        {
            string sql = @"
                SELECT * FROM Authors
                WHERE AuthorId IN (
                    SELECT DISTINCT AuthorId
                    FROM AuthorBooks
                    WHERE BookId = @BookId
                )";
            return cn.Query<Author>(sql, new { bookId }).ToList();
        }

        public static void Insert(int authorId, int bookId, SqlConnection cn)
        {
            string sql = "INSERT INTO AuthorBooks (AuthorId, BookId, CreationDate) VALUES (@AuthorId, @BookId, @CreationDate)";
            cn.Execute(sql, new { authorId, bookId, CreationDate = DateTime.Now });
        }

        public static void Delete(int authorId, int bookId, SqlConnection cn)
        {
            string sql = "DELETE FROM AuthorBooks WHERE AuthorId = @AuthorId AND BookId = @BookId";
            cn.Execute(sql, new { authorId, bookId });
        }

    }
}
