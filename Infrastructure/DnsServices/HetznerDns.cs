using System.Net.Http.Headers;
using System.Text;
using Domain;
using Domain.Dtos.Dns;
using Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Infrastructure.DnsServices;

public class HetznerDns : IDnsProvider
{
    private readonly string _apiToken;

    public HetznerDns(string apiToken)
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

    public async Task CreateDnsRecord(DnsRecordDto dnsRecordDto)
    {
        var client = new HttpClient();

        // Set headers
        client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        client.DefaultRequestHeaders.Add("Auth-API-Token", "LlGoDUQ39S6akqoav5meAsv5OIpeywhj");

        // Prepare the request body
        var requestBody = new
        {
            value = "1.1.1.1",
            ttl = 86400,
            type = "A",
            name = "www",
            zone_id = "1"
        };

        var jsonPayload = JsonConvert.SerializeObject(requestBody);

        // Send the POST request
        var response = await client.PostAsync("https://dns.hetzner.com/api/v1/records",
            new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

        // Ensure we received a successful response
        response.EnsureSuccessStatusCode();

        // Optionally, read the response content
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
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