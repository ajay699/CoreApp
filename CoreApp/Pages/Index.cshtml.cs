using CoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Employee> Employees;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();

            stringBuilder.DataSource = "mysqlserver007.database.windows.net";
            stringBuilder.InitialCatalog = "mydb";
            stringBuilder.UserID = "ap";
            stringBuilder.Password = "Polaris@2019";

            SqlConnection sqlConnection = new SqlConnection(stringBuilder.ConnectionString);

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
            Employees = employees;
        }

    }
}
