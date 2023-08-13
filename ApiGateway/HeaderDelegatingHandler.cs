using System.Net.Http.Headers;

namespace ApiGateway
{
    public class HeaderDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<string> headerValues;
            //if (request.Headers.TryGetValues("Authorization", out headerValues))
            //{
            //    string accessToken = headerValues.First();

            //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //    request.Headers.Remove("AccessToken");
            //}

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
