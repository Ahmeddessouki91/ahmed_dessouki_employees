using envolvice.taskAPI.Employee;
using envolvice.taskAPI.FileReader;
using envolvice.taskAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace envolvice.taskAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {


        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> GetLongestWorkPairOfEmployees(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            var lines = await new TextFileReader().ReadAsync(file.OpenReadStream());

            var employees = _employeeService.GetEmployees(lines);

            return Ok(_employeeService.FindParisWithLongestWorkingDaysTogether(employees));
        }
    }
}
