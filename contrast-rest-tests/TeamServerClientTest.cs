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
using Newtonsoft.Json;

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

        [TestMethod, ExpectedException(typeof(ArgumentException))]
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
                                    ""rel"": ""reset"",
                                    ""href"": ""https://localhost/Contrast/api/ng/3c646b24-48bf-4345-9dac-6933324bafb4/applications/2b75b619-8a37-463e-b605-bd8f340d03aa/reset"",
                                    ""method"": ""POST""
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
                new MemoryStream(Encoding.UTF8.GetBytes(json))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var response = teamServerClient.GetApplications("orgId");
            var apps = response.Applications;

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
                new MemoryStream(Encoding.UTF8.GetBytes(libraryJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var libsResponse = teamServerClient.GetLibraries("orgId", appId);
            var libs = libsResponse.Libraries;

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
                new MemoryStream(Encoding.UTF8.GetBytes(dataFlowTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traceResponse = teamServerClient.GetTraces("orgId");
            var traces = traceResponse.Traces;

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

            DateTime expectedDate = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(1461239904769);
            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/" + orgId + "/orgtraces/filter")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(configTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traceResponse = teamServerClient.GetTraces(orgId);
            var traces = traceResponse.Traces;

            Assert.AreEqual(1, traces.Count);
            Trace trace = traces[0];
            Assert.AreEqual("4AD2-6HT1-X845-IVXE", trace.Uuid);
            Assert.AreEqual("SQL Injection from \"account_name\" Parameter on \"attack\" page", trace.Title);
            Assert.IsNull(trace.Request);
            Assert.AreEqual(expectedDate, trace.FirstTimeSeen);
        }

        [TestMethod]
        public void GetTracesByApp_PropertiesMatchExpected()
        {
            string orgId = "orgId";
            string appId = "appId";
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

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/" + orgId + "/traces/" + appId + "/filter")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(configTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traceResponse = teamServerClient.GetApplicationTraces(orgId, appId);
            var traces = traceResponse.Traces;

            Assert.AreEqual(1, traces.Count);
            Trace trace = traces[0];
            Assert.AreEqual("4AD2-6HT1-X845-IVXE", trace.Uuid);
            Assert.AreEqual("Critical", trace.Severity);
            Assert.AreEqual("sql-injection", trace.RuleName);
        }

        [TestMethod]
        public void GetTracesByServer_PropertiesMatchExpected()
        {
            string orgId = "orgId";
            long serverId = 123456;
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

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/" + orgId + "/servertraces/" + serverId + "/filter")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(configTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traceResponse = teamServerClient.GetServerTraces(orgId, serverId);
            var traces = traceResponse.Traces;

            Assert.AreEqual(1, traces.Count);
            Trace trace = traces[0];
            Assert.AreEqual("4AD2-6HT1-X845-IVXE", trace.Uuid);
            Assert.AreEqual("CRITICAL", trace.DefaultSeverity);
            Assert.IsFalse(trace.HasParentApp);
        }

        [TestMethod]
        public void GetTraceEventsSummary_PropertiesMatched()
        {
            string eventSummaryJson = @"{
                                      ""success"": true,
                                      ""messages"": [
                                        ""Trace Events Summary loaded successfully""
                                      ],
                                      ""showEvidence"": false,
                                      ""showEvents"": true,
                                      ""events"": [
                                        {
                                          ""id"": ""1762213"",
                                          ""important"": true,
                                          ""type"": ""Creation"",
                                          ""description"": ""Dangerous Data Received"",
                                          ""extraDetails"": null,
                                          ""codeView"": {
                                            ""lines"": [
                                              {
                                                ""fragments"": [
                                                  {
                                                    ""type"": ""NORMAL_CODE"",
                                                    ""value"": ""str = facade.getParameter(""
                                                  },
                                                  {
                                                    ""type"": ""CODE_STRING"",
                                                    ""value"": ""&quot;name&quot;""
                                                  },
                                                  {
                                                    ""type"": ""NORMAL_CODE"",
                                                    ""value"": "")""
                                                  }
                                                ],
                                                ""text"": ""str = facade.getParameter(&quot;name&quot;)""
                                              }
                                            ],
                                            ""nested"": false
                                          },
                                          ""probableStartLocationView"": {
                                            ""lines"": [
                                              {
                                                ""fragments"": [
                                                  {
                                                    ""type"": ""STACKTRACE_LINE"",
                                                    ""value"": ""getValue() @ JasperELResolver.java:104""
                                                  }
                                                ],
                                                ""text"": ""getValue() @ JasperELResolver.java:104""
                                              }
                                            ],
                                            ""nested"": false
                                          },
                                          ""dataView"": {
                                            ""lines"": [
                                              {
                                                ""fragments"": [
                                                  {
                                                    ""type"": ""TAINT_VALUE"",
                                                    ""value"": ""Hi""
                                                  }
                                                ],
                                                ""text"": ""{{#taint}}Hi{{/taint}}""
                                              }
                                            ],
                                            ""nested"": false
                                          },
                                          ""collapsedEvents"": [],
                                          ""dupes"": 0
                                        },
                                        {
                                          ""id"": ""1762214"",
                                          ""important"": true,
                                          ""type"": ""Trigger"",
                                          ""description"": ""Untrusted Data Sent in HTTP Response"",
                                          ""extraDetails"": null,
                                          ""codeView"": {
                                            ""lines"": [
                                              {
                                                ""fragments"": [
                                                  {
                                                    ""type"": ""NORMAL_CODE"",
                                                    ""value"": ""impl.write(""
                                                  },
                                                  {
                                                    ""type"": ""CODE_STRING"",
                                                    ""value"": ""&quot;Hi&quot;""
                                                  },
                                                  {
                                                    ""type"": ""NORMAL_CODE"",
                                                    ""value"": "",0,2)""
                                                  }
                                                ],
                                                ""text"": ""impl.write(&quot;Hi&quot;,0,2)""
                                              }
                                            ],
                                            ""nested"": false
                                          },
                                          ""probableStartLocationView"": {
                                            ""lines"": [
                                              {
                                                ""fragments"": [
                                                  {
                                                    ""type"": ""STACKTRACE_LINE"",
                                                    ""value"": ""out() @ OutSupport.java:199""
                                                  }
                                                ],
                                                ""text"": ""out() @ OutSupport.java:199""
                                              }
                                            ],
                                            ""nested"": false
                                          },
                                          ""dataView"": {
                                            ""lines"": [
                                              {
                                                ""fragments"": [
                                                  {
                                                    ""type"": ""TAINT_VALUE"",
                                                    ""value"": ""Hi""
                                                  }
                                                ],
                                                ""text"": ""{{#taint}}Hi{{/taint}}""
                                              }
                                            ],
                                            ""nested"": false
                                          },
                                          ""collapsedEvents"": [],
                                          ""dupes"": 0
                                        }
                                      ]
                                    }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/traces/traceId/events/summary")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(eventSummaryJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var summaryResponse = teamServerClient.GetEventsSummary("orgId", "traceId");

            Assert.AreEqual(2, summaryResponse.Events.Count);
            var summary = summaryResponse.Events[0];

            Assert.AreEqual("1762213", summary.Id);
            Assert.AreEqual("Dangerous Data Received", summary.Description);
            Assert.IsTrue(summary.Important);
            Assert.AreEqual("Creation", summary.Type);
            Assert.AreEqual(1, summary.CodeView.Lines.Count);
        }

        [TestMethod]
        public void GetTraceEventDetails_PropertiesAndStacktraceExpected()
        {
            string detailsJson = @"{
                                ""success"": true,
                                ""messages"": [
                                ""Trace Event Details loaded successfully""
                                ],
                                ""event"": {
                                ""object"": ""org.apache.catalina.connector.RequestFacade@53af461a"",
                                ""method"": ""getParameter(java.lang.String)"",
                                ""parameters"": [
                                    {
                                    ""parameter"": ""name"",
                                    ""tracked"": false
                                    }
                                ],
                                ""stacktraces"": [
                                    {
                                    ""description"": ""javax.servlet.jsp.el.ImplicitObjectELResolver$ScopeManager$7.getAttribute(ImplicitObjectELResolver.java:410)"",
                                    ""type"": ""common"",
                                    ""stackFrameIndex"": 0
                                    },
                                    {
                                    ""description"": ""javax.servlet.jsp.el.ImplicitObjectELResolver$ScopeManager$7.getAttribute(ImplicitObjectELResolver.java:402)"",
                                    ""type"": ""common"",
                                    ""stackFrameIndex"": 1
                                    },
                                    {
                                    ""description"": ""javax.servlet.jsp.el.ImplicitObjectELResolver$ScopeMap.get(ImplicitObjectELResolver.java:601)"",
                                    ""type"": ""common"",
                                    ""stackFrameIndex"": 2
                                    },
                                    {
                                    ""description"": ""javax.el.MapELResolver.getValue(MapELResolver.java:52)"",
                                    ""type"": ""common"",
                                    ""stackFrameIndex"": 3
                                    },
                                    {
                                    ""description"": ""org.apache.jasper.el.JasperELResolver.getValue(JasperELResolver.java:104)"",
                                    ""type"": ""common"",
                                    ""stackFrameIndex"": 4
                                    },
                                    {
                                    ""description"": ""org.apache.el.parser.AstValue.getValue(AstValue.java:183)"",
                                    ""type"": ""common"",
                                    ""stackFrameIndex"": 5
                                    },
                                    {
                                    ""description"": ""org.apache.el.ValueExpressionImpl.getValue(ValueExpressionImpl.java:184)"",
                                    ""type"": ""common"",
                                    ""stackFrameIndex"": 6
                                    }
                                ],
                                ""class"": ""org.apache.catalina.connector.RequestFacade"",
                                ""object_tracked"": false,
                                ""return"": ""Hi"",
                                ""return_tracked"": false,
                                ""last_custom_frame"": 9
                                }
                            }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/traces/traceId/events/12/details")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(detailsJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var detailsResponse = teamServerClient.GetTraceEventDetail("orgId", "traceId", 12);

            Assert.AreEqual("org.apache.catalina.connector.RequestFacade", detailsResponse.Event.ClassName);
            Assert.AreEqual("getParameter(java.lang.String)", detailsResponse.Event.Method);
            Assert.AreEqual(7, detailsResponse.Event.StackTraces.Count);
        }

        [TestMethod]
        public void GetTraceHttpRequest_PropertiesExpected()
        {
            string httpJson = @"{
                              ""success"": true,
                              ""messages"": [
                                ""Trace HTTP Request loaded successfully""
                              ],
                              ""http_request"": {
                                ""text"": ""POST /ticketbook/xss.jsp HTTP/1.0\nContent-Length: 15\nReferer: http://localhost/ticketbook/xss.jsp\nAccept-Language: en-US,en;q=0.8\nCookie: JSESSIONID=E4AE5E48864216D33BAC490A159F0A6A; hudson_auto_refresh=false; JSESSIONID=6378B7D127B2BB06E3A7B4E3A1F6F256\nOrigin: http://localhost:9090\nAccept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\nHost: localhost:9090\nUpgrade-Insecure-Requests: 1\nConnection: keep-alive\nContent-Type: application/x-www-form-urlencoded\nCache-Control: max-age=0\nAccept-Encoding: gzip, deflate\nUser-Agent: Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36\n\nname=Hi&submit="",
                                ""formattedText"": ""POST /ticketbook/xss.jsp HTTP/1.0{{{nl}}}Content-Length: 15{{{nl}}}Referer: http://localhost/ticketbook/xss.jsp{{{nl}}}Accept-Language: en-US,en;q=0.8{{{nl}}}Cookie: JSESSIONID=E4AE5E48864216D33BAC490A159F0A6A; hudson_auto_refresh=false; JSESSIONID=6378B7D127B2BB06E3A7B4E3A1F6F256{{{nl}}}Origin: http://localhost:9090{{{nl}}}Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8{{{nl}}}Host: localhost:9090{{{nl}}}Upgrade-Insecure-Requests: 1{{{nl}}}Connection: keep-alive{{{nl}}}Content-Type: application/x-www-form-urlencoded{{{nl}}}Cache-Control: max-age=0{{{nl}}}Accept-Encoding: gzip, deflate{{{nl}}}User-Agent: Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36{{{nl}}}{{{nl}}}name={{#taint}}Hi{{/taint}}&submit="",
                                ""formattedTextVariables"": {}
                              }
                            }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/traces/traceId/httprequest")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(httpJson))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var httpRequestResponse = teamServerClient.GetTraceHttpRequest("orgId", "traceId");

            Assert.IsTrue(httpRequestResponse.HttpRequest.Text.Contains("POST /ticketbook/xss.jsp HTTP/1.0\nContent-Length: 15\nReferer:"));
            Assert.IsTrue(httpRequestResponse.HttpRequest.FormattedText.Contains("POST /ticketbook/xss.jsp HTTP/1.0{{{nl}}}Content-Length: 15{{{nl}}}Referer:"));
        }

        [TestMethod]
        public void GetTraceStory_PropertiesMatch()
        {
            string storyJson = @"{
                                  ""success"": true,
                                  ""messages"": [
                                    ""Trace Story loaded successfully""
                                  ],
                                  ""story"": {
                                    ""traceId"": ""KVX4-GT1J-ZUR2-OXZN"",
                                    ""chapters"": [
                                      {
                                        ""type"": ""source"",
                                        ""introText"": ""We tracked the following data from \""name\"" Parameter:"",
                                        ""introTextFormat"": ""We tracked the following data from \""{{source0}}\"" Parameter:"",
                                        ""introTextVariables"": {
                                          ""source0"": ""name""
                                        },
                                        ""body"": ""POST /ticketbook/xss.jsp\n\nname=Hi&submit="",
                                        ""bodyFormat"": ""POST /ticketbook/xss.jsp{{{nl}}}{{{nl}}}name={{#taint}}Hi{{/taint}}&submit="",
                                        ""bodyFormatVariables"": {}
                                      },
                                      {
                                        ""type"": ""location"",
                                        ""introText"": ""...which was accessed within the following code:"",
                                        ""introTextFormat"": ""...which was accessed within the following code:"",
                                        ""introTextVariables"": {},
                                        ""body"": ""xss.jsp, line 57"",
                                        ""bodyFormat"": ""{{jsp}}, line {{line}}"",
                                        ""bodyFormatVariables"": {
                                          ""jsp"": ""xss.jsp"",
                                          ""line"": ""57""
                                        }
                                      },
                                      {
                                        ""type"": ""dataflow"",
                                        ""introText"": ""...and observed going into the HTTP response below, without validation or encoding:"",
                                        ""introTextFormat"": ""...and observed going into the HTTP response below, without validation or encoding:"",
                                        ""introTextVariables"": {},
                                        ""body"": ""Hi"",
                                        ""bodyFormat"": ""{{#taint}}Hi{{/taint}}"",
                                        ""bodyFormatVariables"": {},
                                        ""vector"": null
                                      }
                                    ],
                                    ""risk"": {
                                      ""text"": ""The vulnerable code for this issue is in xss.jsp, line 57.Cross-Site Scripting (XSS) enables attackers to inject client-side script into trusted Web pages viewed by other users. They can use this malicious script to force users to silently perform actions that benefit the attacker, like sending them their account or session credentials. Any view code that takes data controlled by a user and puts it on the page without validation or encoding is likely to be vulnerable to this attack."",
                                      ""formattedText"": ""{{#paragraph}}The vulnerable code for this issue is in xss.jsp, line 57.{{/paragraph}}{{#paragraph}}Cross-Site Scripting (XSS) enables attackers to inject client-side script into trusted Web pages viewed by other users. They can use this malicious script to force users to silently perform actions that benefit the attacker, like sending them their account or session credentials. Any view code that takes data controlled by a user and puts it on the page without validation or encoding is likely to be vulnerable to this attack.{{/paragraph}}"",
                                      ""formattedTextVariables"": {}
                                    }
                                  },
                                  ""custom_risk"": {
                                    ""text"": ""This is a test to see if this Description shows up in the rule."",
                                    ""formattedText"": ""This is a test to see if this Description shows up in the rule."",
                                    ""formattedTextVariables"": {}
                                  }
                                }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/traces/traceId/story")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(storyJson))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var storyResponse = teamServerClient.GetTraceStory("orgId", "traceId");

            Assert.AreEqual(3, storyResponse.Story.Chapters.Count);

            var chapter = storyResponse.Story.Chapters[0];
            Assert.AreEqual(ChapterType.source, chapter.Type);
            Assert.AreEqual("We tracked the following data from \"name\" Parameter:", chapter.IntroText);

            chapter = storyResponse.Story.Chapters[1];
            Assert.AreEqual(ChapterType.location, chapter.Type);
            Assert.AreEqual("...which was accessed within the following code:", chapter.IntroTextFormat);

            Assert.AreEqual(0, storyResponse.Story.Risk.FormattedTextVariables.Count);
        }

        [TestMethod]
        public void GetTraceFilterSubfilters_PropertiesMatched()
        {
            string storyJson = @"{
                                  ""success"": true,
                                  ""messages"": [
                                    ""Trace Filter severity loaded successfully""
                                  ],
                                  ""filters"": [
                                    {
                                      ""keycode"": ""CRITICAL"",
                                      ""label"": ""Critical"",
                                      ""count"": 4,
                                      ""links"": [
                                        {
                                          ""rel"": ""self"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3dd46b24-48bf-4345-9dac-6933324bafb4/traces/86b59798-f317-4f1c-8aa5-4d4187c2e999/filter"",
                                          ""method"": ""GET""
                                        }
                                      ],
                                      ""new_group"": false
                                    },
                                    {
                                      ""keycode"": ""HIGH"",
                                      ""label"": ""High"",
                                      ""count"": 5,
                                      ""links"": [
                                        {
                                          ""rel"": ""self"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3dd46b24-48bf-4345-9dac-6933324bafb4/traces/86b59798-f317-4f1c-8aa5-4d4187c2e999/filter"",
                                          ""method"": ""GET""
                                        }
                                      ],
                                      ""new_group"": false
                                    },
                                    {
                                      ""keycode"": ""MEDIUM"",
                                      ""label"": ""Medium"",
                                      ""count"": 33,
                                      ""links"": [
                                        {
                                          ""rel"": ""self"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3dd46b24-48bf-4345-9dac-6933324bafb4/traces/86b59798-f317-4f1c-8aa5-4d4187c2e999/filter"",
                                          ""method"": ""GET""
                                        }
                                      ],
                                      ""new_group"": false
                                    },
                                    {
                                      ""keycode"": ""LOW"",
                                      ""label"": ""Low"",
                                      ""count"": 3,
                                      ""links"": [
                                        {
                                          ""rel"": ""self"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3dd46b24-48bf-4345-9dac-6933324bafb4/traces/86b59798-f317-4f1c-8aa5-4d4187c2e999/filter"",
                                          ""method"": ""GET""
                                        }
                                      ],
                                      ""new_group"": false
                                    },
                                    {
                                      ""keycode"": ""NOTE"",
                                      ""label"": ""Note"",
                                      ""count"": 12,
                                      ""links"": [
                                        {
                                          ""rel"": ""self"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3dd46b24-48bf-4345-9dac-6933324bafb4/traces/86b59798-f317-4f1c-8aa5-4d4187c2e999/filter"",
                                          ""method"": ""GET""
                                        }
                                      ],
                                      ""new_group"": false
                                    }
                                  ]
                                }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/orgtraces/filter/severity/listing")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(storyJson))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var filterResponse = teamServerClient.GetTraceFilterSubfilters("orgId", TraceFilterType.severity, null);

            Assert.AreEqual(5, filterResponse.Filters.Count);
            var filter = filterResponse.Filters[1];

            Assert.AreEqual("HIGH", filter.Keycode);
            Assert.AreEqual("High", filter.Label);
            Assert.AreEqual(5, filter.Count);

            filter = filterResponse.Filters[3];
            Assert.AreEqual(3, filter.Count);
            Assert.AreEqual("Low", filter.Label);
        }

        [TestMethod]
        public void GetApplicationTraceFilterSubfilters_PropertiesMatched()
        {
            string storyJson = @"{
                                  ""success"": true,
                                  ""messages"": [
                                    ""Trace Filter servers loaded successfully""
                                  ],
                                  ""filters"": [
                                    {
                                      ""keycode"": ""855"",
                                      ""label"": ""MyServer (C:\\Windows\\system32\\inetsrv\\w3wp.exe)"",
                                      ""details"": {
                                        ""server_id"": 855,
                                        ""name"": ""MyServer"",
                                        ""hostname"": ""MyServer"",
                                        ""path"": ""C:\\Windows\\system32\\inetsrv\\w3wp.exe"",
                                        ""environment"": ""QA""
                                      },
                                      ""count"": 55,
                                      ""links"": [
                                        {
                                          ""rel"": ""self"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3dd46b24-48bf-4345-9dac-6933324bafb4/traces/86b59798-f317-4f1c-8aa5-4d4187c2e999/filter"",
                                          ""method"": ""GET""
                                        }
                                      ],
                                      ""new_group"": false
                                    },
                                    {
                                      ""keycode"": ""1561"",
                                      ""label"": ""WIN-Plus (\\system32\\inetsrv\\w3wp.exe)"",
                                      ""details"": {
                                        ""server_id"": 1561,
                                        ""name"": ""WIN-Plus"",
                                        ""hostname"": ""WIN-Plus"",
                                        ""path"": ""\\system32\\inetsrv\\w3wp.exe"",
                                        ""environment"": ""QA""
                                      },
                                      ""count"": 9,
                                      ""links"": [
                                        {
                                          ""rel"": ""self"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3dd46b24-48bf-4345-9dac-6933324bafb4/traces/86b59798-f317-4f1c-8aa5-4d4187c2e999/filter"",
                                          ""method"": ""GET""
                                        }
                                      ],
                                      ""new_group"": false
                                    }
                                  ]
                                }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/traces/appId/filter/servers/listing")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(storyJson))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var filterResponse = teamServerClient.GetApplicationTraceFilterSubfilters("orgId", "appId", TraceFilterType.servers, null);

            Assert.AreEqual(2, filterResponse.Filters.Count);
            var filter = filterResponse.Filters[1];

            Assert.AreEqual("1561", filter.Keycode);
            Assert.AreEqual("WIN-Plus (\\system32\\inetsrv\\w3wp.exe)", filter.Label);
            Assert.AreEqual(9, filter.Count);
        }

        [TestMethod]
        public void GetServerTraceFilterSubfilters_PropertiesMatched()
        {
            string storyJson = @"{
                                  ""success"": true,
                                  ""messages"": [
                                    ""Vulnerability Filter modules loaded successfully""
                                  ],
                                  ""filters"": [
                                    {
                                      ""keycode"": ""86b59798-f317-4f1c-8aa5-4d4187c2e999"",
                                      ""label"": ""dotnet-webgoat [.NET]"",
                                      ""details"": {
                                        ""master"": false,
                                        ""child"": false,
                                        ""app_id"": ""86b59798-f317-4f1c-8aa5-4d4187c2e999"",
                                        ""name"": ""dotnet-webgoat"",
                                        ""parent_app_id"": null,
                                        ""total_modules"": 1,
                                        ""language"": "".NET"",
                                        ""context_path"": null,
                                        ""last_seen"": 0
                                      },
                                      ""count"": 9,
                                      ""links"": [
                                        {
                                          ""rel"": ""self"",
                                          ""href"": ""https://localhost/Contrast/api/ng/3dd46b24-48bf-4345-9dac-6933324bafb4/servertraces/1458/filter"",
                                          ""method"": ""GET""
                                        }
                                      ],
                                      ""new_group"": false
                                    }
                                  ]
                                }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/servertraces/1111/filter/modules/listing")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(storyJson))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var filterResponse = teamServerClient.GetServerTraceFilterSubfilters("orgId", 1111, TraceFilterType.modules, null);

            Assert.AreEqual(1, filterResponse.Filters.Count);
            var filter = filterResponse.Filters[0];

            Assert.AreEqual("86b59798-f317-4f1c-8aa5-4d4187c2e999", filter.Keycode);
            Assert.AreEqual("dotnet-webgoat [.NET]", filter.Label);
            Assert.AreEqual(9, filter.Count);
        }

        [TestMethod]
        public void GetTraceRecommendation_PropertiesMatch()
        {
            string recommendationJson = @"{
                                    ""success"": true,
                                    ""messages"": [
                                        ""Trace Recommendation loaded successfully""
                                      ],
                                      ""recommendation"": {
                                        ""text"": ""If the input or output of the parameter can be removed, it should. Otherwise, encode the parameter using the appropriate output encoding technique, as defined in the OWASP Cross Site Scripting Prevention Cheat Sheet (referenced below). For most scenarios, simple HTML encoding with one of these solutions should defuse any attacks:Using JSP EL: \n&lt;c:out value=\""${userControlledValue}\""/&gt; \n\n ... or ... \n\n ${fn:escapeXml(userControlledValue)}Using Spring tags: \nWe detected this application is using Spring MVC. Here's how you can output text safely with the Spring tag library: \n&lt;div&gt; \n\t&lt;spring:escapeBody htmlEscape=\""true\""&gt;${userControlledValue}&lt;/spring:escapeBody&gt; // for data in HTML context&lt;/div&gt; \n&lt;script&gt; \n&lt;!-- \nvar str = \""&lt;spring:escapeBody javaScriptEscape=\""true\""&gt;${userControlledValue}&lt;/spring:escapeBody&gt;\""; // for data in JavaScript context \n--&gt; \n&lt;/script&gt;Using https://www.owasp.org/index.php/ESAPI$$LINK_DELIM$$ESAPI: \nvar str = \""&lt;esapi:encodeForJavaScript&gt;${userControlledValue}&lt;/esapi:encodeForJavaScript&gt;\""; An overwhelming majority of the time, one of the above solutions will work for your situation. However, if you'd like to safely put untrusted user input into other contexts besides HTML or JavaScript, consider the https://www.owasp.org/index.php/ESAPI$$LINK_DELIM$$ESAPI. It has encoding  functions and complementary JSP tags for all the different contexts as described by the https://www.owasp.org/index.php/XSS_(Cross_Site_Scripting)_Prevention_Cheat_Sheet$$LINK_DELIM$$OWASP XSS Prevention CheatSheet referenced below. \n Input validation helps, but many times the characters used in XSS attacks are necessary for the application's purpose. So, while we always recommend whitelist input validation, we recognize that it's not always possible to use this as a defense against XSS. "",
                                        ""formattedText"": ""{{#paragraph}}If the input or output of the parameter can be removed, it should. Otherwise, encode the parameter using the appropriate output encoding technique, as defined in the OWASP Cross Site Scripting Prevention Cheat Sheet (referenced below). For most scenarios, simple HTML encoding with one of these solutions should defuse any attacks:{{/paragraph}}{{#header}}Using JSP EL:{{/header}} \n{{#javaBlock}}&lt;c:out value=\""${userControlledValue}\""/&gt; \n\n ... or ... \n\n ${fn:escapeXml(userControlledValue)}{{/javaBlock}}{{#header}}Using Spring tags:{{/header}} \n{{#paragraph}}We detected this application is using Spring MVC. Here's how you can output text safely with the Spring tag library:{{/paragraph}} \n{{#javaBlock}}&lt;div&gt; \n\t&lt;spring:escapeBody htmlEscape=\""true\""&gt;${userControlledValue}&lt;/spring:escapeBody&gt; // for data in HTML context&lt;/div&gt; \n&lt;script&gt; \n&lt;!-- \nvar str = \""&lt;spring:escapeBody javaScriptEscape=\""true\""&gt;${userControlledValue}&lt;/spring:escapeBody&gt;\""; // for data in JavaScript context \n--&gt; \n&lt;/script&gt;{{/javaBlock}}{{#header}}Using {{#link}}https://www.owasp.org/index.php/ESAPI$$LINK_DELIM$$ESAPI{{/link}}:{{/header}} \n{{#javaBlock}}var str = \""&lt;esapi:encodeForJavaScript&gt;${userControlledValue}&lt;/esapi:encodeForJavaScript&gt;\"";{{/javaBlock}} {{#paragraph}}An overwhelming majority of the time, one of the above solutions will work for your situation. However, if you'd like to safely put untrusted user input into other contexts besides HTML or JavaScript, consider the {{#link}}https://www.owasp.org/index.php/ESAPI$$LINK_DELIM$$ESAPI{{/link}}. It has encoding  functions and complementary JSP tags for all the different contexts as described by the {{#link}}https://www.owasp.org/index.php/XSS_(Cross_Site_Scripting)_Prevention_Cheat_Sheet$$LINK_DELIM$$OWASP XSS Prevention CheatSheet{{/link}} referenced below.{{/paragraph}} \n {{#paragraph}}Input validation helps, but many times the characters used in XSS attacks are necessary for the application's purpose. So, while we always recommend whitelist input validation, we recognize that it's not always possible to use this as a defense against XSS.{{/paragraph}} "",
                                        ""formattedTextVariables"": {}
                                      },
                                      ""owasp"": ""https://www.owasp.org/index.php/XSS_(Cross_Site_Scripting)_Prevention_Cheat_Sheet"",
                                      ""cwe"": ""http://cwe.mitre.org/data/definitions/79.html"",
                                      ""custom_recommendation"": {
                                        ""text"": ""SDF-1 is much better than Voltron."",
                                        ""formattedText"": ""SDF-1 is much better than Voltron."",
                                        ""formattedTextVariables"": {}
                                      },
                                      ""rule_references"": {
                                        ""text"": ""https://www.owasp.org/index.php/Top_10_2013-A3-Cross-Site_Scripting_(XSS){{{nl}}}"",
                                        ""formattedText"": ""https://www.owasp.org/index.php/Top_10_2013-A3-Cross-Site_Scripting_(XSS){{{nl}}}"",
                                        ""formattedTextVariables"": {}
                                      },
                                      ""custom_rule_references"": {
                                        ""text"": ""test reference{{{nl}}}"",
                                        ""formattedText"": ""test reference{{{nl}}}"",
                                        ""formattedTextVariables"": {}
                                      }
                                    }";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/ng/orgId/traces/traceId/recommendation")).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(recommendationJson))
                );
            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);
            var response = teamServerClient.GetTraceRecommendation("orgId", "traceId");

            Assert.AreEqual(response.Owasp, "https://www.owasp.org/index.php/XSS_(Cross_Site_Scripting)_Prevention_Cheat_Sheet");
            Assert.AreEqual(response.CustomRuleReferences.Text, "test reference{{{nl}}}");
            Assert.AreEqual(response.CustomRecommendation.FormattedText, "SDF-1 is much better than Voltron.");
        }
    }
}
