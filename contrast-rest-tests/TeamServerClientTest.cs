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
            string json = @"[{
                          ""name"" : ""MyTestApp"",
                          ""path"" : ""/MyTestApp"",
                          ""language"" : "".NET"",
                          ""license"" : ""Enterprise"",
                          ""views"" : 0,
                          ""links"" : [ {
                            ""rel"" : ""self"",
                            ""href"" : ""https://localhost/Contrast/api/applications/91ce4b14-353c-4e0e-8bab-663895cff574""
                          }, {
                            ""rel"" : ""traces"",
                            ""href"" : ""https://localhost/Contrast/api/traces/91ce4b14-353c-4e0e-8bab-663895cff574""
                          }, {
                            ""rel"" : ""servers"",
                            ""href"" : ""https://localhost/Contrast/api/applications/91ce4b14-353c-4e0e-8bab-663895cff574/servers""
                          }, {
                            ""rel"" : ""sitemap-activity"",
                            ""href"" : ""https://localhost/Contrast/api/applications/91ce4b14-353c-4e0e-8bab-663895cff574/sitemap/activity""
                          }, {
                            ""rel"" : ""reset-application"",
                            ""href"" : ""https://localhost/Contrast/api/applications/91ce4b14-353c-4e0e-8bab-663895cff574""
                          } ],
                          ""app-id"" : ""91ce4b14-353c-4e0e-8bab-663895cff574"",
                          ""application-code"" : null,
                          ""group-name"" : null,
                          ""platform-version"" : null,
                          ""platform-vulnerabilities"" : [ ],
                          ""last-seen"" : 1416352488000
                        }]";
            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup( client => client.GetResponseStream( "api/applications/" ) ).Returns(
                new MemoryStream( Encoding.Unicode.GetBytes(json) )
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var apps = teamServerClient.GetApplications();

            Assert.AreEqual(1, apps.Count);
            ContrastApplication app = apps[0];
            Assert.AreEqual("91ce4b14-353c-4e0e-8bab-663895cff574", app.AppID);
            Assert.AreEqual("MyTestApp", app.Name);
        }

        [TestMethod]
        public void GetLibraries_PropertiesMatchExpected()
        {
            string appId = "arbitraryId";
            string libraryJson = "[ {  \"libraryId\" : 127302,  \"filename\" : \"log4net.dll\",  \"sha1\" : \"08D926E9EFE56C69A370A30737E3346F86F7FB77\",  \"url\" : \"file:/C:\\\\inetpub\\\\wwwroot\\\\MyTestApp\\\\bin\\\\log4net.dll\",  \"version\" : \"1.2.13.0\",  \"profiled\" : false,  \"common\" : false,  \"sponsored\" : false,  \"links\" : [ {    \"rel\" : \"self\",    \"href\" : \"https://localhost/Contrast/api/applications/c744888c-96e2-4e1d-926d-c3d715cedeeb/libraries/127302\"  }, {    \"rel\" : \"cves\",    \"href\" : \"https://localhost/Contrast/api/applications/c744888c-96e2-4e1d-926d-c3d715cedeeb/libraries/127302/cves\"  }, {    \"rel\" : \"servers\",    \"href\" : \"https://localhost/Contrast/api/servers/libraries/127302\"  } ],  \"lines-of-code\" : 4515,  \"internal-date\" : \"2013-11-17\",  \"external-date\" : \"2014-11-04\",  \"class-count\" : 289,  \"used-class-count\" : 0,  \"cve-count\" : 0} ]";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/applications/" + appId + "/libraries/")).Returns(
                new MemoryStream(Encoding.Unicode.GetBytes(libraryJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var libs = teamServerClient.GetLibraries(appId);

            Assert.AreEqual(1, libs.Count);
            Library lib = libs[0];
            Assert.AreEqual("127302", lib.LibraryId);
            Assert.AreEqual("log4net.dll", lib.FileName);
        }

        [TestMethod]
        public void GetTraces_DataFlow_PropertiesMatchExpected()
        {
            string appId = "arbitraryId";
            string dataFlowTraceJson = @"[{
                                      ""uuid"" : ""S17L-WMVW-GYBY-Z00Z"",
                                      ""status"" : ""Reported"",
                                      ""platform"" : """",
                                      ""language"" : "".NET"",
                                      ""title"" : ""Cross-Site Scripting from \""input\"" Parameter on \""CharArrayVuln0.aspx\"" page"",
                                      ""likelihood"" : ""High"",
                                      ""impact"" : ""Medium"",
                                      ""confidence"" : ""High"",
                                      ""request"" : {
                                        ""protocol"" : ""http"",
                                        ""version"" : ""1.1"",
                                        ""uri"" : ""/MyTestApp/propagators/carray/CharArrayVuln0.aspx"",
                                        ""queryString"" : ""input=sourceTaintedData"",
                                        ""method"" : ""GET"",
                                        ""port"" : 80,
                                        ""headers"" : [ {
                                          ""name"" : ""Connection"",
                                          ""value"" : ""keep-alive""
                                        }, {
                                          ""name"" : ""Accept"",
                                          ""value"" : ""text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8""
                                        }, {
                                          ""name"" : ""Accept-Encoding"",
                                          ""value"" : ""gzip, deflate""
                                        }, {
                                          ""name"" : ""Accept-Language"",
                                          ""value"" : ""en-US,en;q=0.5""
                                        }, {
                                          ""name"" : ""Cookie"",
                                          ""value"" : ""ASP.NET_SessionId=tlspmetl2k4155htm41jkkjn""
                                        }, {
                                          ""name"" : ""Host"",
                                          ""value"" : ""localhost""
                                        }, {
                                          ""name"" : ""Referer"",
                                          ""value"" : ""http://localhost/MyTestApp/default.aspx""
                                        }, {
                                          ""name"" : ""User-Agent"",
                                          ""value"" : ""Mozilla/5.0 (Windows NT 6.1; WOW64; rv:35.0) Gecko/20100101 Firefox/35.0""
                                        } ],
                                        ""parameters"" : [ ],
                                        ""links"" : [ ]
                                      },
                                      ""events"" : [ {
                                        ""eventId"" : 567243,
                                        ""type"" : ""Creation"",
                                        ""codeContext"" : null
                                      }, {
                                        ""eventId"" : 567244,
                                        ""type"" : ""O2R"",
                                        ""codeContext"" : null
                                      }, {
                                        ""eventId"" : 567245,
                                        ""type"" : ""P2R"",
                                        ""codeContext"" : null
                                      }, {
                                        ""eventId"" : 567246,
                                        ""type"" : ""Trigger"",
                                        ""codeContext"" : null
                                      } ],
                                      ""links"" : [ {
                                        ""rel"" : ""self"",
                                        ""href"" : ""https://localhost/Contrast/api/traces/c744888c-96e2-4e1d-926d-c3d715cedeeb/S17L-WMVW-GYBY-Z00Z""
                                      }, {
                                        ""rel"" : ""application"",
                                        ""href"" : ""https://localhost/Contrast/api/applications/c744888c-96e2-4e1d-926d-c3d715cedeeb""
                                      } ],
                                      ""trace-id"" : 259779,
                                      ""total-traces-received"" : 1,
                                      ""last-time-seen"" : 1424269052776,
                                      ""first-time-seen"" : 1424269052776,
                                      ""sub-status"" : """",
                                      ""sub-title"" : ""from \""input\"" Parameter on \""CharArrayVuln0.aspx\"" page"",
                                      ""reported-to-bug-tracker"" : false,
                                      ""rule-name"" : ""reflected-xss"",
                                      ""severity"" : ""High""
                                    }]";
            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/traces/" + appId)).Returns(
                new MemoryStream(Encoding.Unicode.GetBytes(dataFlowTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traces = teamServerClient.GetTraces(appId);

            Assert.AreEqual(1, traces.Count);
            Trace trace = traces[0];
            Assert.AreEqual("259779", trace.TraceId);
            Assert.AreEqual("Cross-Site Scripting from \"input\" Parameter on \"CharArrayVuln0.aspx\" page", trace.Title);
            Assert.AreEqual(8, trace.Request.Headers.Count);
            Assert.AreEqual(2, trace.Links.Count);
        }

        [TestMethod]
        public void GetTraces_Config_PropertiesMatchExpected()
        {
            string appId = "arbitraryId";
            string configTraceJson = @"[{
                                      ""uuid"" : ""DW0P-4SKO-JEAK-TDOO"",
                                      ""status"" : ""Reported"",
                                      ""platform"" : """",
                                      ""language"" : "".NET"",
                                      ""title"" : ""Application Displays Detailed Error Messages in \\web.config"",
                                      ""likelihood"" : ""High"",
                                      ""impact"" : ""Low"",
                                      ""confidence"" : ""High"",
                                      ""request"" : {
                                        ""port"" : 0,
                                        ""headers"" : [ ],
                                        ""parameters"" : [ ],
                                        ""links"" : [ ]
                                      },
                                      ""events"" : [ ],
                                      ""links"" : [ {
                                        ""rel"" : ""self"",
                                        ""href"" : ""https://localhost/Contrast/api/traces/c744888c-96e2-4e1d-926d-c3d715cedeeb/DW0P-4SKO-JEAK-TDOO""
                                      }, {
                                        ""rel"" : ""application"",
                                        ""href"" : ""https://localhost/Contrast/api/applications/c744888c-96e2-4e1d-926d-c3d715cedeeb""
                                      } ],
                                      ""trace-id"" : 259676,
                                      ""total-traces-received"" : 1,
                                      ""last-time-seen"" : 1424268996169,
                                      ""first-time-seen"" : 1424268996169,
                                      ""sub-status"" : """",
                                      ""sub-title"" : ""in \\web.config"",
                                      ""reported-to-bug-tracker"" : false,
                                      ""rule-name"" : ""custom-errors-off"",
                                      ""severity"" : ""Medium""
                                    }]";

            DateTime expectedDate = new DateTime(1970, 1, 1).AddMilliseconds(1424268996169);
            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/traces/" + appId)).Returns(
                new MemoryStream(Encoding.Unicode.GetBytes(configTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traces = teamServerClient.GetTraces(appId);

            Assert.AreEqual(1, traces.Count);
            Trace trace = traces[0];
            Assert.AreEqual("259676", trace.TraceId);
            Assert.AreEqual("Application Displays Detailed Error Messages in \\web.config", trace.Title);
            Assert.AreEqual(0, trace.Request.Parameters.Count);
            Assert.AreEqual(expectedDate, trace.FirstTimeSeen);
        }

    }
}
