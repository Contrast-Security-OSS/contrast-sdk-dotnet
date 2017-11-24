using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace contrast_rest_dotnet.Model
{
    public enum TraceFilterType
    {
        modules,
        workflow,
        servers,
        time,
        url,
        vulntype,
        status,
        severity,
        securityStandard
    }

    public class TraceFilterItem
    {
        /// <summary>
        /// Count
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public long Count { get; set; }

        /// <summary>
        /// Key code
        /// </summary>
        [JsonProperty(PropertyName = "keycode")]
        public String Keycode { get; set; }

        /// <summary>
        /// Label
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Add option to a new group
        /// </summary>
        [JsonProperty(PropertyName = "new_group")]
        public bool NewGroup { get; set; }

        /// <summary>
        /// Tooltip
        /// </summary>
        [JsonProperty(PropertyName = "tooltip")]
        public string Tooltip { get; set; }
    }

    [JsonObject]
    public class TraceFilterCatalogDetailsResponse
    {
        /// <summary>
        /// List of available filters for context.
        /// </summary>
        [JsonProperty(PropertyName = "filters")]
        public List<TraceFilterItem> Filters { get; set; }

        /// <summary>
        /// List of messages.
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not.
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }
}
