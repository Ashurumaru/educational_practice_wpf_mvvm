using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace educational_practice.Models
{
    internal class DataBaseLogicModel
    {
        private readonly string connectionString;

        public DataBaseLogicModel(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool IsValidLogin(string login, string password)
        {
            bool validUser;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [User] WHERE Login=@Login AND Password=@Password";
                command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                validUser = command.ExecuteScalar() != null;
            }
            return validUser;
        }

        public bool LoginExists(string login)
        {
            bool exists;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM [User] WHERE Login=@Login";
                command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;

                int count = (int)command.ExecuteScalar();
                exists = count > 0;
            }
            return exists;
        }

        public void CreateUser(string login, string password, string firstName, string lastName, string middleName)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [User] (Login, Password, FirstName, LastName, MiddleName, AccessLevel) " +
                                      "VALUES (@Login, @Password, @FirstName, @LastName, @MiddleName, 1)";
                command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
                command.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = middleName;

                command.ExecuteNonQuery();
            }
        }

        public void UpdateUser(int userId, string login, string password, string firstName, string lastName, string middleName)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [User] SET Login=@Login, Password=@Password, FirstName=@FirstName, " +
                                      "LastName=@LastName, MiddleName=@MiddleName WHERE Id=@UserId";
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
                command.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = middleName;

                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [User] WHERE Id=@UserId";
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                command.ExecuteNonQuery();
            }
        }

        public DataTable GetAllUsers()
        {
            DataTable dataTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT * FROM [User]", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }
            return dataTable;
        }
    }
}

