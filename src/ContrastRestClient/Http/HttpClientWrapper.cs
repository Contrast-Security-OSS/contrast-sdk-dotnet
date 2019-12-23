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

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contrast.Http
{
    public class HttpClientWrapper : IHttpClient
    {
        private string _teamServerUrl;
        private HttpClient _httpClient;

        public HttpClientWrapper(string user, string serviceKey, string apiKey, string teamServerUrl)
        {
            ValidateParameters(user, serviceKey);
            Uri uriCreateResult = ValidateAndCreateUri(teamServerUrl);

            byte[] tokenBytes = Encoding.ASCII.GetBytes(user + ":" + serviceKey);
            string authorizationToken = Convert.ToBase64String(tokenBytes);
            
            _httpClient = new HttpClient(new HttpClientHandler() { UseCookies = false, AllowAutoRedirect = false });
            _httpClient.BaseAddress = uriCreateResult;
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationToken);
            _httpClient.DefaultRequestHeaders.Add("API-Key", apiKey);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        private static void ValidateParameters(string user, string serviceKey)
        {
            if (String.IsNullOrEmpty(user))
            {
                throw new ArgumentException("Username null/empty.", nameof(user));
            }

            if (String.IsNullOrEmpty(serviceKey))
            {
                throw new ArgumentException("serviceKey null/empty.", nameof(serviceKey));
            }
        }

        private Uri ValidateAndCreateUri(string teamServerUrl)
        {
            bool isValidUri = Uri.TryCreate(teamServerUrl, UriKind.Absolute, out var uriCreateResult);
            if (!isValidUri)
            {
                throw new ArgumentException("Rest API URL provided is not a valid URI: '" + teamServerUrl + "'", nameof(teamServerUrl));
            }
            _teamServerUrl = teamServerUrl;
            return uriCreateResult;
        }

        public Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return _httpClient.GetAsync(endpoint);
        }

        private Task<HttpResponseMessage> RequestAsync(string endpoint, string postBody, List<Tuple<string, string>> additionalHeaders, HttpMethod method)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_teamServerUrl + endpoint),
                Method = method,
                Content = new StringContent(postBody, Encoding.UTF8, "application/json")
            };

            if(additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                    request.Headers.Add(header.Item1, header.Item2);
            }

            return _httpClient.SendAsync(request);
        }

        public Task<HttpResponseMessage> PostAsync(string endpoint, string postBody, List<Tuple<string, string>> additionalHeaders)
        {
            return RequestAsync(endpoint, postBody, additionalHeaders, HttpMethod.Post);
        }

        public Task<HttpResponseMessage> PutAsync(string endpoint, string postBody, List<Tuple<string, string>> additionalHeaders)
        {
            return RequestAsync(endpoint, postBody, additionalHeaders, HttpMethod.Put);
        }

        public Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_teamServerUrl + endpoint),
                Method = HttpMethod.Delete
            };

            return _httpClient.SendAsync(request);
        }

        public Task<HttpResponseMessage> DeleteAsync(string endpoint, string postBody)
        {
            return RequestAsync(endpoint, postBody, null, HttpMethod.Delete);
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
