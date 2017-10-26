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
        [Obsolete("Not supported.")]
        [DataMember(Name = "library_id")]
        public string LibraryId { get; set; }

        /// <summary>
        /// Gets the filename of this library.
        /// </summary>
        [DataMember(Name = "file_name")]
        public string FileName { get; set; }

        [DataMember(Name = "app_language")]
        public string AppLanguage { get; set; }

        /// <summary>
        /// If this library is custom.
        /// </summary>
        [DataMember(Name = "custom")]
        public bool Custom { get; set; }

        /// <summary>
        /// Gets the number of classes in this library.
        /// </summary>
        [DataMember(Name = "class_count")]
        public int ClassCount { get; set; }

        /// <summary>
        /// Gets the number of classes used by this library. Right now, this only
	    /// returns the maximum number of classes used by any one instance of the
	    /// running application. In the future, this will be changed to represent
	    /// the total number of distinct classes used across all instances of the
	    /// running application.
        /// </summary>
        [DataMember(Name = "class_used")]
        public int UsedClassCount { get; set; }

        /// <summary>
        /// Gets the version of this library according to the library authority
	    /// like Maven Central or NuGet.
        /// </summary>
        [DataMember(Name = "file_version")]
        public string Version { get; set; }

        [DataMember(Name = "grade")]
        public String Grade { get; set; }

        /// <summary>
        /// Library hash.
        /// </summary>
        [DataMember(Name = "hash")]
        public string Hash { get; set; }

        /// <summary>
        /// Gets a list of Contrast REST endpoint URLs for this library.
        /// </summary>
        [DataMember(Name = "links")]
        public List<Link> Links { get; set; }

        [DataMember(Name = "latest_release_date")]
        public long? LatestReleaseDate { get; set; }

        [DataMember(Name = "months_outdated")]
        public long? MonthsOutdated { get; set; }

        [DataMember(Name = "release_date")]
        public long? ReleaseDate { get; set; }

        [DataMember(Name = "total_vulnerabilities")]
        public long TotalVulnerabilities { get; set; }
    }

    [DataContract]
    public class LibraryResponse
    {
        /// <summary>
        /// Average months
        /// </summary>
        [DataMember(Name = "averageMonths")]
        public int? AverageMonths { get; set; }

        /// <summary>
        /// Average score.
        /// </summary>
        [DataMember(Name = "averageScore")]
        public int? AverageScore { get; set; }

        /// <summary>
        /// Average score letter.
        /// </summary>
        [DataMember(Name = "averageScoreLetter")]
        public string AverageScoreLetter { get; set; }

        [DataMember(Name = "libraries")]
        public List<Library> Libraries { get; set; }
    }
}
