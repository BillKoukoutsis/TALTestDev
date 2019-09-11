using System;

namespace TAL.Developer.Test.Domain.Models
{
    public class MembersModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public decimal SumInsured { get; set; }
        public OccupationsModel Occupations { get; set; }

    }
}
