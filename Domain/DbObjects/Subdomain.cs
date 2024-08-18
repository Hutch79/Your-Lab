namespace Domain.DbObjects;

public class Subdomain
{
    public int Id { get; set; }
    public required RootDomain RootDomain { get; set; }
    public required User User { get; set; }
    public required string SubdomainName { get; set; }
}