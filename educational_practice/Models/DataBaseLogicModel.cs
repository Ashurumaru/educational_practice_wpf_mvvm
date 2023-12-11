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
                                      "VALUES (@Login, @Password, @FirstName, @LastName, @MiddleName, 0)";
                command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
                if (middleName != null)
                {
                    command.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = middleName;
                }
                else
                {
                    command.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = " ";
                }
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUser(UserModel user)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [User] SET Login=@Login, Password=@Password, FirstName=@FirstName, " +
                                      "LastName=@LastName, MiddleName=@MiddleName WHERE Id=@UserId";
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = user.Id;
                command.Parameters.Add("@Login", SqlDbType.VarChar).Value = user.Login;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = user.Password;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = user.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = user.LastName;
                command.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = user.MiddleName;

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

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT * FROM [User]", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new UserModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            MiddleName = reader["MiddleName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Login = reader["Login"].ToString(),
                            AccessLevel = Convert.ToInt32(reader["AccessLevel"])
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public UserModel GetUserByLogin(string login)
        {
            UserModel user = null;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [User] WHERE Login=@Login";
                command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            MiddleName = reader["MiddleName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Login = reader["Login"].ToString(),
                            AccessLevel = Convert.ToInt32(reader["AccessLevel"])
                        };
                    }
                }
            }
            return user;
        }
    }
}

