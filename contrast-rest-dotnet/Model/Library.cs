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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace contrast_rest_dotnet.Model
{
    /// <summary>
    /// An application library.
    /// </summary>
    [DataContract]
    public class Library
    {
        /// <summary>
        /// Gets the ID of this library.
        /// </summary>
        [DataMember(Name = "libraryId")]
        public string LibraryId { get; set; }

        /// <summary>
        /// Gets the filename of this library.
        /// </summary>
        [DataMember(Name = "filename")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets the SHA1 has of this library.
        /// </summary>
        [DataMember(Name = "sha1")]
        public string SHA1 { get; set; }

        /// <summary>
        /// Gets the URL for a library.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets the version of this library according to the library authority
	    /// like Maven Central or NuGet.
        /// </summary>
        [DataMember(Name = "version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets whether this library was profiled.
        /// </summary>
        [DataMember(Name = "profiled")]
        public bool Profiled { get; set; }

        /// <summary>
        /// Gets whether this library is common.
        /// </summary>
        [DataMember(Name = "common")]
        public bool Common { get; set; }

        /// <summary>
        /// Gets whether this library is sponsored.
        /// </summary>
        [DataMember(Name = "sponsored")]
        public bool Sponsored { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST endpoint URLs for this library.
        /// </summary>
        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }

        /// <summary>
        /// Gets an estimate of the number of lines of code in this library.
        /// </summary>
        [DataMember(Name = "lines-of-code")]
        public int LinesOfCode { get; set; }

        /// <summary>
        /// Gets the last date that an entry within this file was altered.
        /// </summary>
        [DataMember(Name = "internal-date")]
        public string InternalDate { get; set; }

        /// <summary>
        /// Gets the last date the library was altered on disk.
        /// </summary>
        [DataMember(Name = "external-date")]
        public string ExternalDate { get; set; }

        /// <summary>
        /// Gets the number of classes in this library.
        /// </summary>
        [DataMember(Name = "class-count")]
        public int ClassCount { get; set; }

        /// <summary>
        /// Gets the number of classes used by this library. Right now, this only
	    /// returns the maximum number of classes used by any one instance of the
	    /// running application. In the future, this will be changed to represent
	    /// the total number of distinct classes used across all instances of the
	    /// running application.
        /// </summary>
        [DataMember(Name = "used-class-count")]
        public int UsedClassCount { get; set; }

        /// <summary>
        /// Gets the CVE count for this library.
        /// </summary>
        [DataMember(Name = "cve-count")]
        public int CveCount { get; set; }
    }
}
