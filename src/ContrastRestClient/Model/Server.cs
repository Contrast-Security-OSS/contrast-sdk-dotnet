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
    /// <summary>
    /// A server with the contrast agent installed.
    /// </summary>
    [JsonObject]
    public class Server
    {
        /// <summary>
        /// Agent version
        /// </summary>
        [JsonProperty(PropertyName = "agent_version")]
        public string AgentVersion { get; set; }

        /// <summary>
        /// Return the list of applications in this server.
        /// </summary>
        [JsonProperty(PropertyName = "applications")]
        public List<ContrastApplication> Applications { get; set; }

        /// <summary>
        /// If this server has assess enabled.
        /// </summary>
        [JsonProperty(PropertyName = "assess")]
        public bool Assess { get; set; }

        /// <summary>
        /// If the server is changing Assess on restart.
        /// </summary>
        [JsonProperty(PropertyName = "assessPending")]
        public bool AssessPending { get; set; }

        /// <summary>
        /// Last assess change time.
        /// </summary>
        [JsonConverter(typeof(EpochDateTimeConverter))]
        [JsonProperty(PropertyName = "assess_last_update")]
        public DateTime? AssessLastUpdate { get; set; }

        /// <summary>
        /// If the assess sensors are active.
        /// </summary>
        [JsonProperty(PropertyName = "assess_sensonrs")]
        public bool AssessSensors { get; set; }

        /// <summary>
        /// Container
        /// </summary>
        [JsonProperty(PropertyName = "container")]
        public string Container { get; set; }

        /// <summary>
        /// If server has Defend.
        /// </summary>
        [JsonProperty(PropertyName = "defend")]
        public bool Defend { get; set; }

        /// <summary>
        /// If server is changing Defend on restart.
        /// </summary>
        [JsonProperty(PropertyName = "defendPending")]
        public bool DefendPending { get; set; }

        /// <summary>
        /// If server has defend sensors active.
        /// </summary>
        [JsonProperty(PropertyName = "defend_sensors")]
        public bool DefendSensors { get; set; }

        /// <summary>
        /// Last defense change time.
        /// </summary>
        [JsonConverter(typeof(EpochDateTimeConverter))]
        [JsonProperty(PropertyName = "defense_last_update")]
        public DateTime? DefenseLastUpdate { get; set; }

        /// <summary>
        /// Server environment. Allowed values: DEVELOPMENT, QA, PRODUCTION.
        /// </summary>
        [JsonProperty(PropertyName = "environment")]
        public string Environment { get; set; }

        /// <summary>
        /// Gets the hostname of this server.
        /// </summary>
        [JsonProperty(PropertyName = "hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets the last time any activity was received from this server.
        /// </summary>
        [JsonConverter(typeof(EpochDateTimeConverter))]
        [JsonProperty(PropertyName = "lastActivity")]
        public DateTime? LastActivity { get; set; }

        /// <summary>
        /// Gets the last time this server was started or restarted.
        /// </summary>
        [JsonConverter(typeof(EpochDateTimeConverter))]
        [JsonProperty(PropertyName = "last_startup")]
        public DateTime? LastStartup{ get; set; }

        /// <summary>
        /// If server s changing Log Enhancers on restart.
        /// </summary>
        [JsonProperty(PropertyName = "logEnhancerPending")]
        public bool LogEnhancerPending { get; set; }

        /// <summary>
        /// Security log level.
        /// </summary>
        [JsonProperty(PropertyName = "logLevel")]
        public string LogLevel { get; set; }

        /// <summary>
        /// Log path
        /// </summary>
        [JsonProperty(PropertyName = "logPath")]
        public string LogPath { get; set; }

        /// <summary>
        /// Server name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// If server is changing any settings on restart.
        /// </summary>
        [JsonProperty(PropertyName = "noPending")]
        public bool NoPending { get; set; }

        /// <summary>
        /// Number of applications on server.
        /// </summary>
        [JsonProperty(PropertyName = "num_apps")]
        public long? TotalApps { get; set; }

        /// <summary>
        /// If the agent on this server is out of date.
        /// </summary>
        [JsonProperty(PropertyName = "out_of_date")]
        public bool OutOfDate { get; set; }

        /// <summary>
        /// Server path
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets the ID for the server.
        /// </summary>
        [JsonProperty(PropertyName = "server_id")]
        public long ServerId { get; set; }

        /// <summary>
        /// Server status. Allowed values: ONLINE, OFFLINE.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// If Syslog is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "syslog_enabled")]
        public bool SyslogEnabled { get; set; }

        /// <summary>
        /// Syslog IP address.
        /// </summary>
        [JsonProperty(PropertyName = "syslog_ip_address")]
        public string SyslogIpAddress { get; set; }

        /// <summary>
        /// List of tags.
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Get this server's type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }

    [JsonObject]
    public class ServerResponse
    {
        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        [JsonProperty(PropertyName = "server")]
        public Server Server { get; set; }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }

    [JsonObject]
    public class ServersResponse
    {
        [JsonProperty(PropertyName = "count")]
        public long Count { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        [JsonProperty(PropertyName = "servers")]
        public List<Server> Servers { get; set; }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }
}
