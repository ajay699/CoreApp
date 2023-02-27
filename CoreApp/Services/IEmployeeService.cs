using CoreApp.Models;
using System.Collections.Generic;

namespace CoreApp.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
    }
}