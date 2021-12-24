using envolvice.taskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace envolvice.taskAPI.Employee
{
    public class EmployeeService : IEmployeeService
    {
        public ICollection<EmployeeModel> GetEmployees(IEnumerable<string> lines)
        {
            var employees = new List<EmployeeModel>();

            foreach (var line in lines)
            {
                var values = line.Split(',').ToList();
                if (values == null || values.Count == 0 || values.Count != 4)
                    throw new InvalidOperationException("Invalid format");

                var employee = new EmployeeModel
                {
                    Id = int.Parse(values[0]),
                    ProjectId = int.Parse(values[1]),
                    StartDate = DateTime.Parse(values[2]),
                    EndDate = values[3] == "NULL" ? DateTime.Now : DateTime.Parse(values[3]),
                };
                employee.WorkingDays = (employee.EndDate - employee.StartDate).Days;
                employees.Add(employee);
            }

            return employees;
        }

        public ICollection<EmployeeModel> FindParisWithLongestWorkingDaysTogether(IEnumerable<EmployeeModel> employees)
        {
            if (employees.Count() == 0)
                return new List<EmployeeModel>();

            var projects = employees.GroupBy(e => e.ProjectId)
                                 .Where(e => e.Count() > 1)
                                 .Select(p =>
                                 {
                                     var longestWoringDays = p.OrderByDescending(e => e.WorkingDays).Take(2)
                                                                                         .Select(x => x).ToList();
                                     return new
                                     {
                                         ProjectId = p.Key,
                                         Employees = longestWoringDays,
                                         WorkingTogether = longestWoringDays.Min(z => z.WorkingDays)
                                     };
                                 });

            var longestWorkingTogetherEmployees = projects.OrderByDescending(p => p.WorkingTogether).FirstOrDefault();
            if (longestWorkingTogetherEmployees == null)
                return new List<EmployeeModel>();

            return longestWorkingTogetherEmployees.Employees;
        }
    }
}
