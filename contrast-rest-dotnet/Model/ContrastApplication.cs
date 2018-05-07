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

namespace contrast_rest_dotnet.Model
{
    /// <summary>
    /// An application that is being monitored by Contrast.
    /// </summary>
    [JsonObject]
    public class ContrastApplication
    {
        /// <summary>
        /// Gets the ID of this application, which is a long, alphanumeric token.
        /// </summary>
        [JsonProperty(PropertyName="app_id")]
        public string AppID { get; set; }

        /// <summary>
        /// If the application is archived
        /// </summary>
        [JsonProperty(PropertyName = "archived")]
        public bool Archived { get; set; }

        /// <summary>
        /// If the application has assessment enabled.
        /// </summary>
        [JsonProperty(PropertyName = "assess")]
        public bool Assess { get; set; }

        /// <summary>
        /// If application has assessment pending for at least one of this
        /// application's server.
        /// </summary>
        [JsonProperty(PropertyName = "assessPending")]
        public bool AssessPending { get; set; }

        /// <summary>
        /// Attack status label.
        /// </summary>
        [JsonProperty(PropertyName = "attack_label")]
        public string AttackLabel { get; set; }

        /// <summary>
        /// Attack status. Allowed values: PROBED, EXPLOITED.
        /// </summary>
        [JsonProperty(PropertyName = "attack_status")]
        public string AttackStatus { get; set; }

        /// <summary>
        /// Custom classes LoC
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public long? Code { get; set; }

        /// <summary>
        /// Custom classes LoC shorthand
        /// </summary>
        [JsonProperty(PropertyName = "code_shorthand")]
        public string CodeShorthand { get; set; }

        /// <summary>
        /// Time it was created.
        /// </summary>
        [JsonConverter(typeof(EpochDateTimeConverter))]
        [JsonProperty(PropertyName = "created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// If Defense is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "defend")]
        public bool Defend { get; set; }

        /// <summary>
        /// If Defense is pending for any of this application's servers.
        /// </summary>
        [JsonProperty(PropertyName = "defendPending")]
        public bool DefendPending { get; set; }

        /// <summary>
        /// Gets the group name.
        /// </summary>
        [JsonProperty(PropertyName = "group_name")]
        public string GroupName { get; set; }

        /// <summary>
        /// Application importance.
        /// </summary>
        [JsonProperty(PropertyName = "importance")]
        public int? Importance { get; set; }

        /// <summary>
        /// Application importance label
        /// </summary>
        [JsonProperty(PropertyName = "importance_description")]
        public ApplicationImportance? ImportanceLabel { get; set; }

        /// <summary>
        /// Gets the language of the application, e.g., Java.
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Time last reset.
        /// </summary>
        [JsonConverter(typeof(EpochDateTimeConverter))]
        [JsonProperty(PropertyName = "last_reset")]
        public DateTime? LastReset { get; set; }

        /// <summary>
        /// Return the time the application was last monitored by Contrast.
        /// </summary>
        [JsonConverter(typeof(EpochDateTimeConverter))]
        [JsonProperty(PropertyName = "last_seen")]
        public DateTime? LastSeen { get; set; }

        /// <summary>
        /// Gets the license level of the applied; one of Enterprise, Business, Pro, Trial 
        /// </summary>
        [JsonProperty(PropertyName = "license")]
        public ApplicationLicense License { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST URLs for this application.
        /// </summary>
        [JsonProperty(PropertyName = "links")]
        public List<Link> Links { get; set; }

        /// <summary>
        /// If this application is master.
        /// </summary>
        [JsonProperty(PropertyName = "master")]
        public bool Master { get; set; }

        /// <summary>
        /// Application child modules
        /// </summary>
        [JsonProperty(PropertyName = "modules")]
        public List<ApplicationModule> Modules { get; set; }

        /// <summary>
        /// Gets the human-readable name of the web application. Note that this method will
        /// return the site name for apps that run at the root of the app.
        /// </summary>
        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }

        /// <summary>
        /// Application notes.
        /// </summary>
        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Override url for this application.
        /// </summary>
        [JsonProperty(PropertyName = "override_url")]
        public string OverrideUrl { get; set; }

        /// <summary>
        /// Parent application ID.
        /// </summary>
        [JsonProperty(PropertyName = "parentApplication")]
        public string ParentApplicationId { get; set; }

        /// <summary>
        /// Gets the path of the web application, e.g., /AcmeApp
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        /// <summary>
        /// List of allowed roles.
        /// </summary>
        [JsonProperty(PropertyName = "roles")]
        public List<string> Roles { get; set; }

        /// <summary>
        /// List of application scores.
        /// </summary>
        [JsonProperty(PropertyName = "scores")]
        public List<Score> Scores { get; set; }

        /// <summary>
        /// If this application has servers without protection enabled.
        /// </summary>
        [JsonProperty(PropertyName = "serversWithoutDefend")]
        public bool ServersWithoutDefend { get; set; }

        /// <summary>
        /// Application's short name.
        /// </summary>
        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; set; }

        /// <summary>
        /// Total LoC
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long? Size { get; set; }

        /// <summary>
        /// Total LoC shorthand.
        /// </summary>
        [JsonProperty(PropertyName = "size_shorthand")]
        public string SizeShorthand { get; set; }

        /// <summary>
        /// Application status
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Stauts { get; set; }

        /// <summary>
        /// List of tags
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets a list of technologies the app is using, e.g., WebForms, Spring, Applet, JSF, Flash, etc.
        /// </summary>
        [JsonProperty(PropertyName = "techs")]
        public List<string> Technologies { get; set; }

        /// <summary>
        /// Number of app modules.
        /// </summary>
        [JsonProperty(PropertyName = "total_modules")]
        public long? TotalModules { get; set; }

        /// <summary>
        /// Application vulnerability breakdown.
        /// </summary>
        [JsonProperty(PropertyName = "trace_breakdown")]
        public TraceBreakdown TraceBreakdown { get; set; }
    }

    public enum ApplicationImportance
    {
        None,
        Low,
        Medium,
        High,
        Critical
    }

    [JsonObject]
    public class ApplicationLicense
    {
        /// <summary>
        /// License end time
        /// </summary>
        [JsonProperty(PropertyName = "end")]
        public long End { get; set; }

        /// <summary>
        /// Service level
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        public string Level { get; set; }

        /// <summary>
        /// If license is near expiration time.
        /// </summary>
        [JsonProperty(PropertyName = "near_expiration")]
        public bool NearExpiration { get; set; }

        /// <summary>
        /// License start time.
        /// </summary>
        [JsonProperty(PropertyName = "start")]
        public long Start { get; set; }
    }

    [JsonObject]
    public class ApplicationResponse
    {
        [JsonProperty(PropertyName = "application")]
        public ContrastApplication Application { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }

    [JsonObject]
    public class ApplicationsResponse
    {
        [JsonProperty(PropertyName = "applications")]
        public List<ContrastApplication> Applications { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }
}
