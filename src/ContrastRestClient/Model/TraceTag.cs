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

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Contrast.Model
{
    [JsonObject]
    public class FieldErrorItem
    {
        /// <summary>
        /// Field name.
        /// </summary>
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }

    [JsonObject]
    public class TagsServersResource
    {
        /// <summary>
        /// List of links.
        /// </summary>
        [JsonProperty(PropertyName = "links")]
        public List<string> Links;

        /// <summary>
        /// List of tags
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags;

        /// <summary>
        /// List of traces UUID
        /// </summary>
        [JsonProperty(PropertyName = "traces_id")]
        public List<string> TracesId;
    }

    [JsonObject]
    public class TagRequest
    {
        [JsonProperty(PropertyName = "tag")]
        public string Tag { get; set; }
    }

    [JsonObject]
    public class TagsTraceRequest
    {
        /// <summary>
        /// List of links.
        /// </summary>
        [JsonProperty(PropertyName = "links")]
        public List<string> Links { get; set; }
        
        /// <summary>
        /// List of traces UUID.
        /// </summary>
        [JsonProperty(PropertyName = "traces_uuid")]
        public List<string> TracesId { get; set; }
    }

    [JsonObject]
    public class TagsTracesUpdateRequest
    {
        /// <summary>
        /// List of links.
        /// </summary>
        [JsonProperty(PropertyName = "links")]
        public List<string> Links { get; set; }

        /// <summary>
        /// List of tags to add.
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// List of traces UUID.
        /// </summary>
        [JsonProperty(PropertyName = "traces_uuid")]
        public List<string> TracesId { get; set; }

        /// <summary>
        /// Lists of tags to remove.
        /// </summary>
        [JsonProperty(PropertyName = "tags_remove")]
        public List<string> TagsRemove { get; set; }
    }

    [JsonObject]
    public class TagsResponse
    {
        /// <summary>
        /// List of errors.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public List<FieldErrorItem>  Errors { get; set; }

        /// <summary>
        /// List of messages.
        /// </summary>
        [JsonProperty(PropertyName = "messages")]
        public List<string> Messages { get; set; }

        /// <summary>
        /// Indicates whether API response was successful or not.
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// List of tags.
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Total number of library hashes.
        /// </summary>
        [JsonProperty(PropertyName = "totalLibraryHashes")]
        public int TotalLibraryHashes { get; set; }
    }
}
