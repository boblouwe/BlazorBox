namespace BlazorBox;

public class OutgoingHttpCachingHandler : DelegatingHandler
{
    private readonly CacheStorageAccessor _cacheStorageAccessor;

    public OutgoingHttpCachingHandler(CacheStorageAccessor cacheStorageAccessor)
    {
        _cacheStorageAccessor = cacheStorageAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;
        try
        {
            response = await base.SendAsync(request, cancellationToken);
            //await _cacheStorageAccessor.RemoveAsync(request);
            await _cacheStorageAccessor.StoreAsync(request, response);
        }
        catch (Exception)
        {
            var responseBody = await _cacheStorageAccessor.GetAsync(request);
            if (string.IsNullOrEmpty(responseBody))
                throw;

            response = new HttpResponseMessage(System.Net.HttpStatusCode.NotModified) { Content = new StringContent(responseBody) };
        }

        return response;
    }
}
