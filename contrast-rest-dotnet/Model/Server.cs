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
    /// A server with the contrast agent installed.
    /// </summary>
    [DataContract]
    public class Server
    {
        /// <summary>
        /// Agent version
        /// </summary>
        [DataMember(Name = "agent_version")]
        public string AgentVersion { get; set; }

        /// <summary>
        /// Return the list of applications in this server.
        /// </summary>
        [DataMember(Name = "applications")]
        public List<ContrastApplication> Applications { get; set; }

        /// <summary>
        /// If this server has assess enabled.
        /// </summary>
        [DataMember(Name = "assess")]
        public bool Assess { get; set; }

        /// <summary>
        /// If the server is changing Assess on restart.
        /// </summary>
        [DataMember(Name = "assessPending")]
        public bool AssessPending { get; set; }

        [DataMember(Name = "assess_last_update")]
        private long? AssessLastUpdateRawValue { get; set; }

        /// <summary>
        /// Last assess change time.
        /// </summary>
        public DateTime? AssessLastUpdate { get; set; }

        /// <summary>
        /// If the assess sensors are active.
        /// </summary>
        [DataMember(Name = "assess_sensonrs")]
        public bool AssessSensors { get; set; }

        /// <summary>
        /// Container
        /// </summary>
        [DataMember(Name = "container")]
        public string Container { get; set; }

        /// <summary>
        /// If server has Defend.
        /// </summary>
        [DataMember(Name = "defend")]
        public bool Defend { get; set; }

        /// <summary>
        /// If server is changing Defend on restart.
        /// </summary>
        [DataMember(Name = "defendPending")]
        public bool DefendPending { get; set; }

        /// <summary>
        /// If server has defend sensors active.
        /// </summary>
        [DataMember(Name = "defend_sensors")]
        public bool DefendSensors { get; set; }

        [DataMember(Name = "defense_last_update")]
        private long? DefenseLastUpdateRawValue { get; set; }

        /// <summary>
        /// Last defense change time.
        /// </summary>
        public DateTime? DefenseLastUpdate { get; set; }

        /// <summary>
        /// Server environment. Allowed values: DEVELOPMENT, QA, PRODUCTION.
        /// </summary>
        [DataMember(Name = "environment")]
        public string Environment { get; set; }

        /// <summary>
        /// Gets the hostname of this server.
        /// </summary>
        [DataMember(Name = "hostname")]
        public string Hostname { get; set; }

        [DataMember(Name = "lastActivity")]
        private long LastActivityRawValue { get; set; }

        /// <summary>
        /// Gets the last time any activity was received from this server.
        /// </summary>
        public DateTime? LastActivity { get; set; }

        [DataMember(Name = "last_startup")]
        private long LastStartupRawValue { get; set; }

        /// <summary>
        /// Gets the last time this server was started or restarted.
        /// </summary>
        public DateTime? LastStartup{ get; set; }

        /// <summary>
        /// If server s changing Log Enhancers on restart.
        /// </summary>
        [DataMember(Name = "logEnhancerPending")]
        public bool LogEnhancerPending { get; set; }

        /// <summary>
        /// Security log level.
        /// </summary>
        [DataMember(Name = "logLevel")]
        public string LogLevel { get; set; }

        /// <summary>
        /// Log path
        /// </summary>
        [DataMember(Name = "logPath")]
        public string LogPath { get; set; }

        /// <summary>
        /// Server name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// If server is changing any settings on restart.
        /// </summary>
        [DataMember(Name = "noPending")]
        public bool NoPending { get; set; }

        /// <summary>
        /// Number of applications on server.
        /// </summary>
        [DataMember(Name = "num_apps")]
        public long? TotalApps { get; set; }

        /// <summary>
        /// If the agent on this server is out of date.
        /// </summary>
        [DataMember(Name = "out_of_date")]
        public bool OutOfDate { get; set; }

        /// <summary>
        /// Server path
        /// </summary>
        [DataMember(Name = "path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets the ID for the server.
        /// </summary>
        [DataMember(Name = "server_id")]
        public long ServerId { get; set; }

        /// <summary>
        /// Server status. Allowed values: ONLINE, OFFLINE.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// If Syslog is enabled.
        /// </summary>
        [DataMember(Name = "syslog_enabled")]
        public bool SyslogEnabled { get; set; }

        /// <summary>
        /// Syslog IP adress.
        /// </summary>
        [DataMember(Name = "syslog_ip_address")]
        public string SyslogIpAddress { get; set; }

        /// <summary>
        /// List of tags.
        /// </summary>
        [DataMember(Name = "tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Get this server's type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            AssessLastUpdate = MicrosecondDateTimeConverter.ConvertFromEpochTime(AssessLastUpdateRawValue);
            DefenseLastUpdate = MicrosecondDateTimeConverter.ConvertFromEpochTime(DefenseLastUpdateRawValue);
            LastStartup = MicrosecondDateTimeConverter.ConvertFromEpochTime(LastStartupRawValue);
            LastActivity = MicrosecondDateTimeConverter.ConvertFromEpochTime(LastActivityRawValue);
        }
    }

    [DataContract]
    public class ServerResponse
    {
        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }

        [DataMember(Name = "server")]
        public Server Server { get; set; }

        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }

    [DataContract]
    public class ServersResponse
    {
        [DataMember(Name = "count")]
        public long Count { get; set; }

        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }

        [DataMember(Name = "servers")]
        public List<Server> Servers { get; set; }

        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}
