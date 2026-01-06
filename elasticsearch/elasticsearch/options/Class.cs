using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticsearch.options;

public class ElasticsearchOptions
{
    public const string SectionName = "Elasticsearch";

    public string Url { get; set; } = string.Empty;
    public string Fingerprint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}
