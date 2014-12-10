/*
 * Copyright (c) 2014, Contrast Security, Inc.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are
 * permitted provided that the following conditions are met:
 *
 * Redistributions of source code must retain the above copyright notice, this list of
 * conditions and the following disclaimer.
 *
 * Redistributions in binary form must reproduce the above copyright notice, this list of
 * conditions and the following disclaimer in the documentation and/or other materials
 * provided with the distribution.
 *
 * Neither the name of the Contrast Security, Inc. nor the names of its contributors may
 * be used to endorse or promote products derived from this software without specific
 * prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
 * THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT
 * OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF
 * THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using contrast_rest_dotnet.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    /// <summary>
    /// A vulnerability identified by Contrast.
    /// </summary>
    [DataContract]
    public class Trace
    {
        /// <summary>
        /// Gets the ID of the trace.
        /// </summary>
        [DataMember(Name="traceId")]
        public string TraceId { get; set; }

        /// <summary>
        /// Gets the unique ID for the trace.
        /// </summary>
        [DataMember(Name = "uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets the total traces received.
        /// </summary>
        [DataMember(Name = "totalTracesRecieved")]
        public int TotalTracesReceived { get; set; }

        [DataMember]
        private long lastTimeSeen { get; set; }

        /// <summary>
        /// Gets the last time the trace was reported.
        /// </summary>
        public DateTime LastTimeSeen { get; set; }

        [DataMember]
        private long firstTimeSeen { get; set; }

        /// <summary>
        /// Gets the first time the trace was reported.
        /// </summary>
        public DateTime FirstTimeSeen { get; set; }

        /// <summary>
        /// Gets the status of this trace, like "Reported", "Verified", "Suspicious", etc.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets the language for the trace.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets the title of the trace.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets the sub-title of the trace.
        /// </summary>
        [DataMember(Name = "subTitle")]
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets whether the trace was reported to bug tracking.
        /// </summary>
        [DataMember(Name = "reportedToBugTracker")]
        public bool ReportedToBugTracker { get; set; }

        /// <summary>
        /// Gets the rule-name for the vulnerability.
        /// </summary>
        [DataMember(Name = "ruleName")]
        public string RuleName { get; set; }

        /// <summary>
        /// Gets the severity of this trace.
        /// </summary>
        [DataMember(Name = "severity")]
        public string Severity { get; set; }

        /// <summary>
        /// Gets the likelihood of this trace.
        /// </summary>
        [DataMember(Name = "likelihood")]
        public string Likelihood { get; set; }

        /// <summary>
        /// Gets the impact of this trace.
        /// </summary>
        [DataMember(Name = "impact")]
        public string Impact { get; set; }

        /// <summary>
        /// Gets the confidence rating for this trace.
        /// </summary>
        [DataMember(Name = "confidence")]
        public string Confidence { get; set; }

        /// <summary>
        /// Gets the HTTP request that caused this trace to occur.
        /// </summary>
        [DataMember(Name = "request")]
        public Request Request { get; set; }

        /// <summary>
        /// Gets the events that make up the vulnerability. Some traces
        /// will only have an evidence field and no events.
        /// </summary>
        [DataMember(Name = "events")]
        public List<TraceEvent> Events { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST endpoint URLs for this trace.
        /// </summary>
        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            LastTimeSeen = MicrosecondDateTimeConverter.ConvertFromEpochTime(lastTimeSeen);
            FirstTimeSeen = MicrosecondDateTimeConverter.ConvertFromEpochTime(firstTimeSeen);
        }
    }
}
