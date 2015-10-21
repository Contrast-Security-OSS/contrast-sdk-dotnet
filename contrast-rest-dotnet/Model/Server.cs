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
        /// Gets the ID for the server.
        /// </summary>
        [DataMember(Name = "serverId")]
        public string ServerId { get; set; }

        [DataMember(Name = "lastStartupReceived")]
        private long lastStartupReceived { get; set; }

        /// <summary>
        /// Gets the last time this server was started or restarted.
        /// </summary>
        public DateTime LastStartupReceived { get; set; }

        [DataMember(Name = "lastTraceReceived")]
        private long lastTraceReceived { get; set; }

        /// <summary>
        /// Gets the last time a trace was received from this server.
        /// </summary>
        public DateTime LastTraceReceived { get; set; }

        [DataMember(Name = "lastActivity")]
        private long lastActivity { get; set; }

        /// <summary>
        /// Gets the last time any activity was received from this server.
        /// </summary>
        public DateTime LastActivity { get; set; }

        /// <summary>
        /// Gets the hostname of this server.
        /// </summary>
        [DataMember(Name = "hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets the path on disk of this server, e.g., /opt/tomcat6/ or C:\Windows\System32\inetsrv\
        /// </summary>
        [DataMember(Name = "serverPath")]
        public string ServerPath { get; set; }

        /// <summary>
        /// Gets the Contrast "server code" for the server, e.g., "IIS7", "jboss5"
        /// </summary>
        [DataMember(Name = "serverType")]
        public string ServerType { get; set; }

        /// <summary>
        /// Gets whether the server has Contrast enabled.
        /// </summary>
        [DataMember(Name = "enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets the version of the engine.
        /// </summary>
        [DataMember(Name = "engineVersion")]
        public string EngineVersion { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST endpoint URLs for this server.
        /// </summary>
        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            LastStartupReceived = MicrosecondDateTimeConverter.ConvertFromEpochTime(lastStartupReceived);
            LastTraceReceived = MicrosecondDateTimeConverter.ConvertFromEpochTime(lastTraceReceived);
            LastActivity = MicrosecondDateTimeConverter.ConvertFromEpochTime(lastActivity);
        }
    }
}
