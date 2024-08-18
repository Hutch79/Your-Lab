using Domain.Dtos.Dns;
using Infrastructure.Interfaces;

namespace Infrastructure.DnsServices;

public class CloudflareDnsService : IDnsProvider
{
    private readonly string _apiToken;

    public CloudflareDnsService(string apiToken)
    {
        _apiToken = apiToken;
    }

    public DnsRecordDto GetDnsRecord()
    {
        throw new NotImplementedException();
    }

    public DnsRecordDto[] GetDnsRecords()
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetDnsRecords(string zoneId)
    {
        var url = $"https://api.cloudflare.com/client/v4/zones/{zoneId}/dns_records";
        using var client = new HttpClient();

// Only set the Authorization header here
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiToken}");

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode(); // This line throws an exception if the request fails

        var responseBody = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(responseBody);


        return responseBody;
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