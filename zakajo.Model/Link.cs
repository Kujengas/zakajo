using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace zakajo.Model
{
    public partial class Link
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("LinkType")]
        public string LinkType { get; set; }
        
        [JsonProperty("ExternalLink")]
        public string ExternalLink { get; set; }
        
        [JsonProperty("InternalLink")]
        public string InternalLink { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }

    public partial class Link
    {
        public static List<Link> FromJson(string json) => JsonConvert.DeserializeObject<List<Link>>(json, Converter.Settings);
    }
}
