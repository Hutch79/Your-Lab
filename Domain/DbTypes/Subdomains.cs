namespace Domain.DbTypes;

public class Subdomains
{
    public int Id { get; set; }
    public required int DomainId { get; set; }
    public required int UserId { get; set; }
    public required string SubdomainName { get; set; }
    public int[]? DnsRecordId { get; set; }
}
