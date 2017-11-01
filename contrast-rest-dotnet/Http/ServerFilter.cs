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
using contrast_rest_dotnet.Serialization;

namespace contrast_rest_dotnet.Http
{
    public class ServerFilter
    {
        /// <summary>
        /// Name, Hostname or server path.
        /// </summary>
        public string QueryParam { get; set; }
        /// <summary>
        /// Include archived servers.
        /// </summary>
        public bool IncludeArchived { get; set; }
        public List<string> ApplicationIds { get; set; }
        public List<string> LogLevels { get; set; }

        public List<ServerExpandValue> Expand { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> Severities { get; set; }
        public string Status { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Sort { get; set; }

        public ServerFilter()
        {
            QueryParam = "";
            IncludeArchived = false;
            ApplicationIds = null;
            LogLevels = null;

            StartDate = null;
            EndDate = null;
            Severities = null;
            Status = "";
            Expand = null;
            Limit = -1;
            Offset = -1;
            Sort = "";
        }

        public override string ToString()
        {
            List<string> filters = new List<string>();

            if (!String.IsNullOrEmpty(QueryParam))
                filters.Add("q=" + QueryParam);

            filters.Add("includeArchived=" + IncludeArchived);

            if (ApplicationIds != null && ApplicationIds.Count > 0)
                filters.Add("applicationIds=" + String.Join(",", ApplicationIds));

            if (LogLevels != null && LogLevels.Count > 0)
                filters.Add("logLevels=" + String.Join(",", LogLevels));

            if (Expand != null && Expand.Count > 0)
                filters.Add("expand=" + String.Join(",", Expand));

            if (StartDate != null)
                filters.Add("startDate=" + MicrosecondDateTimeConverter.ConvertFromDateTime(StartDate));

            if (EndDate != null)
                filters.Add("endDate=" + MicrosecondDateTimeConverter.ConvertFromDateTime(EndDate));

            if (Severities != null && Severities.Count > 0)
                filters.Add("severities=" + String.Join(",", Severities));

            if (!String.IsNullOrEmpty(Status))
                filters.Add("status=" + Status);

            if (!String.IsNullOrEmpty(Sort))
                filters.Add("sort=" + Sort);

            if (Limit > -1)
                filters.Add("limit=" + Limit);

            if (Offset > -1)
                filters.Add("offset=" + Offset);

            if (filters.Count > 0)
                return "?" + String.Join("&", filters);
            else
                return "";
        }
    }

    public enum ServerExpandValue
    {
        applications,
        num_apps
    }
}
