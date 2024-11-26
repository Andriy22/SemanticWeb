namespace DAL.Entities;

public class Scientist
{
    public long Id { get; set; }

    public required string Fullname { get; set; }

    public required string ImageUrl { get; set; }

    public required string ResourceUrl { get; set; }

    public long UniqueWikiId { get; set; }
}
