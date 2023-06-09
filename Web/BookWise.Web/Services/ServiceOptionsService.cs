using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace BookWise.Web.Services
{
    public class ServiceOptionsService : IServiceOptionsService
    {
        private const string AzdsRouteAsHeader = "azds-route-as";
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IOptions<ServiceOptions> serviceOptions;
        private readonly IConfiguration configuration;
        private readonly ILogger<ServiceOptionsService> logger;

        /// <summary>
        /// Initialises a new instance of the <see cref="ServiceOptionsService" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        /// <param name="serviceOptions">The service options.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        public ServiceOptionsService(IHttpContextAccessor httpContextAccessor, IOptions<ServiceOptions> serviceOptions, IConfiguration configuration, ILogger<ServiceOptionsService> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.serviceOptions = serviceOptions;
            this.configuration = configuration;
            this.logger = logger;
        }

        private HttpRequest HttpRequest => this.httpContextAccessor.HttpContext?.Request ?? throw new InvalidOperationException("No HttpContent on HttpContextAccessor");

        /// <inheritdoc/>
        public ServiceOptions GetServiceOptions()
        {
            var options = new ServiceOptions()
            {
                ApiUrl = this.GetUrl(this.serviceOptions.Value.ApiUrl)
            };

            this.logger.LogInformation("refferal host: {host}", this.HttpRequest.Host);

            return options;
        }

        private string GetUrl(string url)
        {
            var routeAs = this.GetDsRouteAs();
            var uri = new Uri(url);
            var resolvedUrl = routeAs != null ? $"{uri.Scheme}://{routeAs}.{uri.Host}{uri.PathAndQuery}" : uri.ToString();
            return resolvedUrl.TrimEnd('/');
        }

        private string? GetDsRouteAs()
        {
            var headers = this.HttpRequest.Headers;
            if (this.configuration["AZDS_BASE_ROUTE"] == null)
            {
                return null;
            }

            var routeAs = headers[AzdsRouteAsHeader];

            return routeAs == this.configuration["AZDS_BASE_ROUTE"] ? null : (string)routeAs;
        }
    }
}
