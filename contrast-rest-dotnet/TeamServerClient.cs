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

using contrast_rest_dotnet.Http;
using contrast_rest_dotnet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace contrast_rest_dotnet
{

    /// <summary>
    /// Entry point for using the Contrast REST API.  Make an instance of this class and call methods.
    /// </summary>
    public class TeamServerClient : IDisposable
    {
        private IContrastRestClient _contrastRestClient;

        private const string DEFAULT_AGENT_PROFILE = "default";

        /// <summary>
        /// Creates the client that will interact with TeamServer. 
        /// </summary>
        /// <param name="user">Username (e.g., joe@acme.com)</param>
        /// <param name="serviceKey">User service key</param>
        /// <param name="apiKey">API Key</param>
        /// <param name="teamServerUrl">he base Contrast API URL (e.g., https://app.contrastsecurity.com/Contrast/api/)</param>
        /// <exception cref="System.ArgumentException">Thrown when an invalid Uri is passed in teamServerUrl or a null/empty value is provided for other parameters</exception>
        public TeamServerClient(string user, string serviceKey, string apiKey, string teamServerUrl)
            : this(new ContrastRestClient(new HttpClientWrapper(user, serviceKey, apiKey, teamServerUrl)))
        { }

        public TeamServerClient(IContrastRestClient contrastRestClient)
        {
            _contrastRestClient = contrastRestClient;
        }

        private T GetResponseAndDeserialize<T>(string endpoint)
        {
            using (Stream responseStream = _contrastRestClient.GetResponseStream(endpoint))
            {
                var deserializer = new DataContractJsonSerializer(typeof(T));
                return (T)deserializer.ReadObject(responseStream);
            }
        }

        // TODO  Remove this method if not exists on new API
        /// <summary>
        /// Returns whether a trace exists for an application based on ID and trace conditions.
        /// </summary>
        /// <param name="appId">the ID of the application</param>
        /// <param name="conditions">a name=value pair querystring of trace conditions</param>
        /// <returns>the HTTP response code of the given query</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        [Obsolete("This method is no longer supported and should not be used.")]
        public System.Net.HttpStatusCode CheckForTrace(string appId, string conditions)
        {
            var responseMessage = _contrastRestClient.PostApplicatonSpecificMessage(Endpoints.TRACE_EXISTS, conditions, appId);

            return responseMessage.StatusCode;
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
        public ContrastApplication GetApplication(string organizationId, string appId)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATIONS, organizationId, appId);
            ApplicationResponse response = GetResponseAndDeserialize<ApplicationResponse>(endpoint);
            return response?.Application;
        }

        /// <summary>
        /// Get the list of applications being monitored by Contrast.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <returns>a List of ContrastApplication objects that are being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<ContrastApplication> GetApplications(string organizationId)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATIONS, organizationId, string.Empty);
            ApplicationsResponse response = (GetResponseAndDeserialize<ApplicationsResponse>(endpoint));
            return response?.Applications;
        }

        /// <summary>
        /// Resets an application's library, coverage, statistics and trace information.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <returns>a ContrastApplication object for the appId supplied</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        [Obsolete("Currently unsupported. A new method will be generated to perform this action.")]
        public void ResetApplication(string organizationId, string appId)
        {
            string endpoint = string.Format(Endpoints.APPLICATIONS, organizationId, appId);
            _contrastRestClient.DeleteMessage(endpoint);
        }

        /// <summary>
        /// Return the libraries of the monitored Contrast application.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <returns>a List of Library objects for the given app</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<Library> GetLibraries(string organizationId, string appId)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATION_LIBRARIES, organizationId, appId);
            LibraryResponse response = (GetResponseAndDeserialize<LibraryResponse>(endpoint));
            return response?.Libraries;
        }

        /// <summary>
        /// Return a single agent profile object
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="profileName">the agent profile name</param>
        /// <returns>a Profile object for the named supplied</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        [Obsolete("Not supported at the moment. Might be removed in the future.")]
        public Profile GetProfile(string organizationId, string profileName)
        {
            string endpoint = String.Format(Endpoints.PROFILES, organizationId, profileName);
            return GetResponseAndDeserialize<Profile>(endpoint);
        }

        /// <summary>
        /// Return the profiles setup in TeamServer for Contrast Agents.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <returns>a List of Profile objects</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        [Obsolete("Not supported at the moment. Might be removed in the future.")]
        public List<Profile> GetProfiles(string organizationId)
        {
            string endpoint = String.Format(Endpoints.PROFILES, organizationId, string.Empty);
            return new List<Profile>(GetResponseAndDeserialize<Profile[]>(endpoint));
        }

        /// <summary>
        /// Return the servers monitored by Contrast agents.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="serverId">the ID of the server</param>
        /// <returns>a Server object for the ID supplied</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public Server GetServer(string organizationId, long serverId)
        {
            string endpoint = String.Format(NgEndpoints.SERVERS, organizationId, serverId);
            ServerResponse response = GetResponseAndDeserialize<ServerResponse>(endpoint);
            return response?.Server;
        }

        /// <summary>
        /// Return the servers monitored by Contrast agents.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <returns>a List of Server objects being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<Server> GetServers(string organizationId)
        {
            return GetServers(organizationId, null);
        }

        /// <summary>
        /// Return the servers monitored by Contrast agents.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="filter">Query params that can be added to filter request.</param>
        /// <returns>a List of Server objects being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<Server> GetServers(string organizationId, ServerFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.SERVERS, organizationId, string.Empty);
            if (filter != null)
                endpoint += filter.ToString();

            ServersResponse response = (GetResponseAndDeserialize<ServersResponse>(endpoint));
            return response?.Servers;
        }

        /// <summary>
        /// Search a trace by trace uuid coincidence.
        /// </summary>
        /// <param name="organizationId">User's organization UUID.</param>
        /// <param name="traceUuid">Trace UUID.</param>
        /// <returns>A list of all the traces with the given UUID.</returns>
        public List<Trace> GetTracesByUuid(string organizationId, string traceUuid)
        {
            string endpoit = String.Format(NgEndpoints.TRACE, organizationId, traceUuid);
            TracesSearchResponse response = GetResponseAndDeserialize<TracesSearchResponse>(endpoit);
            return response?.Traces;
        }

        /// <summary>
        /// Returns a list of traces from a certain organization. Other filtering options are available
        /// through the use of TraceFilter params.
        /// </summary>
        /// <param name="organizationId">Organization from which the traces will be retrieved.</param>
        /// <returns>A List of Trace objects.</returns>
        public List<Trace> GetTraces(string organizationId)
        {
            return GetTraces(organizationId, null);
        }

        /// <summary>
        /// Returns a list of traces from a certain organization. Other filtering options are available
        /// through the use of TraceFilter params.
        /// </summary>
        /// <param name="organizationId">Organization from which the traces will be retrieved.</param>
        /// <param name="filter">Query params that can be added to request.</param>
        /// <returns>A List of Trace objects.</returns>
        public List<Trace> GetTraces(string organizationId, TraceFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.ORGANIZATION_TRACES, organizationId);
            if (filter != null)
                endpoint += filter.ToString();

            TraceFilterResponse response = (GetResponseAndDeserialize<TraceFilterResponse>(endpoint));
            return response?.Traces;
        }

        /// <summary>
        /// Get the vulnerabilities in the application for the ID supplied.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <returns>a List of Trace objects representing the vulnerabilities</returns>
        public List<Trace> GetApplicationTraces(string organizationId, string appId)
        {
            return GetApplicationTraces(organizationId, appId, null);
        }

        /// <summary>
        /// Get the vulnerabilities in the application for the ID supplied.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <param name="filter">Query params that can be added to request.</param>
        /// <returns>a List of Trace objects representing the vulnerabilities</returns>
        public List<Trace> GetApplicationTraces(string organizationId, string appId, TraceFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.APPLICATION_TRACES, organizationId, appId);
            if (filter != null)
                endpoint += filter.ToString();
            return new List<Trace>(GetResponseAndDeserialize<Trace[]>(endpoint));
        }

        /// <summary>
        /// Get the vulnerabilities in the Server for the ID supplied.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization.</param>
        /// <param name="serverId">The ID of the server.</param>
        /// <returns></returns>
        public List<Trace> GetServerTraces(string organizationId, string serverId)
        {
            return GetServerTraces(organizationId, serverId, null);
        }

        /// <summary>
        /// Get the vulnerabilities in the Server for the ID supplied.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization.</param>
        /// <param name="serverId">The ID of the server.</param>
        /// <param name="filter">Query params that can be added to request.</param>
        /// <returns></returns>
        public List<Trace> GetServerTraces(string organizationId, string serverId, TraceFilter filter)
        {
            string endpoint = String.Format(NgEndpoints.SERVER_TRACES, organizationId, serverId);
            if (filter != null)
                endpoint += filter.ToString();
            return new List<Trace>(GetResponseAndDeserialize<Trace[]>(endpoint));
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
