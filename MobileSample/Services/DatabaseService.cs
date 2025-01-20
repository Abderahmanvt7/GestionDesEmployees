using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MobileSample.Models;
using Xamarin.Forms;

namespace MobileSample.Services
{
    public class DatabaseService
    {
        public DatabaseContext GetDatabaseContext()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            return databaseContext;
        }

        public IQueryable<Department> GetDepartments(DatabaseContext databaseContext)
        {
            return databaseContext.Departments;
        }

        public IQueryable<Employee> GetEmployees(DatabaseContext databaseContext)
        {
            return databaseContext.Employees;
        }
    }
}
