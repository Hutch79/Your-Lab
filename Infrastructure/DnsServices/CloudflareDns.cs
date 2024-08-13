using Domain;
using Domain.Dtos.Dns;
using Infrastructure.Interfaces;

namespace Infrastructure.DnsServices;

public class CloudflareDns : IDnsProvider
{
    public DnsRecordDto GetDnsRecord()
    {
        throw new NotImplementedException();
    }

    public DnsRecordDto[] GetDnsRecords()
    {
        throw new NotImplementedException();
    }

    public Task CreateDnsRecord(DnsRecordDto dnsRecordDto)
    {
        throw new NotImplementedException();
    }

    public void UpdateDnsRecord(DnsRecordUpdateDto dnsRecordDto)
    {
        throw new NotImplementedException();
    }

    public void DeleteDnsRecord(int dnsRecordDto)
    {
        throw new NotImplementedException();
    }

    public void DeleteDnsRecords(int[] dnsRecordDto)
    {
        throw new NotImplementedException();
    }
}