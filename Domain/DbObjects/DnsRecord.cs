using Domain.DbObjects;
using Domain.Enums;

namespace Domain.DbTypes;

public class DnsRecord
{
    public int Id { get; set; }
    public required DnsRecordType DnsRecordType { get; set; }
    public int? TTL { get; set; }
    public required string Name { get; set; }
    public int? Priority { get; set; }
    public int? Weight { get; set; }
    public int? Port { get; set; }
    public required string Target { get; set; }
    public string? Comment { get; set; }
    public required Subdomain Subdomain { get; set; }
}