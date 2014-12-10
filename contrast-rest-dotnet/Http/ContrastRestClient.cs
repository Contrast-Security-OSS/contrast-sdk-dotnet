/*
 * Copyright (c) 2014, Contrast Security, Inc.
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
using System.IO;

namespace contrast_rest_dotnet.Http
{
    public class ContrastRestClient : IContrastRestClient
    {
        IHttpClient _httpClient;

        public ContrastRestClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Stream GetResponseStream(string apiEndpoint)
        {
            var responseTask = _httpClient.GetAsync(apiEndpoint);
            responseTask.Wait();

            CheckResponseStatusCode(apiEndpoint, responseTask.Result.StatusCode);
            
            var responseStreamTask = responseTask.Result.Content.ReadAsStreamAsync();
            responseStreamTask.Wait();

            return responseStreamTask.Result;
        }

        private static void CheckResponseStatusCode(string apiEndpoint, System.Net.HttpStatusCode statusCode)
        {
            if ((int)statusCode >= 300)
            {
                if (statusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ResourceNotFoundException("Resource: '" + apiEndpoint + "' not found.");
                }
                else
                {
                    throw new ContrastApiException(
                        String.Format("Team Server returned unexpected response code '{0}' for resource: '{1}'",
                            statusCode, apiEndpoint));
                }
            }
        }

        public System.Net.Http.HttpResponseMessage PostApplicatonSpecificMessage(string endpoint, string postBody, string application )
        {
            var headers = new List<Tuple<string, string>>();
            headers.Add( new Tuple<string,string>( "Application", application ) );

            return PostMessage(endpoint, postBody, headers);
        }

        public System.Net.Http.HttpResponseMessage PostMessage(string endpoint, string postBody, List<Tuple<string,string>> headers )
        {
            var responseTask = _httpClient.PostAsync(endpoint, postBody, headers);
            responseTask.Wait();

            var statusCode = responseTask.Result.StatusCode;
            if ((int)statusCode >= 300)
            {
                if (statusCode != System.Net.HttpStatusCode.NotFound)
                {
                    throw new ContrastApiException(
                        String.Format("Team Server returned unexpected response code '{0}' for resource: '{1}'",
                            statusCode, endpoint));
                }
            }

            return responseTask.Result;
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_httpClient != null)
                {
                    _httpClient.Dispose();
                    _httpClient = null;
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
