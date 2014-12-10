/*
 * Copyright (c) 2014, Contrast Security, Inc.
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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace contrast_rest_dotnet.Model
{
    /// <summary>
    /// A profile for agent downloads containing specifics for TeamServer URL, proxy settings, etc.
    /// </summary>
    [DataContract]
    public class Profile
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the sampling baseline.
        /// </summary>
        [DataMember(Name = "samplingBaseline")]
        public int SamplingBaseline { get; set; }

        /// <summary>
        /// Gets the sampling window.
        /// </summary>
        [DataMember(Name = "samplingWindow")]
        public int SamplingWindow { get; set; }

        /// <summary>
        /// Gets the sampling frequency.
        /// </summary>
        [DataMember(Name = "samplingFrequency")]
        public int SamplingFrequency { get; set; }

        /// <summary>
        /// Gets the stack trace capture mode.
        /// </summary>
        [DataMember(Name = "stacktraceCaptureMode")]
        public string StackTraceCaptureMode { get; set; }

        /// <summary>
        /// Gets whether this agent will use a proxy.
        /// </summary>
        [DataMember(Name = "useProxy")]
        public bool UseProxy { get; set; }

        /// <summary>
        /// Gets the TeamServerUrl.
        /// </summary>
        [DataMember(Name = "overrideTeamServerUrl")]
        public bool OverrideTeamServerUrl { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST endpoint URLs for this profile.
        /// </summary>
        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }
    }
}
