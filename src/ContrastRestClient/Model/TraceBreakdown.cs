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

using Newtonsoft.Json;

namespace Contrast.Model
{
    [JsonObject]
    public class TraceBreakdown
    {
        /// <summary>
        /// Number of vulnerabilities with status Confirmed
        /// </summary>
        [JsonProperty(PropertyName = "confirmed")]
        public long? ConfirmedVulnerabilities { get; set; }

        /// <summary>
        /// Number of critical vulnerabilities
        /// </summary>
        [JsonProperty(PropertyName = "criticals")]
        public long? CriticalVulnerabilities { get; set; }

        /// <summary>
        /// Number of vulnerabilities with status Fixed
        /// </summary>
        [JsonProperty(PropertyName = "fixed")]
        public long? FixedVulnerabilities { get; set; }

        /// <summary>
        /// Number of high vulnerabilities
        /// </summary>
        [JsonProperty(PropertyName = "highs")]
        public long? HighVulnerabilities { get; set; }

        /// <summary>
        /// Number of low vulnerabilities
        /// </summary>
        [JsonProperty(PropertyName = "lows")]
        public long? LowVulnerabilities { get; set; }

        /// <summary>
        /// Number of medium vulnerabilities
        /// </summary>
        [JsonProperty(PropertyName = "meds")]
        public long? MediumVulnerabilities { get; set; }

        /// <summary>
        /// Number of vulnerabilities with status Not a problem
        /// </summary>
        [JsonProperty(PropertyName = "notProblem")]
        public long? NoProblemVulnerabilities { get; set; }

        /// <summary>
        /// Number of notes
        /// </summary>
        [JsonProperty(PropertyName = "notes")]
        public long? Notes { get; set; }

        /// <summary>
        /// Number of vulnerabilities with status Remediated
        /// </summary>
        [JsonProperty(PropertyName = "remediated")]
        public long? Remediated { get; set; }

        /// <summary>
        /// Number of vulnerabilities with status Reported
        /// </summary>
        [JsonProperty(PropertyName = "reported")]
        public long? Reported { get; set; }

        /// <summary>
        /// Number of vulnerabilities marked safe
        /// </summary>
        [JsonProperty(PropertyName = "safes")]
        public long? SafeVulnerabilities { get; set; }

        /// <summary>
        /// Number of vulnerabilities with status Suspicious
        /// </summary>
        [JsonProperty(PropertyName = "suspicious")]
        public long? Suspicious { get; set; }

        /// <summary>
        /// Number of vulnerabilities
        /// </summary>
        [JsonProperty(PropertyName = "traces")]
        public long? Traces { get; set; }

        /// <summary>
        /// Number of triaged vulnerabilities
        /// </summary>
        [JsonProperty(PropertyName = "triaged")]
        public long? Triaged { get; set; }
    }
}
