#region LICENSE
// Copyright (c) 2019, Contrast Security, Inc.
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
// 
// Redistributions of source code must retain the above copyright notice, this list of
// conditions and the following disclaimer.
// 
// Redistributions in binary form must reproduce the above copyright notice, this list of
// conditions and the following disclaimer in the documentation and/or other materials
// provided with the distribution.
// 
// Neither the name of the Contrast Security, Inc. nor the names of its contributors may
// be used to endorse or promote products derived from this software without specific
// prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
// THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT
// OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF
// THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Contrast.Model
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
