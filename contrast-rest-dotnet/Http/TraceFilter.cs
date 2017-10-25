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
    public class TraceFilter
    {
        /// <summary>
        /// Filte text.
        /// </summary>
        public string FilterText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> FilterTags { get; set; }
        public List<RuleSeverity> Severities { get; set; }
        public List<string> Status { get; set; }
        public List<string> VulnTypes { get; set; }
        public List<string> AppVersionTags { get; set; }
        public List<long> ServerIds { get; set; }
        /// <summary>
        /// Server environments.
        /// </summary>
        public List<ServerEnvironment> Environments { get; set; }
        public List<string> Urls { get; set; }
        public List<string> Modules { get; set; }
        /// <summary>
        /// Load additional data. Allowed values: card, events, notes, request, application.
        /// </summary>
        public List<TraceExpandValue> Expand { get; set; }
        /// <summary>
        /// Limit the number of traces to receive.
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// Sort by. Allowed values: lastTimeSeen, status, title, application, name, severity.
        /// </summary>
        public string Sort { get; set; }

        public TraceFilter()
        {
            FilterText = "";
            StartDate = null;
            EndDate = null;
            FilterTags = null;
            Severities = null;
            Status = null;
            VulnTypes = null;
            AppVersionTags = null;
            ServerIds = null;
            Environments = null;
            Urls = null;
            Modules = null;
            Expand = null;
            Limit = -1;
            Offset = -1;
            Sort = "";
        }

        public override string ToString()
        {
            List<string> filters = new List<string>();

            if (!String.IsNullOrEmpty(FilterText))
                filters.Add(FilterText);

            if (Expand != null && Expand.Count > 0)
                filters.Add("expand=" + String.Join(",", Expand));

            if (StartDate != null)
                filters.Add("startDate=" + MicrosecondDateTimeConverter.ConvertFromDateTime(StartDate));

            if(EndDate != null)
                filters.Add("endDate=" + MicrosecondDateTimeConverter.ConvertFromDateTime(EndDate));

            if (FilterTags != null && FilterTags.Count > 0)
                filters.Add("filterTags=" + String.Join(",", FilterTags));

            if (Severities != null && Severities.Count > 0)
                filters.Add("severities=" + String.Join(",", Severities));

            if (Status != null && Status.Count > 0)
                filters.Add("status=" + String.Join(",", Status));

            if (VulnTypes != null && VulnTypes.Count > 0)
                filters.Add("vulnTypes=" + String.Join(",", VulnTypes));

            if (AppVersionTags != null && AppVersionTags.Count > 0)
                filters.Add("appVersionTags=" + String.Join(",", AppVersionTags));

            if (Environments != null && Environments.Count > 0)
                filters.Add("environments=" + String.Join(",", Environments));

            if (ServerIds != null && ServerIds.Count > 0)
                filters.Add("servers=" + String.Join(",", ServerIds));

            if (Urls != null && Urls.Count > 0)
                filters.Add("urls=" + String.Join(",", Urls));

            if (Modules != null && Modules.Count > 0)
                filters.Add("modules=" + String.Join(",", Modules));

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

    public enum RuleSeverity
    {
        Note,
        Low,
        Medium,
        High,
        Critical
    }

    public enum ServerEnvironment
    {
        Development,
        QA,
        Production
    }

    public enum ApplicationExpandValues
    {
        scores,
        trace_breakdown,
        license
    }

    public enum LibrariesExpandValues
    {
        vulns
    }

    public enum TraceExpandValue
    {
        card,
        events,
        notes,
        request,
        application,
        servers
    }
}
