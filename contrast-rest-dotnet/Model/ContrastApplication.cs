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
    /// An application that is being monitored by Contrast.
    /// </summary>
    [DataContract]
    public class ContrastApplication
    {
        /// <summary>
        /// Gets the ID of this application, which is a long, alphanumeric token.
        /// </summary>
        [DataMember(Name="app_id")]
        public string AppID { get; set; }

        /// <summary>
        /// If the application is archived
        /// </summary>
        [DataMember(Name = "archived")]
        public bool Archived { get; set; }

        /// <summary>
        /// If the application has assessment enabled.
        /// </summary>
        [DataMember(Name = "assess")]
        public bool Assess { get; set; }

        /// <summary>
        /// If application has assessment pending for at least one of this
        /// application's server.
        /// </summary>
        [DataMember(Name = "assessPending")]
        public bool AssessPending { get; set; }

        /// <summary>
        /// Attack status label.
        /// </summary>
        [DataMember(Name = "attack_label")]
        public string AttackLabel { get; set; }

        /// <summary>
        /// Attack status. Allowed values: PROBED, EXPLOITED.
        /// </summary>
        [DataMember(Name = "attack_status")]
        public string AttackStatus { get; set; }

        /// <summary>
        /// Custom classes LoC
        /// </summary>
        [DataMember(Name = "code")]
        public long? Code { get; set; }

        /// <summary>
        /// Custom classes LoC shorthand
        /// </summary>
        [DataMember(Name = "code_shorthand")]
        public string CodeShorthand { get; set; }

        [DataMember(Name = "created")]
        public long? CreatedRawValue { get; set; }

        /// <summary>
        /// Time it was created.
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// If Defense is enabled.
        /// </summary>
        [DataMember(Name = "defend")]
        public bool Defend { get; set; }

        /// <summary>
        /// If Defense is pending for any of this application's servers.
        /// </summary>
        [DataMember(Name = "defendPending")]
        public bool DefendPending { get; set; }

        /// <summary>
        /// Gets the group name.
        /// </summary>
        [DataMember(Name = "group_name")]
        public string GroupName { get; set; }

        /// <summary>
        /// Application importance.
        /// </summary>
        [DataMember(Name = "importance")]
        public int? Importance { get; set; }

        /// <summary>
        /// Gets the language of the application, e.g., Java.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "last_reset")]
        private long? LastResetRawValue { get; set; }

        /// <summary>
        /// Time last reset.
        /// </summary>
        public DateTime? LastReset { get; set; }

        [DataMember(Name = "last_seen")]
        private long LastSeenRawValue { get; set; }

        /// <summary>
        /// Return the time the application was last monitored by Contrast.
        /// </summary>
        public DateTime? LastSeen { get; set; }

        /// <summary>
        /// Gets the license level of the applied; one of Enterprise, Business, Pro, Trial 
        /// </summary>
        [DataMember(Name = "license")]
        public ApplicationLicense License { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST URLs for this application.
        /// </summary>
        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }

        /// <summary>
        /// If this application is master.
        /// </summary>
        [DataMember(Name = "master")]
        public bool Master { get; set; }

        /// <summary>
        /// Application child modules
        /// </summary>
        [DataMember(Name = "modules")]
        public List<ApplicationModule> Modules { get; set; }

        /// <summary>
        /// Gets the human-readable name of the web application. Note that this method will
        /// return the site name for apps that run at the root of the app.
        /// </summary>
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Application notes.
        /// </summary>
        [DataMember(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Override url for this application.
        /// </summary>
        [DataMember(Name = "override_url")]
        public string OverrideUrl { get; set; }

        /// <summary>
        /// Parent application ID.
        /// </summary>
        [DataMember(Name = "parentApplication")]
        public string ParentApplicationId { get; set; }

        /// <summary>
        /// Gets the path of the web application, e.g., /AcmeApp
        /// </summary>
        [DataMember(Name = "path")]
        public string Path { get; set; }

        /// <summary>
        /// List of allowed roles.
        /// </summary>
        [DataMember(Name = "roles")]
        public List<string> Roles { get; set; }

        /// <summary>
        /// List of application scores.
        /// </summary>
        [DataMember(Name = "scores")]
        public List<Score> Scores { get; set; }

        /// <summary>
        /// If this application has servers without protection enabled.
        /// </summary>
        [DataMember(Name = "serversWithoutDefend")]
        public bool ServersWithoutDefend { get; set; }

        /// <summary>
        /// Application's short name.
        /// </summary>
        [DataMember(Name = "short_name")]
        public string ShortName { get; set; }

        /// <summary>
        /// Total LoC
        /// </summary>
        [DataMember(Name = "size")]
        public long? Size { get; set; }

        /// <summary>
        /// Total LoC shorthand.
        /// </summary>
        [DataMember(Name = "size_shorthand")]
        public string SizeShorthand { get; set; }

        /// <summary>
        /// Application status
        /// </summary>
        [DataMember(Name = "status")]
        public string Stauts { get; set; }

        /// <summary>
        /// List of tags
        /// </summary>
        [DataMember(Name = "tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets a list of technologies the app is using, e.g., WebForms, Spring, Applet, JSF, Flash, etc.
        /// </summary>
        [DataMember(Name = "techs")]
        public List<string> Technologies { get; set; }

        /// <summary>
        /// Number of app modules.
        /// </summary>
        [DataMember(Name = "total_modules")]
        public long? TotalModules { get; set; }

        /// <summary>
        /// Application vulnerability breakdown.
        /// </summary>
        [DataMember(Name = "trace_breakdown")]
        public TraceBreakdown TraceBreakdown { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Created = MicrosecondDateTimeConverter.ConvertFromEpochTime(CreatedRawValue);
            LastReset = MicrosecondDateTimeConverter.ConvertFromEpochTime(LastResetRawValue);
            LastSeen = MicrosecondDateTimeConverter.ConvertFromEpochTime(LastSeenRawValue);
        }
    }

    [DataContract]
    public class ApplicationLicense
    {
        /// <summary>
        /// License end time
        /// </summary>
        [DataMember(Name = "end")]
        public long End { get; set; }

        /// <summary>
        /// Service level
        /// </summary>
        [DataMember(Name = "level")]
        public string Level { get; set; }

        /// <summary>
        /// If license is near expiration time.
        /// </summary>
        [DataMember(Name = "near_expiration")]
        public bool NearExpiration { get; set; }

        /// <summary>
        /// License start time.
        /// </summary>
        [DataMember(Name = "start")]
        public long Start { get; set; }
    }

    [DataContract]
    public class ApplicationResponse
    {
        [DataMember(Name = "application")]
        public ContrastApplication Application { get; set; }

        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }

        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }

    [DataContract]
    public class ApplicationsResponse
    {
        [DataMember(Name = "applications")]
        public List<ContrastApplication> Applications { get; set; }

        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }

        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}
