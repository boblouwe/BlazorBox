namespace BlazorBox;

public interface IOutgoingHttpClient
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage);
}
