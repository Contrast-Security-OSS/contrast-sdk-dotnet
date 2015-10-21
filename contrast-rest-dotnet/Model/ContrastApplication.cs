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
        [DataMember(Name="app-id")]
        public string AppID { get; set; }

        /// <summary>
        /// Gets the human-readable name of the web application. Note that this method will
        /// return the site name for apps that run at the root of the app.
        /// </summary>
        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the group name.
        /// </summary>
        [DataMember(Name = "group-name")]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets the path of the web application, e.g., /AcmeApp
        /// </summary>
        [DataMember(Name = "path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets the language of the application, e.g., Java.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets the license level of the applied; one of Enterprise, Business, Pro, Trial 
        /// </summary>
        [DataMember(Name = "license")]
        public string License { get; set; }

        /// <summary>
        /// Gets the platform version.
        /// </summary>
        [DataMember(Name = "platform-version")]
        public string PlatformVersion { get; set; }

        /// <summary>
        /// Gets the platform vulnerabilities.
        /// </summary>
        [DataMember(Name = "platform-vulnerabilities")]
        public List<object> PlatformVulnerabilities { get; set; }

        [DataMember(Name = "last-seen")]
        private long lastSeen { get; set; }

        /// <summary>
        /// Return the time the application was last monitored by Contrast.
        /// </summary>
        public DateTime LastSeen { get; set; }

        /// <summary>
        /// Gets the views of this application.
        /// </summary>
        [DataMember(Name = "views")]
        public int Views { get; set; }

        /// <summary>
        /// Gets a list of technologies the app is using, e.g., WebForms, Spring, Applet, JSF, Flash, etc.
        /// </summary>
        [DataMember(Name = "technologies")]
        public List<string> Technologies { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST URLs for this application.
        /// </summary>
        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            LastSeen = MicrosecondDateTimeConverter.ConvertFromEpochTime(lastSeen);
        }
    }
}
