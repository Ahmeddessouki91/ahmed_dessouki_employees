using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace envolvice.taskAPI.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WorkingDays { get; set; }
    }
}
