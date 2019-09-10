namespace TAL.Developer.Test.Domain.Models
{
    public class EmployeesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public int? TimezoneId { get; set; }
        public string TimezoneName { get; set; }
        public decimal Rate { get; set; }
    }
}
