using Domain;
using Domain.Dtos.Dns;
using Domain.Enums;

namespace Infrastructure.Interfaces;

public interface IDnsProvider
{
    DnsRecordDto GetDnsRecord();
    DnsRecordDto[] GetDnsRecords();
    Task CreateDnsRecord(DnsRecordDto dnsRecordDto);
    void UpdateDnsRecord(DnsRecordUpdateDto dnsRecordDto);
    void DeleteDnsRecord(int dnsRecordDto);
    void DeleteDnsRecords(int[] dnsRecordDto);
}