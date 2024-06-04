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

namespace Contrast
{
    internal static class NgEndpoints
    {
        #region "tags"
        internal static string APPLICATION_TRACE_TAGS = "api/ng/{0}/tags/traces/application/{1}";
        internal static string SERVER_TAGS = "api/ng/{0}/";
        internal static string SERVER_TRACE_TAGS = "api/ng/{0}/tags/traces/server/{1}";
        internal static string TRACE_TAGS = "api/ng/{0}/tags/traces/trace/{1}";
        internal static string DELETE_TRACE_TAG = "api/ng/{0}/tags/trace/{1}";
        internal static string TRACES_TAGS = "api/ng/{0}/tags/traces";
        internal static string TRACES_TAG_BULK = "api/ng/{0}/tags/traces/bulk";

        internal static string TAGS_APPLICATION = "/ng/{orgUuid}/tags/application/{appId}";
        internal static string TAGS_APPLICATION_LIST = "api/ng/{0}/tags/application/list/{1}"; // GET
        internal static string TAGS_APPLICATION_DELETE = "api/ng/{0}/tags/application/{1}"; // DELETE
        internal static string TAGS_APPLICATIONS = "api/ng/{0}/tags/applications"; // PUT
        internal static string TAGS_APPLICATIONS_BULK_APP = "api/ng/{0}/tags/applications/bulk?applicationsId={1}"; // GET
        internal static string TAGS_APPLICATIONS_BULK = "api/ng/{0}/tags/applications/bulk"; // PUT
        internal static string TAGS_APPLICATIONS_LIST = "/ng/{0}/tags/applications/list"; // GET


        internal static string TAGS_ATTACK_EVENT_LIST = "/ng/{orgUuid}/tags/attack/event/list/{eventUuid}";
        internal static string TAGS_ATTACK_EVENT = "/ng/{orgUuid}/tags/attack/event/{eventUuid}";
        internal static string TAGS_ATTACK_EVENTS = "/ng/{orgUuid}/tags/attack/events";
        internal static string TAGS_ATTACK_EVENTS_BULK = "/ng/{orgUuid}/tags/attack/events/bulk";
        internal static string TAGS_ATTACK_EVENTS_LIST = "/ng/{orgUuid}/tags/attack/events/list";
        internal static string TAGS_ATTACK_LIST = "/ng/{orgUuid}/tags/attack/list/{attackUuid}";
        internal static string TAGS_ATTACK = "/ng/{orgUuid}/tags/attack/{attackUuid}";
        internal static string TAGS_ATTACKS = "/ng/{orgUuid}/tags/attacks";
        internal static string TAGS_ATTACKS_BULK = "/ng/{orgUuid}/tags/attacks/bulk";
        internal static string TAGS_ATTACKS_LIST = "/ng/{orgUuid}/tags/attacks/list";
        internal static string TAGS_LIBRARIES = "/ng/{orgUuid}/tags/libraries";
        internal static string TAGS_LIBRARIES_BULK = "/ng/{orgUuid}/tags/libraries/bulk";
        internal static string TAGS_LIBRARIES_LIST = "/ng/{orgUuid}/tags/libraries/list";
        internal static string TAGS_LIBRARIES1 = "/ng/{orgUuid}/tags/libraries/{appId}/list";
        internal static string TAGS_LIBRARY = "/ng/{orgUuid}/tags/library/{hash}";
        internal static string TAGS_SERVER_LIST = "/ng/{orgUuid}/tags/server/list/{serverId}";
        internal static string TAGS_SERVER = "/ng/{orgUuid}/tags/server/{serverId}";
        internal static string TAGS_SERVERS = "/ng/{orgUuid}/tags/servers";
        internal static string TAGS_SERVERS_BULK = "/ng/{orgUuid}/tags/servers/bulk";
        internal static string TAGS_SERVERS_LIST = "/ng/{orgUuid}/tags/servers/list";
        internal static string TAGS_SERVERS_LIST_APPLICATION = "/ng/{orgUuid}/tags/servers/list/application/{appId}";
        internal static string TAGS_TRACE = "/ng/{orgUuid}/tags/trace/{traceUuid}";
        internal static string TAGS_TRACES = "/ng/{orgUuid}/tags/traces";
        internal static string TAGS_TRACES_APPLICATION = "/ng/{orgUuid}/tags/traces/application/{appId}";
        internal static string TAGS_TRACES_BULK = "/ng/{orgUuid}/tags/traces/bulk";
        internal static string TAGS_TRACES_SERVER = "/ng/{orgUuid}/tags/traces/server/{serverId}";
        internal static string TAGS_TRACES_TRACE = "/ng/{orgUuid}/tags/traces/trace/{traceUuid}";

        #endregion
        internal static string APPLICATIONS = "api/ng/{0}/applications/{1}";
        internal static string APPLICATION_LIBRARIES = "api/ng/{0}/applications/{1}/libraries";
        internal static string APPLICATION_SERVERS = "api/ng/{0}/applications/{1}/servers";
        internal static string APPLICATION_TRACES = "api/ng/{0}/traces/{1}/filter";
        internal static string APPLICATION_TRACE_MARK_STATUS = "api/ng/{0}/traces/{1}/mark";
        internal static string RESET_APPLICATION = "api/ng/{0}/applications/{1}/reset";
        internal static string DEFAULT_ORGANIZATION = "api/ng/profile/organizations/default";
        internal static string ENGINE_DOTNET = "api/ng/{0}/agents/{1}/dotnet";
        internal static string ENGINE_JAVA1_5 = "api/ng/{0}/agents/{1}/java?jvm=1_5";
        internal static string ENGINE_JAVA = "api/ng/{0}/agents/{1}/java?jvm=1_6";
        internal static string ENGINE_NODE = "api/ng/{0}/agents/{1}/node";
        internal static string ORGANIZATIONS = "api/ng/profile/organizations/";
        internal static string ORGANIZATION_TRACES = "api/ng/{0}/orgtraces/filter";
        internal static string ORGANIZATION_INFORMATION = "api/ng/{0}/organizations";
        internal static string PROFILE = "api/ng/{0}/agents/profiles/{1}";
        internal static string PROFILES = "api/ng/{0}/agents/profiles";
        internal static string SERVERS = "api/ng/{0}/servers/{1}";
        internal static string SERVER_TRACES = "api/ng/{0}/servertraces/{1}/filter";
        internal static string SERVER_TRACE_MARK_STATUS = "api/ng/{0}/servertraces/{1}/mark";
        internal static string TRACE = "api/ng/{0}/traces/{1}";
        internal static string TRACE_EVENTS_SUMMARY = "api/ng/{0}/traces/{1}/events/summary";
        internal static string TRACE_EVENT_DETAIL = "api/ng/{0}/traces/{1}/events/{2}/details";
        internal static string TRACE_HTTP_REQUEST = "api/ng/{0}/traces/{1}/httprequest";
        internal static string TRACE_STORY = "api/ng/{0}/traces/{1}/story";
        internal static string TRACE_RECOMMENDATION = "api/ng/{0}/traces/{1}/recommendation";//Aka how to fix
        internal static string TRACE_FILTERS = "api/ng/{0}/orgtraces/filter/{1}/listing";
        internal static string APPLICATION_TRACE_FILTERS = "api/ng/{0}/traces/{1}/filter/{2}/listing";
        internal static string SERVER_TRACE_FILTERS = "api/ng/{0}/servertraces/{1}/filter/{2}/listing";
        internal static string TRACE_MARK_STATUS = "api/ng/{0}/orgtraces/mark";
        internal static string MODULES = "api/ng/{0}/modules/{1}";

    }
}
