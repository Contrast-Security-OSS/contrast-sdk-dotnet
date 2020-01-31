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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Contrast.Model
{
    [JsonObject]
    public class TraceEventDetail
    {
        /// <summary>
        /// [Optional] Class name
        /// </summary>
        [JsonProperty(PropertyName = "class")]
        public string ClassName { get; set; }

        /// <summary>
        /// Last custom frame.
        /// </summary>
        [JsonProperty(PropertyName = "lastCustomFrame")]
        public long? LastCustomFrame { get; set; }

        /// <summary>
        /// [Optional] Method
        /// </summary>
        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        /// <summary>
        /// [Optional] Object
        /// </summary>
        [JsonProperty(PropertyName = "object")]
        public string Object { get; set; }

        /// <summary>
        /// If the object is being tracked.
        /// </summary>
        [JsonProperty(PropertyName = "objectTracked")]
        public bool ObjectTracked { get; set; }

        /// <summary>
        /// List of parameters
        /// </summary>
        [JsonProperty(PropertyName = "parameters")]
        public List<EventParameter> Parameters { get; set; }

        /// <summary>
        /// If the return is tracked.
        /// </summary>
        [JsonProperty(PropertyName = "returnTracked")]
        public bool ReturnTracked { get; set; }

        /// <summary>
        /// [Optional] Return value.
        /// </summary>
        [JsonProperty(PropertyName = "returnValue")]
        public string ReturnValue { get; set; }

        /// <summary>
        /// List of stack traces.
        /// </summary>
        [JsonProperty(PropertyName = "stacktraces")]
        public List<Stacktrace> StackTraces { get; set; }
    }

    [JsonObject]
    public class EventParameter
    {
        /// <summary>
        /// Parameter value.
        /// </summary>
        [JsonProperty(PropertyName = "parameter")]
        public string Parameter { get; set; }

        /// <summary>
        /// Whether the parameter is being tracked.
        /// </summary>
        [JsonProperty(PropertyName = "tracked")]
        public bool Tracked { get; set; }
    }

    [JsonObject]
    public class Stacktrace
    {
        /// <summary>
        /// StackTrace content.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Stack trace type (e.g. custom, common)
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Line index
        /// </summary>
        [JsonProperty(PropertyName = "stackFrameIndex")]
        public long StackFrameIndex { get; set; }
    }

    [JsonObject]
    public class TraceEventDetailResponse
    {
        /// <summary>
        /// Event
        /// </summary>
        [JsonProperty(PropertyName = "event")]
        public TraceEventDetail Event { get; set; }

        /// <summary>
        /// List of messages
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not
        /// </summary>
        [JsonProperty(PropertyName = "succes")]
        public bool Success { get; set; }
    }
}
