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
        /// Gets the unique ID for the trace.
        /// </summary>
        [DataMember(Name = "uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// List of application version tags
        /// </summary>
        [DataMember(Name = "app_version_tags")]
        public List<string> AppVersionTags { get; set; }

        /// <summary>
        /// This trace Application
        /// </summary>
        [DataMember(Name = "application")]
        public NgApplicationTraceBase Application { get; set; }

        /// <summary>
        /// Period of Remediation Policy that Auto-Remediated this trace
        /// </summary>
        [DataMember(Name = "auto_remediated_expiration_period")]
        public long? AutoRemediatedExpirationPeriod { get; set; }

        [DataMember(Name = "bugtracker_tickets")]
        public List<string> BugtrackerTickets { get; set; }//TODO Update its list type

        [DataMember(Name = "card")]
        public string Card { get; set; }//TODO Update type

        /// <summary>
        /// Gets this trace category
        /// </summary>
        [DataMember(Name = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets this trace closed time
        /// </summary>
        [DataMember(Name = "closed_time")]
        public long? ClosedTime { get; set; }

        /// <summary>
        /// Get this trace Confidence
        /// </summary>
        [DataMember(Name = "confidence")]
        public string Confidence { get; set; }

        /// <summary>
        /// Default Severity. Allowed values: NOTE, LOW, MEDIUM, HIGH, CRITICAL.
        /// </summary>
        [DataMember(Name = "default_severity")]
        public string DefaultSeverity { get; set; }

        /// <summary>
        /// List of events
        /// </summary>
        [DataMember(Name = "events")]
        public List<TraceEvent> Events { get; set; }

        /// <summary>
        /// Gets this trace Evidence
        /// </summary>
        [DataMember(Name = "evidence")]
        public string Evidence { get; set; }

        [DataMember(Name = "first_time_seen")]
        private long FirstTimeSeenRawValue { get; set; }

        /// <summary>
        /// Time first seen
        /// </summary>
        public DateTime? FirstTimeSeen { get; set; }

        /// <summary>
        /// Gets whether this trace has a parent app or not.
        /// </summary>
        [DataMember(Name = "hasParentApp")]
        public bool HasParentApp { get; set; }

        /// <summary>
        /// Gets this trace impact.
        /// </summary>
        [DataMember(Name = "impact")]
        public string Impact { get; set; }

        /// <summary>
        /// Gets the language for the trace.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "last_time_seen")]
        private long LastTimeSeenRawValue { get; set; }

        /// <summary>
        /// Gets the last time the trace was reported.
        /// </summary>
        public DateTime? LastTimeSeen { get; set; }

        /// <summary>
        /// Gets this trace license. Allowed values: ReadOnly, Unlincensed, Licensed.
        /// </summary>
        [DataMember(Name = "license")]
        public string License { get; set; }

        /// <summary>
        /// Gets likelihood for this trace
        /// </summary>
        [DataMember(Name = "likelihood")]
        public string Likelihood { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST endpoint URLs for this trace.
        /// </summary>
        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }

        /// <summary>
        /// List of notes
        /// </summary>
        [DataMember(Name = "notes")]
        public List<TraceNoteResource> Notes { get; set; }

        /// <summary>
        /// Organization Name
        /// </summary>
        [DataMember(Name = "organization_name")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Parent Application ID
        /// </summary>
        [DataMember(Name = "parent_application")]
        public string ParentApplication { get; set; }//TODO Update type to ngapplicationtracebaseresource

        /// <summary>
        /// Is reported to bug tacker
        /// </summary>
        [DataMember(Name = "reported_to_bug_tracker")]
        public bool ReportedToBugTracker { get; set; }

        [DataMember(Name = "reported_to_bug_tracker_time")]
        public long? ReportedToBugTrackerTimeRawValue { get; set; }

        /// <summary>
        /// Time reported to bug tracker
        /// </summary>
        public DateTime? ReportedToBugTrackerTime { get; set; }

        /// <summary>
        /// Gets the HTTP request that caused this trace to occur.
        /// </summary>
        [DataMember(Name = "request")]
        public Request Request { get; set; }

        /// <summary>
        /// Gets the rule-name for the vulnerability.
        /// </summary>
        [DataMember(Name = "rule_name")]
        public string RuleName { get; set; }

        [DataMember(Name = "servers")]
        public List<ServerBase> Servers { get; set; }

        /// <summary>
        /// Gets the severity of this trace.
        /// </summary>
        [DataMember(Name = "severity")]
        public string Severity { get; set; }

        /// <summary>
        /// Gets the status of this trace, like "Reported", "Verified", "Suspicious", etc.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets the sub status of this trace
        /// </summary>
        [DataMember(Name = "sub_status")]
        public string SubStatus { get; set; }

        /// <summary>
        /// Gets the sub-title of the trace.
        /// </summary>
        [DataMember(Name = "sub_title")]
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets the title of the trace.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets the total traces received.
        /// </summary>
        [DataMember(Name = "total_traces_received")]
        public int? TotalTracesReceived { get; set; }

        /// <summary>
        /// Remediation policy violations
        /// </summary>
        [DataMember(Name = "violations")]
        public List<RemediationPolicy> Violations { get; set; }

        [DataMember(Name = "visible")]
        public bool Visible { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            LastTimeSeen = MicrosecondDateTimeConverter.ConvertFromEpochTime(LastTimeSeenRawValue);
            FirstTimeSeen = MicrosecondDateTimeConverter.ConvertFromEpochTime(FirstTimeSeenRawValue);
            ReportedToBugTrackerTime = MicrosecondDateTimeConverter.ConvertFromEpochTime(ReportedToBugTrackerTimeRawValue);
        }
    }

    [DataContract]
    public class TraceNoteResource
    {
        [DataMember(Name = "creation")]
        private long? CreationRawValue { get; set; }

        /// <summary>
        /// Creation time.
        /// </summary>
        public DateTime? Creation { get; set; }

        /// <summary>
        /// Creator name.
        /// </summary>
        [DataMember(Name = "creator")]
        public string Creator { get; set; }

        /// <summary>
        /// Creator UUID.
        /// </summary>
        [DataMember(Name = "creator_uuid")]
        public string CreatorUUID { get; set; }

        /// <summary>
        /// If this note is deletable.
        /// </summary>
        [DataMember(Name = "deletable")]
        public bool Deletable { get; set; }

        /// <summary>
        /// Note id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "last_modification")]
        public long? LastModificationRawValue { get; set; }

        /// <summary>
        /// Last modification time.
        /// </summary>
        public DateTime? LastModification { get; set; }

        /// <summary>
        /// Last updater name.
        /// </summary>
        [DataMember(Name = "last_updater")]
        public string LastUpdater { get; set; }

        /// <summary>
        /// Last updater UUID.
        /// </summary>
        [DataMember(Name = "last_updater_uuid")]
        public string LastUpdaterUUID { get; set; }

        /// <summary>
        /// Note contents.
        /// </summary>
        [DataMember(Name = "note")]
        public string Note { get; set; }

        //TODO Add properties and readOnlyPropertyType if required.

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Creation = MicrosecondDateTimeConverter.ConvertFromEpochTime(CreationRawValue);
            LastModification = MicrosecondDateTimeConverter.ConvertFromEpochTime(LastModificationRawValue);
        }
    }

    [DataContract]
    public class TraceFilterResponse
    {
        /// <summary>
        /// Count
        /// </summary>
        [DataMember(Name = "count")]
        public long Count { get; set; }

        /// <summary>
        /// Number of Traces from a licensed app
        /// </summary>
        [DataMember(Name = "licensedCount")]
        public long LicensedCount { get; set; }

        /// <summary>
        /// List of messages
        /// </summary>
        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not
        /// </summary>
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// List of traces
        /// </summary>
        [DataMember(Name = "traces")]
        public List<Trace> Traces { get; set; }
    }
}
 