using Domain.Enums;

namespace Domain.Dtos.Dns;

public class DnsRecordUpdateDto
{
    public required int Id { get; set; }
    public required DnsRecordType DnsRecordType { get; set; }
    public int? TTL { get; set; }
    public string Name { get; set; }
    public int? Proirity { get; set; }
    public int? Weight { get; set; }
    public int? Port { get; set; }
    public required string Target { get; set; }
    public string? Comment { get; set; }
}
