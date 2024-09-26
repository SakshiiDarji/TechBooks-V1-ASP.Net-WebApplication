using Dapper;
using System;
using System.Data.SqlClient;
using TechBooks.Models;

namespace TechBooks.Data.Dapper
{
    public static class UserData
    {
        public static void Insert(User user, SqlConnection cn)
        {
            string sql = "INSERT INTO Users (Email, Password, CreationDate) VALUES (@Email, @Password, @CreationDate)";
            cn.Execute(sql, new { user.Email, user.Password, CreationDate = DateTime.Now });
        }

        public static bool UserIsUnique(string email, SqlConnection cn)
        {
            string sql = "SELECT COUNT(Email) FROM Users WHERE Email = @Email";
            int count = cn.ExecuteScalar<int>(sql, new { Email = email });
            return count == 0;
        }

        public static bool UserAndPasswordAreValid(User user, SqlConnection cn)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";
            int count = cn.ExecuteScalar<int>(sql, new { user.Email, user.Password });
            return count >= 1;
        }

    }
}
