namespace TAL.Developer.Test.Domain.Models
{
    public class OccupationsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OccupationRatingsModel OccupationRatings { get; set; }
    }
}
