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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    [DataContract]
    public class TraceEventDetail
    {
        /// <summary>
        /// [Optional] Class name
        /// </summary>
        [DataMember(Name = "class")]
        public string ClassName { get; set; }

        /// <summary>
        /// Last custom frame.
        /// </summary>
        [DataMember(Name = "lastCustomFrame")]
        public long? LastCustomFrame { get; set; }

        /// <summary>
        /// [Optional] Method
        /// </summary>
        [DataMember(Name = "method")]
        public string Method { get; set; }

        /// <summary>
        /// [Optional] Object
        /// </summary>
        [DataMember(Name = "object")]
        public string Object { get; set; }

        /// <summary>
        /// If the object is being tracked.
        /// </summary>
        [DataMember(Name = "objectTracked")]
        public bool ObjectTracked { get; set; }

        /// <summary>
        /// List of parameters
        /// </summary>
        [DataMember(Name = "parameters")]
        public List<EventParameter> Parameters { get; set; }

        /// <summary>
        /// If the return is tracked.
        /// </summary>
        [DataMember(Name = "returnTracked")]
        public bool ReturnTracked { get; set; }

        /// <summary>
        /// [Optional] Return value.
        /// </summary>
        [DataMember(Name = "returnValue")]
        public string ReturnValue { get; set; }

        /// <summary>
        /// List of stack traces.
        /// </summary>
        [DataMember(Name = "stacktraces")]
        public List<Stacktrace> StackTraces { get; set; }
    }

    [DataContract]
    public class EventParameter
    {
        [DataMember(Name = "parameter")]
        public string Parameter { get; set; }

        [DataMember(Name = "tracked")]
        public bool Tracked { get; set; }
    }

    [DataContract]
    public class Stacktrace
    {
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class TraceEventDetailResponse
    {
        /// <summary>
        /// Event
        /// </summary>
        [DataMember(Name = "event")]
        public TraceEventDetail Event { get; set; }

        /// <summary>
        /// List of messges
        /// </summary>
        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not
        /// </summary>
        [DataMember(Name = "succes")]
        public bool Success { get; set; }
    }
}
