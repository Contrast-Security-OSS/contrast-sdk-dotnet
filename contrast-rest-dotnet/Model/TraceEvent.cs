/*
 * Copyright (c) 2015, Contrast Security, Inc.
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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace contrast_rest_dotnet.Model
{
    /// <summary>
    /// A collection of TraceEvents make up a vulnerability, or, "trace". They
    /// represent a method invocation that Contrast monitored.
    /// </summary>
    [JsonObject]
    public class TraceEvent
    {
        /// <summary>
        /// Gets the event ID.
        /// </summary>
        [JsonProperty(PropertyName="eventId")]
        public string EventId { get; set; }

        /// <summary>
        /// Gets the event type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string EventType { get; set; }

        /// <summary>
        /// Gets the code context for the event.
        /// </summary>
        [JsonProperty(PropertyName = "codeContext")]
        public object CodeContext { get; set; }
    }

    [JsonObject]
    public class TraceEventSummary
    {
        /// <summary>
        /// Raw code creation.
        /// </summary>
        [JsonProperty(PropertyName = "codeView")]
        public CodeView CodeView { get; set; }

        /// <summary>
        /// List of collapsed events
        /// </summary>
        [JsonProperty(PropertyName = "collapsedEvents")]
        public List<TraceEventSummary> CollapsedEvents { get; set; }

        /// <summary>
        /// Data snapshot
        /// </summary>
        [JsonProperty(PropertyName = "dataView")]
        public CodeView DataView { get; set; }

        /// <summary>
        /// Event description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Number of duplicated events collapsed.
        /// </summary>
        [JsonProperty(PropertyName = "dupes")]
        public int? Dupes { get; set; }

        /// <summary>
        /// Event extra details.
        /// </summary>
        [JsonProperty(PropertyName = "extraDetails")]
        public string ExtraDetails { get; set; }

        /// <summary>
        /// Event id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// If this event is important.
        /// </summary>
        [JsonProperty(PropertyName = "important")]
        public bool Important { get; set; }

        /// <summary>
        /// Probable start location
        /// </summary>
        [JsonProperty(PropertyName = "probableStartLocationView")]
        public CodeView ProbableStartLocationView { get; set; }

        /// <summary>
        /// Event type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

    [JsonObject]
    public class TraceEventSummaryResponse
    {
        /// <summary>
        /// List of events
        /// </summary>
        [JsonProperty(PropertyName = "events")]
        public List<TraceEventSummary> Events { get; set; }

        /// <summary>
        /// Evidence
        /// </summary>
        [JsonProperty(PropertyName = "evidence")]
        public string Evidence { get; set; }

        /// <summary>
        /// List of messages
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// If events are shown.
        /// </summary>
        [JsonProperty(PropertyName = "showEvents")]
        public bool ShowEvents { get; set; }

        /// <summary>
        /// If evidence is shown.
        /// </summary>
        [JsonProperty(PropertyName = "showEvidence")]
        public bool ShowEvidence { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }
}
