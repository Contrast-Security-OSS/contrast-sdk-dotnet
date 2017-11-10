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

using contrast_rest_dotnet.Serialization;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    /// <summary>
    /// A vulnerability identified by Contrast.
    /// </summary>
    [JsonObject]
    public class Trace
    {
        /// <summary>
        /// Gets the unique ID for the trace.
        /// </summary>
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// List of application version tags
        /// </summary>
        [JsonProperty(PropertyName = "app_version_tags")]
        public List<string> AppVersionTags { get; set; }

        /// <summary>
        /// This trace Application
        /// </summary>
        [JsonProperty(PropertyName = "application")]
        public ContrastApplication Application { get; set; }

        /// <summary>
        /// Period of Remediation Policy that Auto-Remediated this trace
        /// </summary>
        [JsonProperty(PropertyName = "auto_remediated_expiration_period")]
        public long? AutoRemediatedExpirationPeriod { get; set; }

        [JsonProperty(PropertyName = "card")]
        public Card Card { get; set; }

        /// <summary>
        /// Gets this trace category
        /// </summary>
        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets this trace closed time
        /// </summary>
        [JsonProperty(PropertyName = "closed_time")]
        public long? ClosedTime { get; set; }

        /// <summary>
        /// Get this trace Confidence
        /// </summary>
        [JsonProperty(PropertyName = "confidence")]
        public string Confidence { get; set; }

        /// <summary>
        /// Default Severity. Allowed values: NOTE, LOW, MEDIUM, HIGH, CRITICAL.
        /// </summary>
        [JsonProperty(PropertyName = "default_severity")]
        public string DefaultSeverity { get; set; }

        /// <summary>
        /// List of events
        /// </summary>
        [JsonProperty(PropertyName = "events")]
        public List<TraceEvent> Events { get; set; }

        /// <summary>
        /// Gets this trace Evidence
        /// </summary>
        [JsonProperty(PropertyName = "evidence")]
        public string Evidence { get; set; }

        [JsonProperty(PropertyName = "first_time_seen")]
        private long FirstTimeSeenRawValue { get; set; }

        /// <summary>
        /// Time first seen
        /// </summary>
        public DateTime? FirstTimeSeen { get; set; }

        /// <summary>
        /// Gets whether this trace has a parent app or not.
        /// </summary>
        [JsonProperty(PropertyName = "hasParentApp")]
        public bool HasParentApp { get; set; }

        /// <summary>
        /// Gets this trace impact.
        /// </summary>
        [JsonProperty(PropertyName = "impact")]
        public string Impact { get; set; }

        /// <summary>
        /// Gets the language for the trace.
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "last_time_seen")]
        private long LastTimeSeenRawValue { get; set; }

        /// <summary>
        /// Gets the last time the trace was reported.
        /// </summary>
        public DateTime? LastTimeSeen { get; set; }

        /// <summary>
        /// Gets this trace license. Allowed values: ReadOnly, Unlincensed, Licensed.
        /// </summary>
        [JsonProperty(PropertyName = "license")]
        public string License { get; set; }

        /// <summary>
        /// Gets likelihood for this trace
        /// </summary>
        [JsonProperty(PropertyName = "likelihood")]
        public string Likelihood { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST endpoint URLs for this trace.
        /// </summary>
        [Obsolete("Use the field from TraceFilterResponse object.")]
        [JsonProperty(PropertyName = "links")]
        public List<Link> Links { get; set; }

        /// <summary>
        /// List of notes
        /// </summary>
        [JsonProperty(PropertyName = "notes")]
        public List<TraceNote> Notes { get; set; }

        /// <summary>
        /// Organization Name
        /// </summary>
        [JsonProperty(PropertyName = "organization_name")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Parent Application ID
        /// </summary>
        [JsonProperty(PropertyName = "parent_application")]
        public ContrastApplication ParentApplication { get; set; }

        /// <summary>
        /// Is reported to bug tacker
        /// </summary>
        [JsonProperty(PropertyName = "reported_to_bug_tracker")]
        public bool ReportedToBugTracker { get; set; }

        [JsonProperty(PropertyName = "reported_to_bug_tracker_time")]
        public long? ReportedToBugTrackerTimeRawValue { get; set; }

        /// <summary>
        /// Time reported to bug tracker
        /// </summary>
        public DateTime? ReportedToBugTrackerTime { get; set; }

        /// <summary>
        /// Gets the HTTP request that caused this trace to occur.
        /// </summary>
        [JsonProperty(PropertyName = "request")]
        public Request Request { get; set; }

        /// <summary>
        /// Gets the rule-name for the vulnerability.
        /// </summary>
        [JsonProperty(PropertyName = "rule_name")]
        public string RuleName { get; set; }

        [JsonProperty(PropertyName = "servers")]
        public List<Server> Servers { get; set; }

        /// <summary>
        /// Gets the severity of this trace.
        /// </summary>
        [JsonProperty(PropertyName = "severity")]
        public string Severity { get; set; }

        /// <summary>
        /// Gets the status of this trace, like "Reported", "Verified", "Suspicious", etc.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets the sub status of this trace
        /// </summary>
        [JsonProperty(PropertyName = "sub_status")]
        public string SubStatus { get; set; }

        /// <summary>
        /// Gets the sub-title of the trace.
        /// </summary>
        [JsonProperty(PropertyName = "sub_title")]
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets the title of the trace.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets the total traces received.
        /// </summary>
        [JsonProperty(PropertyName = "total_traces_received")]
        public int? TotalTracesReceived { get; set; }

        [JsonProperty(PropertyName = "visible")]
        public bool Visible { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            LastTimeSeen = DateTimeConverter.ConvertToDateTime(LastTimeSeenRawValue);
            FirstTimeSeen = DateTimeConverter.ConvertToDateTime(FirstTimeSeenRawValue);
            ReportedToBugTrackerTime = DateTimeConverter.ConvertToDateTime(ReportedToBugTrackerTimeRawValue);
        }
    }

    [JsonObject]
    public class TraceNote
    {
        [JsonProperty(PropertyName = "creation")]
        private long? CreationRawValue { get; set; }

        /// <summary>
        /// Creation time.
        /// </summary>
        public DateTime? Creation { get; set; }

        /// <summary>
        /// Creator name.
        /// </summary>
        [JsonProperty(PropertyName = "creator")]
        public string Creator { get; set; }

        /// <summary>
        /// Creator UUID.
        /// </summary>
        [JsonProperty(PropertyName = "creator_uuid")]
        public string CreatorUUID { get; set; }

        /// <summary>
        /// If this note is deletable.
        /// </summary>
        [JsonProperty(PropertyName = "deletable")]
        public bool Deletable { get; set; }

        /// <summary>
        /// Note id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "last_modification")]
        public long? LastModificationRawValue { get; set; }

        /// <summary>
        /// Last modification time.
        /// </summary>
        public DateTime? LastModification { get; set; }

        /// <summary>
        /// Last updater name.
        /// </summary>
        [JsonProperty(PropertyName = "last_updater")]
        public string LastUpdater { get; set; }

        /// <summary>
        /// Last updater UUID.
        /// </summary>
        [JsonProperty(PropertyName = "last_updater_uuid")]
        public string LastUpdaterUUID { get; set; }

        /// <summary>
        /// Note contents.
        /// </summary>
        [JsonProperty(PropertyName = "note")]
        public string Note { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Creation = DateTimeConverter.ConvertToDateTime(CreationRawValue);
            LastModification = DateTimeConverter.ConvertToDateTime(LastModificationRawValue);
        }
    }

    [JsonObject]
    public class TraceFilterResponse
    {
        /// <summary>
        /// Count
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public long Count { get; set; }

        /// <summary>
        /// Number of Traces from a licensed app
        /// </summary>
        [JsonProperty(PropertyName = "licensedCount")]
        public long LicensedCount { get; set; }

        /// <summary>
        /// List of messages
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// List of traces
        /// </summary>
        [JsonProperty(PropertyName = "traces")]
        public List<Trace> Traces { get; set; }
    }

    [JsonObject]
    public class TracesSearchResponse
    {
        /// <summary>
        /// List of messages
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// List of traces
        /// </summary>
        [JsonProperty(PropertyName = "traces")]
        public List<Trace> Traces { get; set; }
    }
}
 