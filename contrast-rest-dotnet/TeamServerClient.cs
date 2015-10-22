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

        /// <summary>
        /// Creates the client that will interact with TeamServer. 
        /// </summary>
        /// <param name="user">Username (e.g., joe@acme.com)</param>
        /// <param name="serviceKey">User service key</param>
        /// <param name="apiKey">API Key</param>
        /// <param name="teamServerUrl">he base Contrast API URL (e.g., https://app.contrastsecurity.com/Contrast/api/)</param>
        /// <exception cref="System.ArgumentException">Thrown when an invalid Uri is passed in teamServerUrl or a null/empty value is provided for other parameters</exception>
        public TeamServerClient( string user, string serviceKey, string apiKey, string teamServerUrl ) 
            : this( new ContrastRestClient( new HttpClientWrapper( user, serviceKey, apiKey, teamServerUrl ) ) )
        {}

        public TeamServerClient(IContrastRestClient contrastRestClient)
        {
            _contrastRestClient = contrastRestClient;
        }

        private T GetResponseAndDeserialize<T>( string endpoint )
        {
            using (Stream responseStream = _contrastRestClient.GetResponseStream(endpoint))
            {
                var deserializer = new DataContractJsonSerializer(typeof(T));
                return (T)deserializer.ReadObject(responseStream);
            }
        }

        /// <summary>
        /// Returns whether a trace exists for an application based on ID and trace conditions.
        /// </summary>
        /// <param name="appId">the ID of the application</param>
        /// <param name="conditions">a name=value pair querystring of trace conditions</param>
        /// <returns>the HTTP response code of the given query</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
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
            return GetAgent(agentType, organizationId, "default");
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
            if(agentType == AgentType.DotNet)
            {
                agentEndpoint = string.Format(Endpoints.ENGINE_DOTNET, organizationId, profileName);
            }
            else if (agentType == AgentType.Java)
            {
                agentEndpoint = string.Format(Endpoints.ENGINE_JAVA, organizationId, profileName);
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
            string endpoint = String.Format(Endpoints.APPLICATIONS, organizationId, appId);
            return GetResponseAndDeserialize<ContrastApplication>(endpoint);
        }

        /// <summary>
        /// Get the list of applications being monitored by Contrast.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <returns>a List of ContrastApplication objects that are being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<ContrastApplication> GetApplications(string organizationId)
        {
            string endpoint = String.Format(Endpoints.APPLICATIONS, organizationId, string.Empty);
            return new List<ContrastApplication>(GetResponseAndDeserialize<ContrastApplication[]>(endpoint));
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
            string endpoint = String.Format(Endpoints.LIBRARIES, organizationId, appId);
            return new List<Library>(GetResponseAndDeserialize<Library[]>(endpoint));
        }

        /// <summary>
        /// Return a single agent profile object
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="profileName">the agent profile name</param>
        /// <returns>a Profile object for the named supplied</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
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
        public Server GetServer(string organizationId, string serverId)
        {
            string endpoint = String.Format(Endpoints.SERVERS, organizationId, serverId);
            return GetResponseAndDeserialize<Server>(endpoint);
        }

        /// <summary>
        /// Return the servers monitored by Contrast agents.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <returns>a List of Server objects being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<Server> GetServers(string organizationId)
        {
            string endpoint = String.Format(Endpoints.SERVERS, organizationId, string.Empty);
            return new List<Server>(GetResponseAndDeserialize<Server[]>(endpoint));
        }

        /// <summary>
        /// Get the vulnerabilities in the application for the ID supplied.
        /// </summary>
        /// <param name="organizationId">The uuid of the user's organization</param>
        /// <param name="appId">the ID of the application</param>
        /// <returns>a List of Trace objects representing the vulnerabilities</returns>
        public List<Trace> GetTraces(string organizationId, string appId)
        {
            string endpoint = String.Format(Endpoints.TRACES, organizationId, appId);
            return new List<Trace>(GetResponseAndDeserialize<Trace[]>(endpoint));
        }

        /// <summary>
        /// Get the organizations associated with the API user
        /// </summary>
        /// <returns>a List of Organization objects representing the organizations</returns>
        public List<Organization> GetOrganizations()
        {
            Stream responseStream = _contrastRestClient.GetResponseStream(NgEndpoints.ORGANIZATIONS);
            var deserializer = new DataContractJsonSerializer(typeof(OrganizationResponse));
            var orgs = (OrganizationResponse)deserializer.ReadObject(responseStream);

            return orgs.organizations;
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
