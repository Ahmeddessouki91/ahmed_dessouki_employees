using envolvice.taskAPI.Models;
using System.Collections.Generic;

namespace envolvice.taskAPI.Employee
{
    public interface IEmployeeService
    {
        ICollection<EmployeeModel> FindParisWithLongestWorkingDaysTogether(IEnumerable<EmployeeModel> employees);
        ICollection<EmployeeModel> GetEmployees(IEnumerable<string> lines);
    }
}