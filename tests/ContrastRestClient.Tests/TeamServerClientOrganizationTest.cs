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
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Contrast;
using Contrast.Http;
using System.Text;
using System.IO;

namespace sdk_tests
{
    [TestClass]
    public class TeamServerClientOrganizationTest
    {
        [TestMethod]
        public void GetOrganizationInfo_VerifySuccess()
        {
            string json = @"{
                              ""success"": true,
                              ""messages"": [
                                ""Organization Information loaded successfully""
                              ],
                              ""organization"": {
                                ""name"": ""Test organization"",
                                ""timezone"": ""EST"",
                                ""superadmin"": false,
                                ""organization_uuid"": ""0c2a726b-af04-47b6-8be9-844058fbcdbd"",
                                ""date_format"": ""MM/dd/yyyy"",
                                ""time_format"": ""hh:mm a"",
                                ""creation_time"": 1531430241000,
                                ""protection_enabled"": true,
                                ""auto_license_protection"": false,
                                ""auto_license_assessment"": false,
                                ""is_superadmin"": false,
                                ""server_environments"": []
                                },
                              ""managed"": true
                            }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/organizations")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(json))
                );
            var teamServerClient = new Client(mockSdkHttpClient.Object);
            var response = teamServerClient.GetOrganizationInfo("orgId");

            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Organization.name, "Test organization");
        }

        [TestMethod]
        public void GetOrganizationInfoWithExpand_VerifySuccess()
        {
            string json = @"{
                              ""success"": true,
                              ""messages"": [
                                ""Organization Information loaded successfully""
                              ],
                              ""organization"": {
                                ""name"": ""Test organization"",
                                ""timezone"": ""EST"",
                                ""freemium"": false,
                                ""superadmin"": false,
                                ""organization_uuid"": ""0c2a726b-af04-47b6-8be9-844058fbcdbd"",
                                ""date_format"": ""MM/dd/yyyy"",
                                ""time_format"": ""hh:mm a"",
                                ""creation_time"": 1531430241000,
                                ""protection_enabled"": true,
                                ""auto_license_protection"": false,
                                ""auto_license_assessment"": false,
                                ""is_superadmin"": false,
                                ""server_environments"": []
                                },
                              ""managed"": true
                            }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/organizations?expand=freemium")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(json))
                );
            var teamServerClient = new Client(mockSdkHttpClient.Object);
            var response = teamServerClient.GetOrganizationInfo("orgId", new List<OrganizationExpandValues>{ OrganizationExpandValues.freemium });

            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Organization.name, "Test organization");
        }
    }
}
