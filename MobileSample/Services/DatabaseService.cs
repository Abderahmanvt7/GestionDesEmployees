using System;
using System.Linq;
using MobileSample.Models;
using Xamarin.Forms;

namespace MobileSample.Services
{
    public class DatabaseService
    {
        // Retrieve the database context
        public DatabaseContext GetDatabaseContext()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            return databaseContext;
        }

        // Departments-related methods
        public IQueryable<Department> GetDepartments(DatabaseContext databaseContext)
        {
            return databaseContext.Departments;
        }

        // Employees-related methods
        public IQueryable<Employee> GetEmployees(DatabaseContext databaseContext)
        {
            return databaseContext.Employees;
        }

        // Users-related methods (added for authentication)

        // Retrieve all users
        public IQueryable<User> GetUsers(DatabaseContext databaseContext)
        {
            return databaseContext.Users;
        }

        // Retrieve a user by username
        public User GetUserByUsername(DatabaseContext databaseContext, string username)
        {
            return databaseContext.Users.FirstOrDefault(u => u.Username == username);
        }

        // Add a new user
        public void AddUser(DatabaseContext databaseContext, User user)
        {
            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();
        }

        // Validate user credentials
        public bool ValidateUser(DatabaseContext databaseContext, string username, string password)
        {
            var user = databaseContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user != null;
        }
    }
}
