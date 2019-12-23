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

using Contrast.Http;
using Contrast.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;

namespace Contrast
{
    public enum RequestMethod
    {
        Post,
        Put,
        Delete
    }

    /// <summary>
    /// Entry point for using the Contrast REST API.  Make an instance of this class and call methods.
    /// </summary>
    public class Client : IDisposable
    {
        private IContrastRestClient _contrastRestClient;

        private const string DEFAULT_AGENT_PROFILE = "default";
        private const int POST_METHOD = 1;
        private const int PUT_METHOD = 2;
        private const int DELETE_METHOD = 3;

        /// <summary>
        /// Creates the client that will interact with TeamServer. 
        /// </summary>
        /// <param name="user">Username (e.g., joe@acme.com)</param>
        /// <param name="serviceKey">User service key</param>
        /// <param name="apiKey">API Key</param>
        /// <param name="teamServerUrl">he base Contrast API URL (e.g., https://app.contrastsecurity.com/Contrast/api/)</param>
        /// <exception cref="System.ArgumentException">Thrown when an invalid Uri is passed in teamServerUrl or a null/empty value is provided for other parameters</exception>
        public Client(string user, string serviceKey, string apiKey, string teamServerUrl)
            : this(new ContrastRestClient(new HttpClientWrapper(user, serviceKey, apiKey, teamServerUrl)))
        { }

        public Client(IContrastRestClient contrastRestClient)
        {
            _contrastRestClient = contrastRestClient;
        }

        private T GetResponseAndDeserialize<T>(string endpoint)
        {
            Stream responseStream = null;
            try
            {
                responseStream = _contrastRestClient.GetResponseStream(endpoint);
                using (JsonTextReader textReader = new JsonTextReader(new StreamReader(responseStream)))
                {
                    responseStream = null;
                    JsonSerializer deserializer = new JsonSerializer();
                    return (T)deserializer.Deserialize(textReader, typeof(T));
                }
            }
            finally
            {
                if(responseStream != null)
                    responseStream.Dispose();
            }
        }

        private T GetResponseAndDeserialize<T>(string endpoint, string requestBody, RequestMethod method)
        {
            Stream responseStream = null;
            try
            {
                HttpResponseMessage response;

                switch (method)
                {
                    case RequestMethod.Put:
                        response = _contrastRestClient.PutMessage(endpoint, requestBody, null);
                        break;
                    case RequestMethod.Delete:
                        if(String.IsNullOrWhiteSpace(requestBody))
                            response = _contrastRestClient.DeleteMessage(endpoint);
                        else
                            response = _contrastRestClient.DeleteMessage(endpoint, requestBody);
                        break;
                    case RequestMethod.Post:
                    default:
                        response = _contrastRestClient.PostMessage(endpoint, requestBody, null);
                        break;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new ForbiddenException(
                        String.Format("Current use doesn't have enough authority to perform action for resource: '{0}'",
                            endpoint));
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new ResourceNotFoundException("Resource: '" + endpoint + "' not found.");

                responseStream = response.Content.ReadAsStreamAsync().Result;

                using (JsonTextReader textReader = new JsonTextReader(new StreamReader(responseStream)))
                {
                    responseStream = null;
                    JsonSerializer deserializer = new JsonSerializer();
                    return (T)deserializer.Deserialize(textReader, typeof(T));
                }
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Dispose();
            }
        }

        private T GetDeleteResponseAndDeserialize<T>(string endpoint)
        {
            return GetResponseAndDeserialize<T>(endpoint, null, RequestMethod.Delete);
        }

        /// <summary>
        /// Download a contrast agent associated with this account. The .NET agent should
        /// be saved to a contrast.zip file. The Java agent should be saved to contrast.jar.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="agentType">the type of agent to download (.NET or Java)</param>
        /// <returns>a Stream of the agent file contents which should be saved using the appropriate filetype</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public Stream GetAgent(AgentType agentType, string organizationId)
        {
            return GetAgent(agentType, organizationId, DEFAULT_AGENT_PROFILE);
        }

        /// <summary>
        /// Download a contrast agent associated with this account. The .NET agent should
        /// be saved to a contrast.zip file. The Java agent should be saved to contrast.jar.
        /// This signature takes a parameter which contains the name of the saved engine profile
        /// to download.
        /// </summary>
        /// <param name="agentType">the type of agent to download (.NET or Java)</param>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="profileName">the name of the saved engine profile to download</param>
        /// <returns>a Stream of the agent file contents which should be saved using the appropriate filetype</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public Stream GetAgent(AgentType agentType, string organizationId, string profileName)
        {
            string agentEndpoint = null;

            switch (agentType)
            {
                case AgentType.DotNet:
                    agentEndpoint = string.Format(NgEndpoints.ENGINE_DOTNET, organizationId, profileName);
                    break;
                case AgentType.Java:
                    agentEndpoint = string.Format(NgEndpoints.ENGINE_JAVA, organizationId, profileName);
                    break;
                case AgentType.Java1_5:
                    agentEndpoint = string.Format(NgEndpoints.ENGINE_JAVA1_5, organizationId, profileName);
                    break;
                case AgentType.Node:
                    agentEndpoint = string.Format(NgEndpoints.ENGINE_NODE, organizationId, profileName);
                    break;
            }

            return _contrastRestClient.GetResponseStream(agentEndpoint);
        }

        /// <summary>
        /// Get summary information about a single application.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <returns>a ContrastApplication object for the appId supplied</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public ApplicationResponse GetApplication(string organizationId, string appId)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATIONS, organizationId, appId);
            return GetResponseAndDeserialize<ApplicationResponse>(endpoint);
        }

        /// <summary>
        /// Get the list of applications being monitored by Contrast.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <returns>A response object which contains a list of ContrastApplication objects that are being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public ApplicationsResponse GetApplications(string organizationId)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATIONS, organizationId, string.Empty);
            return (GetResponseAndDeserialize<ApplicationsResponse>(endpoint));
        }

        /// <summary>
        /// Resets an application's library, coverage, statistics and trace information.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public void ResetApplication(string organizationId, string appId)
        {
            string endpoint = string.Format(NgEndpoints.RESET_APPLICATION, organizationId, appId);
            _contrastRestClient.PutMessage(endpoint, "{}", null);
        }

        /// <summary>
        /// Return the libraries of the monitored Contrast application.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <returns>A response object which contains a list of Library objects for the given app</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public LibraryResponse GetLibraries(string organizationId, string appId)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATION_LIBRARIES, organizationId, appId);
            return (GetResponseAndDeserialize<LibraryResponse>(endpoint));
        }

        /// <summary>
        /// Return a single agent profile object
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="profileName">the agent profile name</param>
        /// <returns>A Profile response object that contains the requested agent profile.</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public ProfileResponse GetProfile(string organizationId, string profileName)
        {
            string endpoint = String.Format(NgEndpoints.PROFILE, organizationId, profileName);
            return GetResponseAndDeserialize<ProfileResponse>(endpoint);
        }

        /// <summary>
        /// Return the profiles setup in TeamServer for Contrast Agents.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <returns>A Profiles response object that contains the list of agent profiles found.</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public ProfilesResponse GetProfiles(string organizationId)
        {
            string endpoint = String.Format(NgEndpoints.PROFILES, organizationId);
            return (GetResponseAndDeserialize<ProfilesResponse>(endpoint));
        }

        /// <summary>
        /// Return the servers monitored by Contrast agents.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="serverId">the ID of the server</param>
        /// <returns>a ServerResponse object for the ID supplied</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public ServerResponse GetServer(string organizationId, long serverId)
        {
            string endpoint = String.Format(NgEndpoints.SERVERS, organizationId, serverId);
            return GetResponseAndDeserialize<ServerResponse>(endpoint);
        }

        /// <summary>
        /// Return the servers monitored by Contrast agents.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <returns>A ServerResponse object which contains a list of Server objects being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public ServersResponse GetServers(string organizationId)
        {
            return GetServers(organizationId, null);
        }

        /// <summary>
        /// Return the servers monitored by Contrast agents.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="filter">Query params that can be added to filter request.</param>
        /// <returns>A ServerResponse object which contains a list of Server objects being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public ServersResponse GetServers(string organizationId, ServerFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.SERVERS, organizationId, string.Empty);
            if (filter != null)
                endpoint += filter.ToString();

            return (GetResponseAndDeserialize<ServersResponse>(endpoint));
        }

        /// <summary>
        /// Search a trace by trace uuid coincidence.
        /// </summary>
        /// <param name="organizationId">User's organization UUID.</param>
        /// <param name="traceUuid">Trace UUID.</param>
        /// <returns>A TraceSearchResponse object which contains a list of all the traces with the given UUID.</returns>
        public TracesSearchResponse GetTracesByUuid(string organizationId, string traceUuid)
        {
            string endpoit = String.Format(NgEndpoints.TRACE, organizationId, traceUuid);
            return GetResponseAndDeserialize<TracesSearchResponse>(endpoit);
        }

        /// <summary>
        /// Returns a list of traces from a certain organization. Other filtering options are available
        /// through the use of TraceFilter params.
        /// </summary>
        /// <param name="organizationId">Organization from which the traces will be retrieved.</param>
        /// <returns>A TraceFilterResponse object with the List of Trace objects.</returns>
        public TraceFilterResponse GetTraces(string organizationId)
        {
            return GetTraces(organizationId, null);
        }

        /// <summary>
        /// Returns a list of traces from a certain organization. Other filtering options are available
        /// through the use of TraceFilter params.
        /// </summary>
        /// <param name="organizationId">Organization from which the traces will be retrieved.</param>
        /// <param name="filter">Query params that can be added to request.</param>
        /// <returns>A TraceFilterResponse object with the List of Trace objects.</returns>
        public TraceFilterResponse GetTraces(string organizationId, TraceFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.ORGANIZATION_TRACES, organizationId);
            if (filter != null)
                endpoint += filter.ToString();

            return (GetResponseAndDeserialize<TraceFilterResponse>(endpoint));
        }

        /// <summary>
        /// Get the vulnerabilities in the application for the ID supplied.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <returns>A TraceFilterResponse object with the List of Trace objects.</returns>
        public TraceFilterResponse GetApplicationTraces(string organizationId, string appId)
        {
            return GetApplicationTraces(organizationId, appId, null);
        }

        /// <summary>
        /// Get the vulnerabilities in the application for the ID supplied.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <param name="filter">Query params that can be added to request.</param>
        /// <returns>A TraceFilterResponse object with the List of Trace objects.</returns>
        public TraceFilterResponse GetApplicationTraces(string organizationId, string appId, TraceFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATION_TRACES, organizationId, appId);
            if (filter != null)
                endpoint += filter.ToString();

            return (GetResponseAndDeserialize<TraceFilterResponse>(endpoint));
        }

        /// <summary>
        /// Get the vulnerabilities in the Server for the ID supplied.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization.</param>
        /// <param name="serverId">The ID of the server.</param>
        /// <returns>A TraceFilterResponse object with the List of Trace objects.</returns>
        public TraceFilterResponse GetServerTraces(string organizationId, long serverId)
        {
            return GetServerTraces(organizationId, serverId, null);
        }

        /// <summary>
        /// Get the vulnerabilities in the Server for the ID supplied.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization.</param>
        /// <param name="serverId">The ID of the server.</param>
        /// <param name="filter">Query params that can be added to request.</param>
        /// <returns>A TraceFilterResponse object with the List of Trace objects.</returns>
        public TraceFilterResponse GetServerTraces(string organizationId, long serverId, TraceFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.SERVER_TRACES, organizationId, serverId);
            if (filter != null)
                endpoint += filter.ToString();
            return GetResponseAndDeserialize<TraceFilterResponse>(endpoint);
        }

        /// <summary>
        /// Get the organizations associated with the API user
        /// </summary>
        /// <returns>a List of Organization objects representing the organizations</returns>
        public List<Organization> GetOrganizations()
        {
            var organizationResponse = GetResponseAndDeserialize<OrganizationResponse>(NgEndpoints.ORGANIZATIONS);
            return organizationResponse.organizations;
        }

        /// <summary>
        /// Get the organizations associated with the API user in non EAC environments.
        /// </summary>
        /// <returns>a List of Organization objects representing the organizations</returns>
        public Organization GetDefaultOrganization()
        {
            string endpoint = NgEndpoints.DEFAULT_ORGANIZATION;
            var response = GetResponseAndDeserialize<DefaultOrganizationResponse>(endpoint);
            return response.organization;
        }

        /// <summary>
        /// Get a organization information based on its UUID.
        /// </summary>
        /// <param name="organizationId">Organization UUID</param>
        /// <returns>A object that contains basic organization data</returns>
        public OrganizationManagedResponse GetOrganizationInfo(string organizationId)
        {
            return GetOrganizationInfo(organizationId, null);
        }

        /// <summary>
        /// Get a organization information based on its UUID.
        /// </summary>
        /// <param name="organizationId">Organization UUID</param>
        /// <param name="expand">Load additional data</param>
        /// <returns>A object that contains basic organization data</returns>
        public OrganizationManagedResponse GetOrganizationInfo(string organizationId, List<OrganizationExpandValues> expand)
        {
            string endpoint = String.Format(NgEndpoints.ORGANIZATION_INFORMATION, organizationId);
            if (expand?.Count > 0)
            {
                endpoint += "?expand=" + String.Join(",", expand);
            }
            return GetResponseAndDeserialize<OrganizationManagedResponse>(endpoint);
        }

        /// <summary>
        /// Gets a trace list of events with their summary
        /// </summary>
        /// <param name="organizationId">User's organization UUID</param>
        /// <param name="traceUuid">Trace UUID.</param>
        /// <returns>A response object containing the list of events summary.</returns>
        public TraceEventSummaryResponse GetEventsSummary(string organizationId, string traceUuid)
        {
            string endpoint = String.Format(NgEndpoints.TRACE_EVENTS_SUMMARY, organizationId, traceUuid);
            return GetResponseAndDeserialize<TraceEventSummaryResponse>(endpoint);
        }

        /// <summary>
        /// Get trace recommendation from a trace
        /// </summary>
        /// <param name="organizationId">Organization UUID</param>
        /// <param name="traceUuid">Trace UUID</param>
        /// <returns>A response which contains a recommendation details for the trace.</returns>
        public TraceRecommendationResponse GetTraceRecommendation(string organizationId, string traceUuid)
        {
            string endpoint = String.Format(NgEndpoints.TRACE_RECOMMENDATION, organizationId, traceUuid);
            return GetResponseAndDeserialize<TraceRecommendationResponse>(endpoint);
        }
        
        /// <summary>
        /// Gets the details for the indicated trace event.
        /// </summary>
        /// <param name="organizationId">User's organization UUID</param>
        /// <param name="traceUuid">Trace UUID></param>
        /// <param name="eventId">Trace event Id.</param>
        /// <returns>A response object that contains the event data.</returns>
        public TraceEventDetailResponse GetTraceEventDetail(string organizationId, string traceUuid, long eventId)
        {
            string endpoint = String.Format(NgEndpoints.TRACE_EVENT_DETAIL, organizationId, traceUuid, eventId);
            return GetResponseAndDeserialize<TraceEventDetailResponse>(endpoint);
        }
        
        /// <summary>
        /// Retrieves a trace story.
        /// </summary>
        /// <param name="organizationId">User's organization UUID.</param>
        /// <param name="traceUuid">Trace UUID.</param>
        /// <returns>A response object that contains the trace story.</returns>
        public TraceStoryResponse GetTraceStory(string organizationId, string traceUuid)
        {
            string endpoint = String.Format(NgEndpoints.TRACE_STORY, organizationId, traceUuid);
            return GetResponseAndDeserialize<TraceStoryResponse>(endpoint);
        }

        /// <summary>
        /// Retrieves the trace request text with mustache format.
        /// </summary>
        /// <param name="organizationId">User's organization UUID</param>
        /// <param name="traceUuid">Trace UUID></param>
        /// <returns>A response cont</returns>
        public TraceRequestResponse GetTraceHttpRequest(string organizationId, string traceUuid)
        {
            string endpoint = String.Format(NgEndpoints.TRACE_HTTP_REQUEST, organizationId, traceUuid);
            return GetResponseAndDeserialize<TraceRequestResponse>(endpoint);
        }

        private string getTraceFilterTypeValue(TraceFilterType type)
        {
            switch(type)
            {
                case TraceFilterType.serversEnvironment:
                    return "servers-environment";
                case TraceFilterType.securityStandard:
                    return "security-standard";
                default:
                    return type.ToString();
            }
        }

        /// <summary>
        /// Gets subfilters for a filter.
        /// </summary>
        /// <param name="organizationId">Organization UUID</param>
        /// <param name="type">Filter type. Allowed values: modules,workflow,servers,time,url,vulntype,status,severity,security-standard.</param>
        /// <param name="filter">Query params that can be added to request.</param>
        /// <returns>A TraceFilterCatalogDetailsResponse that contains all the available subfilters.</returns>
        public TraceFilterCatalogDetailsResponse GetTraceFilterSubfilters(string organizationId, TraceFilterType type, TraceFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.TRACE_FILTERS, organizationId, getTraceFilterTypeValue(type));
            if (filter != null)
                endpoint += filter.ToString();
            return GetResponseAndDeserialize<TraceFilterCatalogDetailsResponse>(endpoint);
        }

        /// <summary>
        /// Gets subfilters for a filter.
        /// </summary>
        /// <param name="organizationId">Organization UUID</param>
        /// <param name="appId">Application UUID</param>
        /// <param name="type">Filter type. Allowed values: appversiontags,workflow,servers,time,url,vulntype,servers,security-standard.</param>
        /// <param name="filter">Query params that can be added to request.</param>
        /// <returns>A TraceFilterCatalogDetailsResponse that contains all the available subfilters.</returns>
        public TraceFilterCatalogDetailsResponse GetApplicationTraceFilterSubfilters(string organizationId, string appId, TraceFilterType type, TraceFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATION_TRACE_FILTERS, organizationId, appId, getTraceFilterTypeValue(type));
            if (filter != null)
                endpoint += filter.ToString();
            return GetResponseAndDeserialize<TraceFilterCatalogDetailsResponse>(endpoint);
        }

        /// <summary>
        /// Gets subfilters for a filter.
        /// </summary>
        /// <param name="organizationId">Organization UUID</param>
        /// <param name="serverId">Server ID</param>
        /// <param name="type">Filter type. Allowed values: modules,workflow,servers,time,url,vulntype,security-standard.</param>
        /// <param name="filter">Query params that can be added to request.</param>
        /// <returns>A TraceFilterCatalogDetailsResponse that contains all the available subfilters.</returns>
        public TraceFilterCatalogDetailsResponse GetServerTraceFilterSubfilters(string organizationId, long serverId, TraceFilterType type, TraceFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.SERVER_TRACE_FILTERS, organizationId, serverId, getTraceFilterTypeValue(type));
            if (filter != null)
                endpoint += filter.ToString();
            return GetResponseAndDeserialize<TraceFilterCatalogDetailsResponse>(endpoint);
        }

        /// <summary>
        /// Remove tag from trace.
        /// </summary>
        /// <param name="organizationId">Organization UUID.</param>
        /// <param name="traceUuid">Trace UUID.</param>
        /// <param name="tag">The tag to be deleted.</param>
        /// <returns>A TagsResponse object which indicates wheter the operation was successful or not.</returns>
        public TagsResponse DeleteTraceTag(string organizationId, string traceUuid, string tag)
        {
            string endpoint = String.Format(NgEndpoints.DELETE_TRACE_TAG, organizationId, traceUuid);
            TagRequest request = new TagRequest();
            request.Tag = tag;

            return GetResponseAndDeserialize<TagsResponse>(endpoint, JsonConvert.SerializeObject(request), RequestMethod.Delete);
        }

        /// <summary>
        /// Get all unique trace tags by organization.
        /// </summary>
        /// <param name="organizationId">Organization UUID</param>
        /// <returns>A response with all unique tags found.</returns>
        public TagsResponse GetTracesUniqueTags(string organizationId)
        {
            string endpoint = String.Format(NgEndpoints.TRACES_TAGS, organizationId);
            return GetResponseAndDeserialize<TagsResponse>(endpoint);
        }

        /// <summary>
        /// Get all unique trace tags by server.
        /// </summary>
        /// <param name="organizationId">Organization UUID</param>
        /// <param name="serverId">Server Id.</param>
        /// <returns>A response with all unique tags found.</returns>
        public TagsResponse GetTracesUniqueTags(string organizationId, long serverId)
        {
            string endpoint = String.Format(NgEndpoints.SERVER_TRACE_TAGS, organizationId, serverId);
            return GetResponseAndDeserialize<TagsResponse>(endpoint);
        }

        /// <summary>
        /// Get all unique trace tags by application.
        /// </summary>
        /// <param name="organizationId">Organization UUID</param>
        /// <param name="appId">Application UUID.</param>
        /// <returns>A response with all unique tags found.</returns>
        public TagsResponse GetTracesUniqueTags(string organizationId, string appId)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATION_TRACE_TAGS, organizationId, appId);
            return GetResponseAndDeserialize<TagsResponse>(endpoint);
        }

        /// <summary>
        /// Tag traces.
        /// </summary>
        /// <param name="organizationId">Organization UUID.</param>
        /// <param name="requestBody">A TagsServerResource object with a list of tags and the traces to be tagged.</param>
        /// <returns>A base response to indicate success of the operation.</returns>
        public BaseApiResponse TagTraces(string organizationId, TagsServersResource requestBody)
        {
            string endpoint = String.Format(NgEndpoints.TRACES_TAGS, organizationId);
            return GetResponseAndDeserialize<BaseApiResponse>(endpoint, JsonConvert.SerializeObject(requestBody), RequestMethod.Put);
        }

        /// <summary>
        /// Get all tags by traces.
        /// </summary>
        /// <param name="organizationUuid">Organization UUID.</param>
        /// <param name="requestBody">A TagsTraceRequest object with a list of traces UUIDs.</param>
        /// <returns>A response with all the tags found.</returns>
        public TagsResponse GetTagsByTraces(string organizationUuid, TagsTraceRequest requestBody)
        {
            string endpoint = String.Format(NgEndpoints.TRACES_TAG_BULK, organizationUuid);
            return GetResponseAndDeserialize<TagsResponse>(endpoint, JsonConvert.SerializeObject(requestBody), RequestMethod.Post);
        }

        /// <summary>
        /// Tag traces bulk
        /// </summary>
        /// <param name="organizationId">Organization UUID.</param>
        /// <param name="requestBody">A TagsTracesUpdateRequest object with a list of tags and the list of traces to be tagged.</param>
        /// <returns>A base response to indicate success of the operation.</returns>
        public BaseApiResponse TagsTracesBulk(string organizationId, TagsTracesUpdateRequest requestBody)
        {
            string endpoint = String.Format(NgEndpoints.TRACES_TAG_BULK, organizationId);
            return GetResponseAndDeserialize<BaseApiResponse>(endpoint, JsonConvert.SerializeObject(requestBody), RequestMethod.Put);
        }

        /// <summary>
        /// Get all tags by trace
        /// </summary>
        /// <param name="organizationId">Organization UUID.</param>
        /// <param name="traceUuid">Trace UUID.</param>
        /// <returns>A TagsResponse object with the list of tags found.</returns>
        public TagsResponse GetTagsByTrace(string organizationId, string traceUuid)
        {
            string endpoint = String.Format(NgEndpoints.TRACE_TAGS, organizationId, traceUuid);
            return GetResponseAndDeserialize<TagsResponse>(endpoint);
        }

        /// <summary>
        /// Updates status for a list of traces.
        /// </summary>
        /// <param name="organizationId">Organization UUID.</param>
        /// <param name="requestBody">Request object that contains status, notes and list of traces id.</param>
        /// <returns>A base response to indicate success of the operation.</returns>
        public BaseApiResponse MarkTraceStatus(string organizationId, TraceMarkStatusRequest requestBody)
        {
            string endpoint = String.Format(NgEndpoints.TRACE_MARK_STATUS, organizationId);
            return GetResponseAndDeserialize<BaseApiResponse>(endpoint, JsonConvert.SerializeObject(requestBody), RequestMethod.Put);
        }

        /// <summary>
        /// Updates status for a list of traces on a server.
        /// </summary>
        /// <param name="organizationId">Organization UUID.</param>
        /// <param name="serverId">Server Id.</param>
        /// <param name="requestBody">Request object that contains status, notes and list of traces id.</param>
        /// <returns>A base response to indicate success of the operation.</returns>
        public BaseApiResponse MarkTraceStatus(string organizationId, long serverId, TraceMarkStatusRequest requestBody)
        {
            string endpoint = String.Format(NgEndpoints.SERVER_TRACE_MARK_STATUS, organizationId, serverId);
            return GetResponseAndDeserialize<BaseApiResponse>(endpoint, JsonConvert.SerializeObject(requestBody), RequestMethod.Put);
        }

        /// <summary>
        /// Updates status for a list of traces on an application.
        /// </summary>
        /// <param name="organizationId">Organization UUID.</param>
        /// <param name="appId">Application UUID.</param>
        /// <param name="requestBody">Request object that contains status, notes and list of traces id.</param>
        /// <returns>A base response to indicate success of the operation.</returns>
        public BaseApiResponse MarkTraceStatus(string organizationId, string appId, TraceMarkStatusRequest requestBody)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATION_TRACE_MARK_STATUS, organizationId, appId);
            return GetResponseAndDeserialize<BaseApiResponse>(endpoint, JsonConvert.SerializeObject(requestBody), RequestMethod.Put);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_contrastRestClient != null)
                {
                    _contrastRestClient.Dispose();
                    _contrastRestClient = null;
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
