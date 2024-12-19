using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using SecureUserAuthenticationSystem.Models;

namespace SecureUserAuthenticationSystem.Controllers
{
    public class HomeController : Controller
    {
        // Connection string to connect to SQL Server
        private string connectionString = "Server=desktop-f3iokie;Database=SecureUserAuthenticationSystemDB;User Id=your_user;Password=your_password;";

        // Index Page - Fetch users from the database
        public ActionResult Index()
        {
            var users = GetUsersFromDatabase();
            return View(users);  // Pass the list of users to the view
        }

        // Get users from the database using ADO.NET
        private List<User> GetUsersFromDatabase()
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, FullName, Email FROM Users";  // SQL query to fetch users (password not shown)
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = (int)reader["Id"],
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString(),
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log SQL exception or show a meaningful message
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any general exceptions
                Console.WriteLine("Exception: " + ex.Message);
            }

            return users;
        }

        // About Page
        public ActionResult About()
        {
            ViewBag.Message = "This project is a Secure User Authentication System developed using ASP.NET MVC.";
            ViewBag.DeveloperName = "Idossa Dhibissa";
            ViewBag.DeveloperEmail = "idodawit@gmail.com";
            ViewBag.DeveloperLocation = "Addis Ababa, Ethiopia";
            return View();
        }

        // Contact Page
        public ActionResult Contact()
        {
            ViewBag.Message = "Get in touch with us!";
            ViewBag.DeveloperName = "Idossa Dhibissa";
            ViewBag.DeveloperEmail = "idodawit@gmail.com";
            ViewBag.DeveloperPhone = "+251917165193";
            ViewBag.DeveloperLocation = "Addis Ababa, Ethiopia";
            return View();
        }

        // Create new user (For demonstration purposes)
        [HttpGet]
        public ActionResult Create()
        {
            return View();  // Return the view where a new user can be created
        }

        // Post method to handle user creation
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                AddUserToDatabase(user);  // Add the new user to the database
                return RedirectToAction("Index");  // Redirect back to the Index action
            }
            return View(user);  // If validation fails, return the user to the create view
        }

        // Add a new user to the database using ADO.NET
        private void AddUserToDatabase(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (FullName, Email, PasswordHash) VALUES (@FullName, @Email, @PasswordHash)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FullName", user.FullName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);  // Store hashed password

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Log SQL exception or show a meaningful message
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any general exceptions
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        // User Login - To handle user login and authenticate
        [HttpGet]
        public ActionResult Login()
        {
            return View();  // Display the login view
        }

        // Post method to handle login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                if (AuthenticateUser(email, password))
                {
                    return RedirectToAction("Index");  // Redirect to home page after successful login
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View();  // If authentication fails, show the login page with error message
        }

        // Authenticate user by checking if email and password match
        private bool AuthenticateUser(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT PasswordHash FROM Users WHERE Email = @Email";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        // Compare hashed password with stored hashed password
                        string storedHashPassword = result.ToString();
                        string hashedPassword = HashPassword(password);
                        return storedHashPassword.Equals(hashedPassword);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log SQL exception or show a meaningful message
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any general exceptions
                Console.WriteLine("Exception: " + ex.Message);
            }

            return false;
        }

        // Hash the password before saving to the database
        private string HashPassword(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var builder = new System.Text.StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
