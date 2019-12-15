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

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Contrast.Model
{
    /// <summary>
    /// A profile for agent downloads containing specifics for TeamServer URL, proxy settings, etc.
    /// </summary>
    [JsonObject]
    public class Profile
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the sampling baseline.
        /// </summary>
        [JsonProperty(PropertyName = "sampling_baseline")]
        public int SamplingBaseline { get; set; }

        /// <summary>
        /// Gets the sampling window.
        /// </summary>
        [JsonProperty(PropertyName = "sampling_window")]
        public int SamplingWindow { get; set; }

        /// <summary>
        /// Gets the sampling frequency.
        /// </summary>
        [JsonProperty(PropertyName = "sampling_frequency")]
        public int SamplingFrequency { get; set; }

        /// <summary>
        /// Gets the stack trace capture mode.
        /// </summary>
        [JsonProperty(PropertyName = "stacktrace_capture_mode")]
        public string StackTraceCaptureMode { get; set; }

        /// <summary>
        /// Gets whether this agent will use a proxy.
        /// </summary>
        [JsonProperty(PropertyName = "use_proxy")]
        public bool UseProxy { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST endpoint URLs for this profile.
        /// </summary>
        [JsonProperty(PropertyName = "links")]
        public List<Link> Links { get; set; }
    }

    [JsonObject]
    public class ProfileResponse
    {
        /// <summary>
        /// List of messages
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Agent profile resource
        /// </summary>
        [JsonProperty(PropertyName = "property")]
        public Profile Profile { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }

    [JsonObject]
    public class ProfilesResponse
    {
        /// <summary>
        /// List of messages
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// List of agent profile resources
        /// </summary>
        [JsonProperty(PropertyName = "profiles")]
        public List<Profile> Profiles { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }
}
