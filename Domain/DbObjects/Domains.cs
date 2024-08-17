namespace Domain.DbTypes;

public class Domains
{
    public int Id { get; set; }
    public required string Domain { get; set; }
    public required bool Active { get; set; }
}