using System;

namespace TAL.Developer.Test.Domain.Models
{
    public class TimesheetsModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int TimezoneId { get; set; }
        public string TimezoneName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Rate { get; set; }
    }
}
