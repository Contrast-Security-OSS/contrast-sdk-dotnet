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

namespace contrast_rest_dotnet
{
    internal static class NgEndpoints
    {
        internal static string APPLICATIONS = "api/ng/{0}/applications/{1}";
        internal static string APPLICATION_LIBRARIES = "api/ng/{0}/applications/{1}/libraries";
        internal static string APPLICATION_TRACES = "api/ng/{0}/traces/{1}/filter";
        internal static string DEFAULT_ORGANIZATION = "api/ng/profile/organizations/default";
        internal static string ENGINE_DOTNET = "api/api/{0}/engine/{1}/dotnet";
        internal static string ENGINE_JAVA1_5 = "api/ng/{0}/agent/{1}/java?jvm=1_5";
        internal static string ENGINE_JAVA = "api/ng/{0}/agent/{1}/java?jvm=1_6";
        internal static string ENGINE_NODE = "api/ng/{0}/agent/{1}/node";
        internal static string ORGANIZATIONS = "api/ng/profile/organizations/";
        internal static string ORGANIZATION_TRACES = "api/ng/{0}/orgtraces/filter";
        internal static string SERVERS = "api/ng/{0}/servers/{1}";
        internal static string SERVER_TRACES = "api/ng/{0}/servertraces/{1}/filter";
        internal static string TRACE = "api/ng/{0}/traces/{1}";
        internal static string TRACE_EVENTS_SUMMARY = "api/ng/{0}/traces/{1}/events/summary";
        internal static string TRACE_EVENT_DETAIL = "api/ng/{0}/traces/{1}/events/{2}/details";
        internal static string TRACE_HTTP_REQUEST = "api/ng/{0}/traces/{1}/httprequest";
        internal static string TRACE_STORY = "api/ng/{0}/traces/{1}/story";
        internal static string TRACE_FILTERS = "api/ng/{0}/orgtraces/filter/{1}/listing";
        internal static string APPLICATION_TRACE_FILTERS = "api/ng/{0}/traces/{1}/filter/{2}/listing";
        internal static string SERVER_TRACE_FILTERS = "api/ng/{0}/servertraces/{1}/filter/{2}/listing";
    }
}
