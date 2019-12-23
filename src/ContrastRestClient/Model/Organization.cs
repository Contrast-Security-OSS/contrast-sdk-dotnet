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

using Contrast.Serialization;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Contrast.Model
{
    public enum ServerEnvironment
    {
        DEVELOPMENT,
        QA,
        PRODUCTION
    }

    public class Organization
    {
        /// <summary>
        /// Organization name
        /// </summary>
        public string name { get; set; }

        public string shortname { get; set; }

        /// <summary>
        /// Organization time zone
        /// </summary>
        public string timezone { get; set; }

        public List<Link> links { get; set; }

        /// <summary>
        /// Organization ID
        /// </summary>
        public string organization_uuid { get; set; }

        /// <summary>
        /// Account ID
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public String AccountId { get; set; }

        /// <summary>
        /// Number of applications onboarded
        /// </summary>
        [JsonProperty(PropertyName = "apps_onboarded")]
        public long? AppsOnboarded { get; set; }

        /// <summary>
        /// Auto license assessment
        /// </summary>
        [JsonProperty(PropertyName = "auto_license_assessment")]
        public bool AutoLicenseAssessment { get; set; }

        /// <summary>
        /// Auto license protetion
        /// </summary>
        [JsonProperty(PropertyName = "auto_license_protection")]
        public bool AutoLicenseProtection { get; set; }

        /// <summary>
        /// Organization creation time
        /// </summary>
        [JsonConverter(typeof(EpochDateTimeConverter))]
        [JsonProperty(PropertyName = "creation_time")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// Is this organization freemium?
        /// </summary>
        [JsonProperty(PropertyName = "freemium")]
        public bool? IsFreemium { get; set; }

        /// <summary>
        /// Is user guest in this organization
        /// </summary>
        [JsonProperty(PropertyName = "guest")]
        public bool? IsGuest { get; set; }

        /// <summary>
        /// Is a Superadmin Organization
        /// </summary>
        [JsonProperty(PropertyName = "is_superadmin")]
        public bool? IsSuperadmin { get; set; }

        /// <summary>
        /// Has user protect enabled in this organization?
        /// </summary>
        [JsonProperty(PropertyName = "protect")]
        public bool? IsProtect { get; set; }

        /// <summary>
        /// Protection enabled
        /// </summary>
        [JsonProperty(PropertyName = "protection_enabled")]
        public bool IsProtectionEnabled { get; set; }

        /// <summary>
        /// Sample application ID
        /// </summary>
        [JsonProperty(PropertyName = "sample_application_id")]
        public String SampleAppId { get; set; }

        /// <summary>
        /// Sample server ID
        /// </summary>
        [JsonProperty(PropertyName = "sample_server_id")]
        public long? SampleServerId { get; set; }

        /// <summary>
        /// List of server environments
        /// </summary>
        [JsonProperty(PropertyName = "server_environments")]
        public List<ServerEnvironment> ServerEnvironments { get; set; }

        [JsonProperty(PropertyName = "superadmin")]
        public bool? Superadmin { get; set; }

        /// <summary>
        /// Organization date format
        /// </summary>
        [JsonProperty(PropertyName = "date_format")]
        public string DateFormat { get; set; }

        /// <summary>
        /// Organization time format
        /// </summary>
        [JsonProperty(PropertyName = "time_format")]
        public string TimeFormat { get; set; }
    }

    public class OrganizationResponse
    {
        public List<Organization> organizations { get; set; }
        public int count { get; set; }
        public List<object> org_disabled { get; set; }
    }

    public class DefaultOrganizationResponse
    {
        public bool success { get; set; }
        public List<string> messages { get; set; }
        public Organization organization { get; set; }
        public List<string> roles { get; set; }
        public bool enterprise { get; set; }
    }

    /// <summary>
    /// Organization Managed Response
    /// </summary>
    public class OrganizationManagedResponse
    {
        /// <summary>
        /// If user accounts are managed by Contrast
        /// </summary>
        [JsonProperty(PropertyName = "managed")]
        public bool Managed { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Organization resource
        /// </summary>
        [JsonProperty(PropertyName = "organization")]
        public Organization Organization { get; set; }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }

}
