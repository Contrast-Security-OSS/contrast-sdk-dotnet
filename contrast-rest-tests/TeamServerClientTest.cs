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

using contrast_rest_dotnet;
using contrast_rest_dotnet.Http;
using contrast_rest_dotnet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Text;

namespace sdk_tests
{
    [TestClass]
    public class TeamServerClientTest
    {
        [TestMethod]
        public void Constructor_ValidUrl_NoException()
        {
            var tsClient = new TeamServerClient("arbitraryUser", "arbitraryServiceKey", "arbitraryApiKey",
                "http://localhost/Contrast");
        }

        [TestMethod,ExpectedException(typeof(ArgumentException))]
        public void Constructor_InvalidUrl_ArgumentExceptionThrown()
        {
            var tsClient = new TeamServerClient("arbitraryUser", "arbitraryServiceKey", "arbitraryApiKey",
                "invalidUrlValue");
        }

        [TestMethod]
        public void GetApplications_PropertiesMatchExpected()
        {
            string json = @"{
                            ""success"": true,
                            ""messages"": [
                              ""Applications loaded successfully""
                            ],
                            ""applications"": [
                              {
                                ""name"": ""MyTestApp"",
                                ""path"": ""/MyTestApp"",
                                ""language"": "".NET"",
                                ""created"": 1422560875000,
                                ""status"": ""offline"",
                                ""importance"": 2,
                                ""archived"": false,
                                ""assess"": true,
                                ""assessPending"": false,
                                ""master"": false,
                                ""notes"": """",
                                ""roles"": [
                                  ""ROLE_EDIT"",
                                  ""ROLE_RULES_ADMIN"",
                                  ""ROLE_VIEW""
                                ],
                                ""tags"": [
                                  ""Awesome Group""
                                  ],
                                ""techs"": [],
                                ""policies"": [],
                                ""links"": [
                                  {
                                    ""rel"": ""self"",
                                    ""href"": ""https://localhost/Contrast/api/ng/3c646b24-48bf-4345-9dac-6933324bafb4/applications/2b75b619-8a37-463e-b605-bd8f340d03aa"",
                                    ""method"": ""GET""
                                  },
                                  {
                                    ""rel"": ""restore"",
                                    ""href"": ""https://localhost/Contrast/api/ng/3c646b24-48bf-4345-9dac-6933324bafb4/applications/2b75b619-8a37-463e-b605-bd8f340d03aa/restore"",
                                    ""method"": ""POST""
                                  },
                                  {
                                    ""rel"": ""archive"",
                                    ""href"": ""https://localhost/Contrast/api/ng/3c646b24-48bf-4345-9dac-6933324bafb4/applications/2b75b619-8a37-463e-b605-bd8f340d03aa/archive"",
                                    ""method"": ""POST""
                                  },
                                  {
                                    ""rel"": ""reset"",
                                    ""href"": ""https://localhost/Contrast/api/ng/3c646b24-48bf-4345-9dac-6933324bafb4/applications/2b75b619-8a37-463e-b605-bd8f340d03aa/reset"",
                                    ""method"": ""POST""
                                  },
                                  {
                                    ""rel"": ""delete"",
                                    ""href"": ""https://localhost/Contrast/api/ng/3c646b24-48bf-4345-9dac-6933324bafb4/applications/2b75b619-8a37-463e-b605-bd8f340d03aa"",
                                    ""method"": ""DELETE""
                                  }
                                ],
                                ""app_id"": ""2b75b619-8a37-463e-b605-bd8f340d03aa"",
                                ""group_name"": null,
                                ""last_seen"": 1422582483000,
                                ""last_reset"": null,
                                ""size_shorthand"": ""0k"",
                                ""size"": 0,
                                ""code_shorthand"": ""0k"",
                                ""code"": 0,
                                ""override_url"": null,
                                ""short_name"": null,
                                ""total_modules"": 1
                              }
                            ]
                        }";
            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/applications/")).Returns(
                new MemoryStream( Encoding.Unicode.GetBytes(json) )
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var apps = teamServerClient.GetApplications("orgId");

            Assert.AreEqual(1, apps.Count);
            ContrastApplication app = apps[0];
            Assert.AreEqual("2b75b619-8a37-463e-b605-bd8f340d03aa", app.AppID);
            Assert.AreEqual("MyTestApp", app.Name);
        }

        [TestMethod]
        public void GetLibraries_PropertiesMatchExpected()
        {
            string appId = "arbitraryId";
            string libraryJson = @"{
                                  ""success"": true,
                                  ""messages"": [
                                    ""Libraries loaded successfully""
                                  ],
                                  ""libraries"": [
                                    {
                                      ""hash"": ""44c59665e7044a1cda8456af3fc8b6dd8713c509"",
                                      ""custom"": true,
                                      ""grade"": ""?"",
                                      ""score"": -1,
                                      ""agePenalty"": 0,
                                      ""versionPenalty"": 0,
                                      ""version"": ""?"",
                                      ""loc"": 38813,
                                      ""tags"": [],
                                      ""restricted"": false,
                                      ""count"": 0,
                                      ""file_name"": ""esapi-2.1.0.jar"",
                                      ""app_language"": ""Java"",
                                      ""group"": """",
                                      ""file_version"": ""?"",
                                      ""latest_version"": ""?"",
                                      ""release_date"": 0,
                                      ""latest_release_date"": 0,
                                      ""classes_used"": 34,
                                      ""class_count"": 197,
                                      ""loc_shorthand"": null,
                                      ""total_vulnerabilities"": 0,
                                      ""months_outdated"": -1,
                                      ""versions_behind"": 0,
                                      ""high_vulnerabilities"": 0,
                                      ""invalid_version"": false
                                    }
                                  ],
                                  ""count"": 9,
                                  ""averageScoreLetter"": ""F"",
                                  ""averageScore"": 58,
                                  ""averageMonths"": null,
                                  ""quickFilters"": []
                                }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/applications/arbitraryId/libraries")).Returns(
                new MemoryStream(Encoding.Unicode.GetBytes(libraryJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var libs = teamServerClient.GetLibraries("orgId", appId);

            Assert.AreEqual(1, libs.Count);
            Library lib = libs[0];
            Assert.AreEqual("Java", lib.AppLanguage);
            Assert.AreEqual("esapi-2.1.0.jar", lib.FileName);
            Assert.IsTrue(lib.Custom);
        }

        [TestMethod]
        public void GetTraces_DataFlow_PropertiesMatchExpected()
        {
            string orgId = "orgId";
            string dataFlowTraceJson = @"{
                                      ""success"": true,
                                      ""messages"": [
                                        ""Organization Vulnerabilities loaded successfully""
                                      ],
                                      ""traces"": [
                                        {
                                          ""app_version_tags"": [],
                                          ""bugtracker_tickets"": [],
                                          ""category"": ""Injection"",
                                          ""closed_time"": null,
                                          ""confidence"": ""High"",
                                          ""default_severity"": ""CRITICAL"",
                                          ""evidence"": null,
                                          ""first_time_seen"": 1461239904769,
                                          ""hasParentApp"": false,
                                          ""impact"": ""High"",
                                          ""language"": ""Java"",
                                          ""last_time_seen"": 1461239904769,
                                          ""license"": ""Licensed"",
                                          ""likelihood"": ""High"",
                                          ""organization_name"": ""Organization Test"",
                                          ""reported_to_bug_tracker"": false,
                                          ""reported_to_bug_tracker_time"": null,
                                          ""request"": {
                                            ""protocol"": ""http"",
                                            ""version"": ""1.0"",
                                            ""uri"": ""/webgoat/attack"",
                                            ""queryString"": ""Screen=14&menu=1100"",
                                            ""method"": ""POST"",
                                            ""port"": 0,
                                            ""headers"": [
                                                {
                                                ""name"": ""Content-length"",
                                                ""value"": ""30""
                                                },
                                                {
                                                ""name"": ""Referer"",
                                                ""value"": ""http://localhost/webgoat/attack?Screen=14&menu=1100&Restart=14""
                                                },
                                                {
                                                ""name"": ""Accept-language"",
                                                ""value"": ""en-US,en;q=0.8""
                                                },
                                                {
                                                ""name"": ""Cookie"",
                                                ""value"": ""JSESSIONID=94BB3A4BF19AFE18B1CA9C6E2AA807C6""
                                                },
                                                {
                                                ""name"": ""Origin"",
                                                ""value"": ""http://localhost""
                                                },
                                                {
                                                ""name"": ""Accept"",
                                                ""value"": ""text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8""
                                                },
                                                {
                                                ""name"": ""Authorization"",
                                                ""value"": ""Basic Z3Vlc3Q6Z3Vlc3Q=""
                                                },
                                                {
                                                ""name"": ""Host"",
                                                ""value"": ""localhost:9090""
                                                },
                                                {
                                                ""name"": ""Upgrade-insecure-requests"",
                                                ""value"": ""1""
                                                },
                                                {
                                                ""name"": ""Connection"",
                                                ""value"": ""keep-alive""
                                                },
                                                {
                                                ""name"": ""Content-type"",
                                                ""value"": ""application/x-www-form-urlencoded""
                                                },
                                                {
                                                ""name"": ""Cache-control"",
                                                ""value"": ""max-age=0""
                                                },
                                                {
                                                ""name"": ""Accept-encoding"",
                                                ""value"": ""gzip, deflate""
                                                },
                                                {
                                                ""name"": ""User-agent"",
                                                ""value"": ""Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36""
                                                }
                                            ],
                                            ""parameters"": [
                                                {
                                                ""name"": ""Screen"",
                                                ""value"": ""14""
                                                },
                                                {
                                                ""name"": ""account_name"",
                                                ""value"": ""Citi""
                                                },
                                                {
                                                ""name"": ""SUBMIT"",
                                                ""value"": ""Go!""
                                                },
                                                {
                                                ""name"": ""menu"",
                                                ""value"": ""1100""
                                                }
                                            ]
                                          },
                                          ""rule_name"": ""sql-injection"",
                                          ""severity"": ""Critical"",
                                          ""status"": ""Reported"",
                                          ""sub_status"": """",
                                          ""sub_title"": ""from \""account_name\"" Parameter on \""attack\"" page"",
                                          ""title"": ""SQL Injection from \""account_name\"" Parameter on \""attack\"" page"",
                                          ""total_traces_received"": 1,
                                          ""uuid"": ""4AD2-6HT1-X845-IVXE"",
                                          ""visible"": true
                                        }
                                      ],
                                      ""count"": 202,
                                      ""licensedCount"": 145,
                                      ""links"": [
                                        {
                                          ""rel"": ""nextPage"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3c646b24-48bf-4345-9dac-6933324bafb4/orgtraces/filter?expand=events&limit=1&offset=9&sort=-lastTimeSeen"",
                                          ""method"": ""GET""
                                        }
                                      ]
                                    }";
            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/" + orgId + "/orgtraces/filter")).Returns(
                new MemoryStream(Encoding.Unicode.GetBytes(dataFlowTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traces = teamServerClient.GetTraces("orgId");

            Assert.AreEqual(1, traces.Count);
            Trace trace = traces[0];
            Assert.AreEqual("4AD2-6HT1-X845-IVXE", trace.Uuid);
            Assert.AreEqual(14, trace.Request.Headers.Count);
            Assert.AreEqual(4, trace.Request.Parameters.Count);
        }

        [TestMethod]
        public void GetTraces_Events_PropertiesMatchExpected()
        {
            string orgId = "orgId";
            string configTraceJson = @"{
                                      ""success"": true,
                                      ""messages"": [
                                        ""Organization Vulnerabilities loaded successfully""
                                      ],
                                      ""traces"": [
                                        {
                                          ""app_version_tags"": [],
                                          ""bugtracker_tickets"": [],
                                          ""category"": ""Injection"",
                                          ""closed_time"": null,
                                          ""confidence"": ""High"",
                                          ""default_severity"": ""CRITICAL"",
                                          ""events"": [
                                            {
                                              ""eventId"": 1788332,
                                              ""type"": ""Creation"",
                                              ""codeContext"": null
                                            },
                                            {
                                              ""eventId"": 1788333,
                                              ""type"": ""P2O"",
                                              ""codeContext"": null
                                            },
                                            {
                                              ""eventId"": 1788335,
                                              ""type"": ""O2R"",
                                              ""codeContext"": null
                                            },
                                            {
                                              ""eventId"": 1788336,
                                              ""type"": ""Trigger"",
                                              ""codeContext"": null
                                            }
                                          ],
                                          ""evidence"": null,
                                          ""first_time_seen"": 1461239904769,
                                          ""hasParentApp"": false,
                                          ""impact"": ""High"",
                                          ""language"": ""Java"",
                                          ""last_time_seen"": 1461239904769,
                                          ""license"": ""Licensed"",
                                          ""likelihood"": ""High"",
                                          ""organization_name"": ""Organization Test"",
                                          ""reported_to_bug_tracker"": false,
                                          ""reported_to_bug_tracker_time"": null,
                                          ""rule_name"": ""sql-injection"",
                                          ""severity"": ""Critical"",
                                          ""status"": ""Reported"",
                                          ""sub_status"": """",
                                          ""sub_title"": ""from \""account_name\"" Parameter on \""attack\"" page"",
                                          ""title"": ""SQL Injection from \""account_name\"" Parameter on \""attack\"" page"",
                                          ""total_traces_received"": 1,
                                          ""uuid"": ""4AD2-6HT1-X845-IVXE"",
                                          ""visible"": true
                                        }
                                      ],
                                      ""count"": 202,
                                      ""licensedCount"": 145,
                                      ""links"": [
                                        {
                                          ""rel"": ""nextPage"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3c646b24-48bf-4345-9dac-6933324bafb4/orgtraces/filter?expand=events&limit=1&offset=9&sort=-lastTimeSeen"",
                                          ""method"": ""GET""
                                        }
                                      ]
                                    }";

            DateTime expectedDate = new DateTime(1970, 1, 1).AddMilliseconds(1461239904769);
            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/" + orgId + "/orgtraces/filter")).Returns(
                new MemoryStream(Encoding.Unicode.GetBytes(configTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traces = teamServerClient.GetTraces(orgId);

            Assert.AreEqual(1, traces.Count);
            Trace trace = traces[0];
            Assert.AreEqual("4AD2-6HT1-X845-IVXE", trace.Uuid);
            Assert.AreEqual("SQL Injection from \"account_name\" Parameter on \"attack\" page", trace.Title);
            Assert.IsNull(trace.Request);
            Assert.AreEqual(expectedDate, trace.FirstTimeSeen);
        }

    }
}
