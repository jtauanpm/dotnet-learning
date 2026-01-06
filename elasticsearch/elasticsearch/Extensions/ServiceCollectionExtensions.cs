using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Security;
using Elastic.Transport;
using elasticsearch.options;
using Microsoft.Extensions.Options;

namespace elasticsearch.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddElasticSearch(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ElasticsearchOptions>(
        builder.Configuration.GetSection(ElasticsearchOptions.SectionName));

        builder.Services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<ElasticsearchOptions>>().Value;

            var settings = new ElasticsearchClientSettings(new Uri(options.Url))
                .Authentication(new Elastic.Transport.ApiKey(options.ApiKey))
                .CertificateFingerprint(options.Fingerprint);

            return new ElasticsearchClient(settings);
        });

        return builder.Services;
    }
}
