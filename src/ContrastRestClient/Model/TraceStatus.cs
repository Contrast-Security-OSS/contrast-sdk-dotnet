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

using System.Collections.Generic;

using Newtonsoft.Json;

namespace Contrast.Model
{
    /// <summary>
    /// Class that contains allowed trace status.
    /// </summary>
    public static class TraceStatus
    {
        public const string CONFIRMED_STATUS = "Confirmed";
        public const string SUSPICIOUS_STATUS = "Suspicious";
        public const string NOT_A_PROBLEM_STATUS = "Not a Problem";
        public const string REMEDIATED_STATUS = "Remediated";
        public const string REPORTED_STATUS = "Reported";
        public const string FIXED_STATUS = "Fixed";
    }

    [JsonObject]
    public class TraceMarkStatusRequest
    {
        /// <summary>
        /// Array of traces
        /// </summary>
        [JsonProperty(PropertyName = "traces")]
        public List<string> Traces { get; set; }
        /// <summary>
        /// New status.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Subs status
        /// </summary>
        [JsonProperty(PropertyName = "substatus")]
        public string Substatus { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        [JsonProperty(PropertyName = "note")]
        public string Note { get; set; }

        /// <summary>
        /// Comment preference.
        /// </summary>
        [JsonProperty(PropertyName = "comment_preference")]
        public bool CommentPreference { get; set; }
    }
}
