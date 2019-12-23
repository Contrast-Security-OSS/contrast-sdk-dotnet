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
    public class Score
    {
        /// <summary>
        /// Grade
        /// </summary>
        [JsonProperty(PropertyName = "grade")]
        public int? Grade { get; set; }

        /// <summary>
        /// Letter grade
        /// </summary>
        [JsonProperty(PropertyName = "letter_grade")]
        public string LetterGrade { get; set; }

        /// <summary>
        /// Library scoring type. Allowed values: DEFAULT, VULN
        /// </summary>
        [JsonProperty(PropertyName = "library_scoring_type")]
        public string LibraryScoringType { get; set; }

        [JsonProperty(PropertyName = "links")]
        public List<Link> Links { get; set; }

        /// <summary>
        /// Overall scoring type
        /// </summary>
        [JsonProperty(PropertyName = "overall_scoring_type")]
        public string OverallScoringType { get; set; }

        /// <summary>
        /// Platform score
        /// </summary>
        [JsonProperty(PropertyName = "platform")]
        public ScoreMetricResource Platform { get; set; }

        /// <summary>
        /// Security score
        /// </summary>
        [JsonProperty(PropertyName = "security")]
        public ScoreMetricResource Security { get; set; }
    }

    [JsonObject]
    public class ScoreMetricResource
    {
        /// <summary>
        /// Grade
        /// </summary>
        [JsonProperty(PropertyName = "grade")]
        public int? Grade { get; set; }

        /// <summary>
        /// Letter grade
        /// </summary>
        [JsonProperty(PropertyName = "letter_grade")]
        public string LetterGrade { get; set; }

        [JsonProperty(PropertyName = "links")]
        public List<Link> Links { get; set; }
    }
}
