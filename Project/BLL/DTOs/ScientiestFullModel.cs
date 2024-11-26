namespace BLL.DTOs
{
    public class ScientiestFullModel
    {
        public required string Name { get; set; }

        public required string ImageUrl { get; set; }

        public required string Description { get; set; }

        public required string BirthDate { get; set; }

        public required string BirthPlaceUrl { get; set; }

        public required string BirthPlace { get; set; }

        public required string Occupation { get; set; }
    }
}
