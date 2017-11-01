/*
 * Copyright (c) 2017, Contrast Security, Inc.
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
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    [DataContract]
    public class TraceStory
    {
        [DataMember(Name = "traceId")]
        public string TraceId { get; set; }

        [DataMember(Name = "chapters")]
        public List<Chapter> Chapters { get; set; }

        [DataMember(Name = "risk")]
        public Snippet Risk { get; set; }
    }

    [DataContract]
    public class Property
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }

    [DataContract]
    public class Chapter
    {
        [DataMember(Name = "type")]
        public String Type { get; set; }
        
        [DataMember(Name = "introText")]
        public string IntroText { get; set; }

        [DataMember(Name = "introTextFormat")]
        public string IntroTextFormat { get; set; }

        [DataMember(Name = "introTextVariables")]
        public Dictionary<string,string> IntroTextVariables { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "bodyFormat")]
        public string BodyFormat { get; set; }

        [DataMember(Name = "bodyFormatVariables")]
        public Dictionary<string, string> BodyFormatVariables { get; set; }

        [DataMember(Name = "propertyResources")]
        public List<Property> Properties { get; set; }
    }

    [DataContract]
    public class TraceStoryResponse
    {
        /// <summary>
        /// Custom risk.
        /// </summary>
        [DataMember(Name = "custom_risk")]
        public Snippet CustomRisk { get; set; }

        /// <summary>
        /// List of messages.
        /// </summary>
        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Trace story.
        /// </summary>
        [DataMember(Name = "story")]
        public TraceStory Story { get; set; }

        /// <summary>
        /// Indicate whether API response was successful or not.
        /// </summary>
        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}
