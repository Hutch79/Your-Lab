using Domain.Enums;

namespace Domain.DbObjects;

public class RootDomain
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required bool Active { get; set; }
    public required DnsProvider DnsProvider { get; set; }
    public required string DnsProviderId { get; set; }
}