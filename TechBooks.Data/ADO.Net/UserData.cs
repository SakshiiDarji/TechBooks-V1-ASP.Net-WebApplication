using System;
using System.Data;
using System.Data.SqlClient;
using TechBooks.Models;

namespace TechBooks.Data.ADO.Net
{
    public static class UserData
    {
        public static void Insert(User user, SqlConnection cn)
        {
            using (var cmd = new SqlCommand("INSERT INTO Users (Email, Password, CreationDate) VALUES (@Email, @Password, @CreationDate)", cn))
            {
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("Password", user.Password);
                cmd.Parameters.AddWithValue("CreationDate", DateTime.Now);
                if (cn.State == ConnectionState.Closed) cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static bool UserIsUnique(string email, SqlConnection cn)
        {
            bool result = false;
            using (var cmd = new SqlCommand("SELECT COUNT(Email) FROM Users WHERE Email=@Email", cn))
            {
                cmd.Parameters.AddWithValue("Email", email);
                if (cn.State == ConnectionState.Closed) cn.Open();
                var count = (int)cmd.ExecuteScalar();
                result = count == 0;
            }
            return result;
        }

        public static bool UserAndPasswordAreValid(User user, SqlConnection cn)
        {
            bool result = false;
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@Email AND Password=@Password", cn))
            {
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("Password", user.Password);
                if (cn.State == ConnectionState.Closed) cn.Open();
                var count = (int)cmd.ExecuteScalar();
                result = count >= 1;
            }
            return result;
        }
    }
}
