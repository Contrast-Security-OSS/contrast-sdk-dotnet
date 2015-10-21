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

        /// <summary>
        /// Returns whether a trace exists for an application based on ID and trace conditions.
        /// </summary>
        /// <param name="appId">the ID of the application</param>
        /// <param name="conditions">a name=value pair querystring of trace conditions</param>
        /// <returns>the HTTP response code of the given query</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public System.Net.HttpStatusCode CheckForTrace(string appId, string conditions)
        {
            var responseMessage = _contrastRestClient.PostApplicatonSpecificMessage("s/traces/exists", conditions, appId);

            return responseMessage.StatusCode;
        }

        /// <summary>
        /// Download a contrast agent associated with this account. The .NET agent should
        /// be saved to a contrast.zip file. The Java agent should be saved to contrast.jar.
        /// </summary>
        /// <param name="agentType">the type of agent to download (.NET or Java)</param>
        /// <returns>a Stream of the agent file contents which should be saved using the appropriate filetype</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public Stream GetAgent(AgentType agentType)
        {
            return GetAgent(agentType, "default");
        }

        /// <summary>
        /// Download a contrast agent associated with this account. The .NET agent should
        /// be saved to a contrast.zip file. The Java agent should be saved to contrast.jar.
        /// This signature takes a parameter which contains the name of the saved engine profile
        /// to download.
        /// </summary>
        /// <param name="agentType">the type of agent to download (.NET or Java)</param>
        /// <param name="profileName">the name of the saved engine profile to download</param>
        /// <returns>a Stream of the agent file contents which should be saved using the appropriate filetype</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public Stream GetAgent(AgentType agentType, string profileName)
        {
            string agentEndpoint = null;
            if(agentType == AgentType.DotNet)
            {
                agentEndpoint = string.Format(Endpoints.ENGINE_DOTNET, profileName);
            }
            else if (agentType == AgentType.Java)
            {
                agentEndpoint = string.Format(Endpoints.ENGINE_JAVA, profileName);
            }

            return _contrastRestClient.GetResponseStream(agentEndpoint);
        }

        /// <summary>
        /// Get summary information about a single application.
        /// </summary>
        /// <param name="appId">the ID of the application</param>
        /// <returns>a ContrastApplication object for the appId supplied</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public ContrastApplication GetApplication(string appId)
        {
            Stream responseStream = _contrastRestClient.GetResponseStream(Endpoints.APPLICATIONS + appId );

            var deserializer = new DataContractJsonSerializer(typeof(ContrastApplication));
            var app = (ContrastApplication)deserializer.ReadObject(responseStream);

            return app;
        }

        /// <summary>
        /// Get the list of applications being monitored by Contrast.
        /// </summary>
        /// <returns>a List of ContrastApplication objects that are being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<ContrastApplication> GetApplications()
        {
            Stream responseStream = _contrastRestClient.GetResponseStream(Endpoints.APPLICATIONS);

            var deserializer = new DataContractJsonSerializer(typeof(ContrastApplication[]));
            var apps = (ContrastApplication[])deserializer.ReadObject(responseStream);           

            return new List<ContrastApplication>( apps );
        }

        /// <summary>
        /// Return the libraries of the monitored Contrast application.
        /// </summary>
        /// <param name="appId">the ID of the application</param>
        /// <returns>a List of Library objects for the given app</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<Library> GetLibraries(string appId)
        {
            Stream responseStream = _contrastRestClient.GetResponseStream(Endpoints.APPLICATIONS + appId + "/libraries/");

            var deserializer = new DataContractJsonSerializer(typeof(Library[]));
            var libraries = (Library[])deserializer.ReadObject(responseStream);

            return new List<Library>(libraries);
        }

        /// <summary>
        /// Return a single agent profile object
        /// </summary>
        /// <param name="profileName">the agent profile name</param>
        /// <returns>a Profile object for the named supplied</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public Profile GetProfile(string profileName)
        {
            Stream responseStream = _contrastRestClient.GetResponseStream(Endpoints.PROFILES + profileName );

            var deserializer = new DataContractJsonSerializer(typeof(Profile));
            var profile = (Profile)deserializer.ReadObject(responseStream);

            return profile;
        }

        /// <summary>
        /// Return the profiles setup in TeamServer for Contrast Agents.
        /// </summary>
        /// <returns>a List of Profile objects</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<Profile> GetProfiles()
        {
            Stream responseStream = _contrastRestClient.GetResponseStream(Endpoints.PROFILES);

            var deserializer = new DataContractJsonSerializer(typeof(Profile[]));
            var profiles = (Profile[])deserializer.ReadObject(responseStream);

            return new List<Profile>( profiles );
        }

        /// <summary>
        /// Return the servers monitored by Contrast agents.
        /// </summary>
        /// <param name="serverId">the ID of the server</param>
        /// <returns>a Server object for the ID supplied</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public Server GetServer(string serverId)
        {
            Stream responseStream = _contrastRestClient.GetResponseStream(Endpoints.SERVERS + serverId);

            var deserializer = new DataContractJsonSerializer(typeof(Server));
            var server = (Server)deserializer.ReadObject(responseStream);

            return server;
        }

        /// <summary>
        /// Return the servers monitored by Contrast agents.
        /// </summary>
        /// <returns>a List of Server objects being monitored</returns>
        /// <exception cref="System.AggregateException">Thrown when there is an error communicating with TeamServer</exception>
        public List<Server> GetServers()
        {
            Stream responseStream = _contrastRestClient.GetResponseStream(Endpoints.SERVERS);

            var deserializer = new DataContractJsonSerializer(typeof(Server[]));
            var servers = (Server[])deserializer.ReadObject(responseStream);

            return new List<Server>(servers);
        }

        /// <summary>
        /// Get the vulnerabilities in the application for the ID supplied.
        /// </summary>
        /// <param name="appId">the ID of the application</param>
        /// <returns>a List of Trace objects representing the vulnerabilities</returns>
        public List<Trace> GetTraces(string appId)
        {
            Stream responseStream = _contrastRestClient.GetResponseStream(Endpoints.TRACES + appId);

            var deserializer = new DataContractJsonSerializer(typeof(Trace[]));
            var traces = (Trace[])deserializer.ReadObject(responseStream);

            return new List<Trace>(traces);
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
