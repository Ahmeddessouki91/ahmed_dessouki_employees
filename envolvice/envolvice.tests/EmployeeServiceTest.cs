using envolvice.taskAPI.Employee;
using envolvice.taskAPI.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace envolvice.tests
{
    [TestFixture]
    public class EmployeeServiceTest
    {
        private EmployeeService _employeeService;

        [SetUp]
        public void Setup()
        {
            _employeeService = new EmployeeService();
        }

        [Test]
        public void GetEmployees_WithLinesOfData_ReturnList()
        {
            var lines = new List<string>() {
                "1,101,2014-11-01,2015-05-01",
                "1,103,2013-11-01,2016-05-01",
                "2,101,2013-12-06,2014-10-06",
                "2,103,2014-06-05,2015-05-14",
            };

            var res = _employeeService.GetEmployees(lines);

            Assert.AreEqual(4, res.Count);
        }

        [Test]
        public void GetEmployees_WithEmptyLines_ReturnList()
        {

            var res = _employeeService.GetEmployees(new List<string>());
            Assert.AreEqual(0, res.Count);
        }

        [Test]
        public void GetEmployees_WithNullEndDate_ReturnList()
        {
            var lines = new List<string>() {
                "1,101,2014-11-01,2015-05-01",
                "1,103,2013-11-01,NULL",
                "2,101,2013-12-06,NULL",
                "2,103,2014-06-05,2015-05-14",
            };

            var res = _employeeService.GetEmployees(lines);
            Assert.AreEqual(4, res.Count);
        }

        [Test]
        public void FindParisWithLongestWorkingDaysTogether_WithEmployeeList_ReturnParisOfEmployees()
        {
            var lines = new List<string>() {
                "1,101,2014-11-01,2014-11-02",
                "2,102,2014-11-01,2014-11-05",
                "3,101,2014-11-01,2014-11-03",
                "4,102,2014-11-02,2014-11-04",
            };
            var employees = _employeeService.GetEmployees(lines);

            var res = _employeeService.FindParisWithLongestWorkingDaysTogether(employees);

            Assert.AreEqual(2, res.Count);
            Assert.That(res.Any(e => e.Id == 2));
            Assert.That(res.Any(e => e.Id == 4));
        }

        [Test]
        public void FindParisWithLongestWorkingDaysTogether_WithEmptyList_ReturnsEmptyList()
        {
            var res = _employeeService.FindParisWithLongestWorkingDaysTogether(new List<EmployeeModel>());

            Assert.AreEqual(0, res.Count);
        }

        [Test]
        public void FindParisWithLongestWorkingDaysTogether_WithNoParisData_ReturnsEmptyList()
        {
            var lines = new List<string>() {
                "1,101,2014-11-01,2014-11-02",
                "2,103,2014-11-01,2014-11-05",
                "3,104,2014-11-01,2014-11-03",
                "4,105,2014-11-02,2014-11-04",
            };
            var employees = _employeeService.GetEmployees(lines);

            var res = _employeeService.FindParisWithLongestWorkingDaysTogether(employees);

            Assert.AreEqual(0, res.Count);
        }
    }
}