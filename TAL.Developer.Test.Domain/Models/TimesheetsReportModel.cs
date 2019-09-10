using System;

namespace TAL.Developer.Test.Domain.Models
{
    public class TimesheetsReportModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeUsername { get; set; }
        public string TimezoneName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Rate { get; set; }
        public string Status { get; set; }
        public decimal? HoursWorked { get; set; }
        public decimal? Cost { get; set; }
    }
}
