namespace BlazorBox;

public class OutgoingHttpClient : IOutgoingHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    public const string HttpClientName = "OutgoingHttpClient";

    public OutgoingHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);

        return httpClient.SendAsync(httpRequestMessage);
    }
}