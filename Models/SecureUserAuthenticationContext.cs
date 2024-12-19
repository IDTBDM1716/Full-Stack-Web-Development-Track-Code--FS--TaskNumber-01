using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace SecureUserAuthenticationSystem.Models
{
    public class SecureUserAuthenticationContext
    {
        // Connection string directly from Web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["SecureUserAuthenticationDb"].ConnectionString;

        // Method to fetch users from the database
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            // SQL query to fetch users
            string query = "SELECT Id, FullName, Email FROM Users";

            // Using SqlConnection and SqlCommand to interact with the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add each user to the list
                        users.Add(new User
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            return users;
        }

        // Method to add a user to the database
        public void AddUser(User user)
        {
            string query = "INSERT INTO Users (FullName, Email) VALUES (@FullName, @Email)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FullName", user.FullName);
                command.Parameters.AddWithValue("@Email", user.Email);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
