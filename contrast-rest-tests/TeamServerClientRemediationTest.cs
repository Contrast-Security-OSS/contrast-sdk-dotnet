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

using Moq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using contrast_rest_dotnet;
using contrast_rest_dotnet.Model;
using contrast_rest_dotnet.Http;

namespace sdk_tests
{
    [TestClass]
    public class TeamServerClientRemediationTest
    {
        [TestMethod]
        public void MarkTraceStatus_VerifySuccess()
        {
            string json = @"{
                            ""success"": true,
                            ""messages"": [
                                ""1 Vulnerability successfully marked as Reported""
                            ]
                        }";
            TraceMarkStatusRequest request = new TraceMarkStatusRequest();
            request.Traces = new List<string> { "traceId" };
            request.Note = "This is my note.";
            request.Status = "";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.PutMessage("api/ng/orgId/orgtraces/mark", JsonConvert.SerializeObject(request), null)).Returns(
                PostUtil.GetPostResponse(System.Net.HttpStatusCode.OK, json)
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.MarkTraceStatus("orgId", request);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void MarkTraceStatusByServer_VerifySuccess()
        {
            string json = @"{
                            ""success"": true,
                            ""messages"": [
                                ""1 Vulnerability successfully marked as Reported""
                            ]
                        }";
            TraceMarkStatusRequest request = new TraceMarkStatusRequest();
            request.Traces = new List<string> { "traceId" };
            request.Note = "This is my note.";
            request.Status = "";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.PutMessage("api/ng/orgId/servertraces/1/mark", JsonConvert.SerializeObject(request), null)).Returns(
                PostUtil.GetPostResponse(System.Net.HttpStatusCode.OK, json)
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.MarkTraceStatus("orgId", 1, request);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void MarkTraceStatusByApplication_VerifySuccess()
        {
            string json = @"{
                            ""success"": true,
                            ""messages"": [
                                ""1 Vulnerability successfully marked as Reported""
                            ]
                        }";
            TraceMarkStatusRequest request = new TraceMarkStatusRequest();
            request.Traces = new List<string> { "traceId" };
            request.Note = "This is my note.";
            request.Status = "";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.PutMessage("api/ng/orgId/traces/appId/mark", JsonConvert.SerializeObject(request), null)).Returns(
                PostUtil.GetPostResponse(System.Net.HttpStatusCode.OK, json)
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.MarkTraceStatus("orgId", "appId", request);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void MarkTraceStatus_VerifyException()
        {
            string json = @"{
                            ""success"": false,
                            ""messages"": [
                                ""Forbidden access?""
                            ]
                        }";
            TraceMarkStatusRequest request = new TraceMarkStatusRequest();
            request.Traces = new List<string> { "traceId" };
            request.Note = "This is my note.";
            request.Status = "";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.PutMessage("api/ng/orgId/orgtraces/mark", JsonConvert.SerializeObject(request), null)).Returns(
                PostUtil.GetPostResponse(System.Net.HttpStatusCode.Forbidden, json)
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            try
            {
                var response = teamServerClient.MarkTraceStatus("orgId", request);
                Assert.Fail();
            }
            catch(Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ForbiddenException));
            }
        }
    }
}
