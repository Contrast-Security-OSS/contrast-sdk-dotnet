using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace contrast_rest_dotnet.Model
{
    [JsonObject]
    public class TraceRecommendationResponse
    {
        /// <summary>
        /// Indicates whether API response was successful or not.
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// List of messages.
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<String> Messages { get; set; }

        /// <summary>
        /// Recommendation.
        /// </summary>
        [JsonProperty(PropertyName = "recommendation")]
        public Snippet Recommendation { get; set; }

        /// <summary>
        /// OWASP.
        /// </summary>
        [JsonProperty(PropertyName = "owasp")]
        public String Owasp { get; set; }

        /// <summary>
        /// CWE.
        /// </summary>
        [JsonProperty(PropertyName = "cwe")]
        public String Cwe { get; set; }

        /// <summary>
        /// Custom recommendation.
        /// </summary>
        [JsonProperty(PropertyName = "custom_recommendation")]
        public Snippet CustomRecommendation { get; set; }

        /// <summary>
        /// Rule references.
        /// </summary>
        [JsonProperty(PropertyName = "rule_references")]
        public Snippet RuleReferences { get; set; }

        /// <summary>
        /// Custom rule references.
        /// </summary>
        [JsonProperty(PropertyName = "custom_rule_references")]
        public Snippet CustomRuleReferences { get; set; }
    }
}
