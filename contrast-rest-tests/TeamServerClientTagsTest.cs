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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Moq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using contrast_rest_dotnet;
using contrast_rest_dotnet.Model;
using contrast_rest_dotnet.Http;

namespace sdk_tests
{
    [TestClass]
    public class TeamServerClientTagsTest
    {
        [TestMethod]
        public void DeleteTags_VerifyBaseResponse()
        {
            string json = @"{
                            ""success"": true,
                            ""messages"": [
                                ""Delete successful""
                            ],
                            ""totalLibraryHashes"": 0
                        }";
            TagRequest request = new TagRequest();
            request.Tag = "none";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.DeleteMessage("api/ng/orgId/tags/trace/traceId", JsonConvert.SerializeObject(request))).Returns(
                PostUtil.GetPostResponse(System.Net.HttpStatusCode.OK, json)
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.DeleteTraceTag("orgId", "traceId", "none");

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Messages.Count);
        }

        [TestMethod]
        public void GetTraceUniqueTags_VerifyTags()
        {
            string json = @"{
                              ""success"": true,
                              ""messages"": [
                                ""Unique tags for organization loaded successfully""
                              ],
                              ""tags"": [
                                ""Infinite Scroll Test"",
                                ""Another test too""
                              ],
                              ""totalLibraryHashes"": 0
                            }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/tags/traces")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(json))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.GetTracesUniqueTags("orgId");

            Assert.AreEqual(2, response.Tags.Count);
            Assert.AreEqual("Infinite Scroll Test", response.Tags[0]);
            Assert.AreEqual("Another test too", response.Tags[1]);
        }

        [TestMethod]
        public void GetTraceUniqueTagsByServer_VerifyTags()
        {
            string json = @"{
                              ""success"": true,
                              ""messages"": [
                                ""Unique tags for organization loaded successfully""
                              ],
                              ""tags"": [
                                ""Infinite Scroll Test"",
                                ""Another test too""
                              ],
                              ""totalLibraryHashes"": 0
                            }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/tags/traces/server/1")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(json))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.GetTracesUniqueTags("orgId", 1);

            Assert.AreEqual(2, response.Tags.Count);
            Assert.AreEqual("Infinite Scroll Test", response.Tags[0]);
            Assert.AreEqual("Another test too", response.Tags[1]);
        }

        [TestMethod]
        public void GetTraceUniqueTagsByApplication_VerifyTags()
        {
            string json = @"{
                              ""success"": true,
                              ""messages"": [
                                ""Unique tags for organization loaded successfully""
                              ],
                              ""tags"": [
                                ""Infinite Scroll Test"",
                                ""Another test too""
                              ],
                              ""totalLibraryHashes"": 0
                            }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/tags/traces/application/appId")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(json))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.GetTracesUniqueTags("orgId", "appId");

            Assert.AreEqual(2, response.Tags.Count);
            Assert.AreEqual("Infinite Scroll Test", response.Tags[0]);
            Assert.AreEqual("Another test too", response.Tags[1]);
        }

        [TestMethod]
        public void TagTraces_VerifySuccess()
        {
            string json = @"{
                            ""success"": true,
                            ""messages"": [
                                ""Tag successful""
                            ]
                        }";
            TagsServersResource request = new TagsServersResource();
            request.TracesId = new List<string> { "traceId1", "traceId2" };
            request.Tags = new List<string> { "testTag", "anotherTag"};

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.PutMessage("api/ng/orgId/tags/traces", JsonConvert.SerializeObject(request), null)).Returns(
                PostUtil.GetPostResponse(System.Net.HttpStatusCode.OK, json)
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.TagTraces("orgId", request);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void GetTagsByTraces_VerifyTags()
        {
            string json = @"{
                              ""success"": true,
                              ""messages"": [
                                ""Unique tags for organization loaded successfully""
                              ],
                              ""tags"": [
                                ""Infinite Scroll Test"",
                                ""Different test too""
                              ],
                              ""totalLibraryHashes"": 0
                            }";
            TagsTraceRequest request = new TagsTraceRequest();
            request.TracesId = new List<string> { "traceId1", "traceId2" };

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.PostMessage("api/ng/orgId/tags/traces/bulk", JsonConvert.SerializeObject(request), null)).Returns(
                PostUtil.GetPostResponse(System.Net.HttpStatusCode.OK, json)
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.GetTagsByTraces("orgId", request);

            Assert.AreEqual(2, response.Tags.Count);
            Assert.AreEqual("Infinite Scroll Test", response.Tags[0]);
            Assert.AreEqual("Different test too", response.Tags[1]);
        }

        [TestMethod]
        public void TagsTracesBulk_VerifySuccess()
        {
            string json = @"{
                            ""success"": true,
                            ""messages"": [
                                ""Tag successful""
                            ]
                        }";
            TagsTracesUpdateRequest request = new TagsTracesUpdateRequest();
            request.TracesId = new List<string> { "traceId1", "traceId2" };

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.PutMessage("api/ng/orgId/tags/traces/bulk", JsonConvert.SerializeObject(request), null)).Returns(
                PostUtil.GetPostResponse(System.Net.HttpStatusCode.OK, json)
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.TagsTracesBulk("orgId", request);

            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void GetTagsByTrace_VerifyTags()
        {
            string json = @"{
                              ""success"": true,
                              ""messages"": [
                                ""Unique tags for organization loaded successfully""
                              ],
                              ""tags"": [
                                ""Different test""
                              ],
                              ""totalLibraryHashes"": 0
                            }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/tags/traces/trace/traceId")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(json))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.GetTagsByTrace("orgId", "traceId");

            Assert.AreEqual(1, response.Tags.Count);
            Assert.AreEqual("Different test", response.Tags[0]);
        }
    }
}
