using CoreApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Employee> GetEmployees()
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("SQLConnection"));

            SqlCommand command = new SqlCommand("select * from Employee");
            command.Connection = sqlConnection;
            command.CommandType = System.Data.CommandType.Text;

            sqlConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            List<Employee> employees = new List<Employee>();

            while (dataReader.Read())
            {
                employees.Add(new Employee
                {
                    Id = dataReader.GetInt32(0),
                    FirstName = dataReader.GetString(1),
                    LastName = dataReader.GetString(2),
                    Salary = dataReader.GetInt32(3)
                }); ;
            }

            sqlConnection.Close();
            return employees;
        }
    }
}
