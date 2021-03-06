﻿#region LICENSE
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

using Contrast;
using Contrast.Http;
using Contrast.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SampleContrastClient
{
    class Program
    {
        private static string _organizationId;

        static void Main(string[] args)
        {
            Console.WriteLine("SampleContrastClient Started.  Reading configuration...");

            string user = ConfigurationManager.AppSettings["TeamServerUserName"];
            string serviceKey = ConfigurationManager.AppSettings["TeamServerServiceKey"];
            string apiKey = ConfigurationManager.AppSettings["TeamServerApiKey"];
            string url = ConfigurationManager.AppSettings["TeamServerUrl"];
            string version = ConfigurationManager.AppSettings["IntegrationVersion"];
            string integrationName = ConfigurationManager.AppSettings["IntegrationName"];
            

            using (Client client = new Client(user, serviceKey, apiKey, url, version, (IntegrationName) Enum.Parse(typeof(IntegrationName), integrationName)))
            {
                Console.WriteLine("Connecting to Contrast Team Server: '{0}' as user: '{1}'", url, user);

                var orgs = client.GetOrganizations();
                Console.WriteLine("User is associated with {0} orgs. {1}", orgs.Count,
                    (orgs.Count > 0 ? "First Organization: " + orgs[0].Name : string.Empty));

                if (orgs.Count > 0)
                {
                    _organizationId = orgs[0].OrganizationId;
                }

                var defaultOrg = client.GetDefaultOrganization();
                Console.WriteLine("User's default org is:{0}({1})", defaultOrg.Name, defaultOrg.OrganizationId);

                var serverResponse = client.GetServers(_organizationId);
                if (serverResponse != null)
                    Console.WriteLine("Found {0} servers.", serverResponse.Servers.Count);
                else
                    Console.WriteLine("No servers found.");

                var appsResponse = client.GetApplications(_organizationId);
                if (appsResponse != null)
                    Console.WriteLine("Found {0} applications.", appsResponse.Applications.Count);
                else
                    Console.WriteLine("No applications found.");

                if (appsResponse != null && appsResponse.Applications.Count > 0)
                {
                    var apps = appsResponse.Applications;
                    string appId = apps[0].AppId;
                    string appName = apps[0].Name;
                    Console.WriteLine("Retrieving traces for the first application: {0} ({1}", appName, appId);

                    var traceResponse = client.GetTraces(_organizationId);

                    if (traceResponse != null)
                        Console.WriteLine("Found {0} traces for application.", traceResponse.Traces.Count);
                    else
                        Console.WriteLine("No traces found for application.");

                    if (traceResponse != null && traceResponse.Traces.Count > 0)
                    {
                        var traces = traceResponse.Traces;
                        WriteFirstTenTraces(traces);

                        //foreach (Trace trace in traces)
                        //{
                        //    Console.WriteLine("Trace Exists:{0}", DoesTraceExist(client, traces.Uuid, _organizationId));
                        //}
                    }
                }

                // DownloadAgentToDesktop(client);
            }

            Console.WriteLine("SampleContrastClient Finished.");
            Console.ReadLine();
        }

        private static void WriteFirstTenTraces(List<Trace> traces)
        {
            var traceSelection = (from t in traces select t).Take<Trace>(10).ToList();

            Console.WriteLine("The First " + traceSelection.Count + " Traces:");
            Console.WriteLine("---------------------------------------");

            foreach (var trace in traceSelection)
            {
                Console.WriteLine("{0} (found: {1}, lastSeen: {2}", GetTitle(trace), trace.FirstTimeSeen, trace.LastTimeSeen);
            }
            Console.WriteLine("---------------------------------------");
        }

        private static string GetTitle(Trace trace)
        {
            string title = trace.Title;

            if (String.IsNullOrEmpty(title))
            {
                title = trace.RuleName;
            }

            return title;
        }

        // Example usage of GetAgent method
        private static void DownloadAgentToDesktop(Client client)
        {
            string filename = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\dotnetagent.zip";
            using (var agentStream = client.GetAgent(AgentType.DotNet, _organizationId))
            {
                using (var fs = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    agentStream.CopyTo(fs);
                }
            }
        }

        // Example usage of DoesTraceExist method
        private static bool DoesTraceExist(Client client, string traceUuid, string organizationId)
        {
            var traces = client.GetTracesByUuid(organizationId, traceUuid)?.Traces;

            return (traces != null && traces.Count > 0);
        }
    }
}
