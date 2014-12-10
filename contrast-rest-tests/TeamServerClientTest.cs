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
            string json = "[{  \"appID\" : \"18c3d43e-90db-4831-8672-e867ab4d91ea\",  \"name\" : \"MyTestApp\",  \"shortName\" : null,  \"groupName\" : null,  \"path\" : \"/MyTestApp\",  \"language\" : \".NET\",  \"license\" : \"Enterprise\",  \"platformVersion\" : null,  \"platformVulnerabilities\" : [ ],  \"lastSeen\" : 1409324167000,  \"views\" : 0,  \"technologies\" : [ \"HTML5\", \"Bootstrap\", \"jQuery\", \"Flash\" ],  \"links\" : [ {    \"rel\" : \"self\",    \"href\" : \"https://localhost/Contrast/api/applications/18c3d43e-90db-4831-8672-e867ab4d91ea\"  }, {    \"rel\" : \"traces\",    \"href\" : \"https://localhost/Contrast/api/traces/18c3d43e-90db-4831-8672-e867ab4d91ea\"  }, {    \"rel\" : \"servers\",    \"href\" : \"https://localhost/Contrast/api/applications/18c3d43e-90db-4831-8672-e867ab4d91ea/servers\"  }, {    \"rel\" : \"sitemap-activity\",    \"href\" : \"https://localhost/Contrast/api/applications/18c3d43e-90db-4831-8672-e867ab4d91ea/sitemap/activity\"  }, {    \"rel\" : \"reset-application\",    \"href\" : \"https://localhost/Contrast/api/applications/18c3d43e-90db-4831-8672-e867ab4d91ea\"  } ]}]";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup( client => client.GetResponseStream( "api/applications/" ) ).Returns(
                new MemoryStream( Encoding.Unicode.GetBytes(json) )
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var apps = teamServerClient.GetApplications();

            Assert.AreEqual(1, apps.Count);
            ContrastApplication app = apps[0];
            Assert.AreEqual("18c3d43e-90db-4831-8672-e867ab4d91ea", app.AppID);
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
            string dataFlowTraceJson = "[ {  \"traceId\" : 217295,  \"uuid\" : \"FCO1-4LYY-CJFC-0QQ1\",  \"totalTracesRecieved\" : 1,  \"lastTimeSeen\" : 1417465343329,  \"firstTimeSeen\" : 1417465343329,  \"status\" : \"Reported\",  \"subStatus\" : \"\",  \"platform\" : \"\",  \"language\" : \".NET\",  \"title\" : \"XSS from \\\"input\\\" Parameter on \\\"StringConcatVuln1.aspx\\\" page\",  \"subTitle\" : \"from \\\"input\\\" Parameter on \\\"StringConcatVuln1.aspx\\\" page\",  \"reportedToBugTracker\" : false,  \"ruleName\" : \"reflected-xss\",  \"severity\" : \"High\",  \"likelihood\" : \"High\",  \"impact\" : \"Medium\",  \"confidence\" : \"High\",  \"request\" : {    \"protocol\" : \"http\",    \"version\" : \"1.1\",    \"uri\" : \"/MyTestApp/propagators/stringconcat/StringConcatVuln1.aspx\",    \"queryString\" : \"input=stringConcatTaintedData\",    \"method\" : \"GET\",    \"port\" : 80,    \"headers\" : [ {      \"name\" : \"Connection\",      \"value\" : \"keep-alive\"    }, {      \"name\" : \"Accept\",      \"value\" : \"text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\"    }, {      \"name\" : \"Accept-Encoding\",      \"value\" : \"gzip, deflate, sdch\"    }, {      \"name\" : \"Accept-Language\",      \"value\" : \"en-US,en;q=0.8\"    }, {      \"name\" : \"Cookie\",      \"value\" : \"ASP.NET_SessionId=ogaafeai5cjvmjce4mhgsbpz\"    }, {      \"name\" : \"Host\",      \"value\" : \"localhost\"    }, {      \"name\" : \"Referer\",      \"value\" : \"http://localhost/MyTestApp/\"    }, {      \"name\" : \"User-Agent\",      \"value\" : \"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36\"    } ],    \"parameters\" : [ {      \"name\" : \"__VIEWSTATE\",      \"value\" : \"hO51OK0+QewvbgvYrY0kboOXIr18p1KY3+vy8bzXCa2uZNFibT1rs7nO5zWCN+8mX6tf8xhzKbpWwxuJ3IBvypW/RJY+L0LxQkyNsotCxdg=\"    }, {      \"name\" : \"__EVENTVALIDATION\",      \"value\" : \"PAhG+LpBIaH/iRpbudoJDJz8NYuubJP40xvYR1ftTCPGBj91ZQDmVkLGkvoxIWVzOJUqotz4vEsE4UaBsinJrlKGQpIqCRiThcsnHZ1o7X7aafcPlAKJmKVsZRlYSKXutiHwuKZegMek5uzEriKNfg==\"    }, {      \"name\" : \"input\",      \"value\" : \"taintedFormData\"    }, {      \"name\" : \"Button1\",      \"value\" : \"Button\"    } ],    \"links\" : [ {    \"rel\" : \"self\",    \"href\" : \"https://localhost/Contrast/api/traces/c744888c-96e2-4e1d-926d-c3d715cedeeb/4487-7HA7-4F08-87G9\"  }, {    \"rel\" : \"application\",    \"href\" : \"https://localhost/Contrast/api/applications/c744888c-96e2-4e1d-926d-c3d715cedeeb\"  } ]  },  \"events\" : [ {    \"eventId\" : 481819,    \"type\" : \"Creation\",    \"codeContext\" : null  }, {    \"eventId\" : 481820,    \"type\" : \"P2R\",    \"codeContext\" : null  }, {    \"eventId\" : 481821,    \"type\" : \"Trigger\",    \"codeContext\" : null  } ],  \"links\" : [ {    \"rel\" : \"self\",    \"href\" : \"https://localhost/Contrast/api/traces/c744888c-96e2-4e1d-926d-c3d715cedeeb/FCO1-4LYY-CJFC-0QQ1\"  }, {    \"rel\" : \"application\",    \"href\" : \"https://localhost/Contrast/api/applications/c744888c-96e2-4e1d-926d-c3d715cedeeb\"  } ]} ]";

            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/traces/" + appId)).Returns(
                new MemoryStream(Encoding.Unicode.GetBytes(dataFlowTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traces = teamServerClient.GetTraces(appId);

            Assert.AreEqual(1, traces.Count);
            Trace trace = traces[0];
            Assert.AreEqual("217295", trace.TraceId);
            Assert.AreEqual("XSS from \"input\" Parameter on \"StringConcatVuln1.aspx\" page", trace.Title);
            Assert.AreEqual(4, trace.Request.Parameters.Count);
            Assert.AreEqual(2, trace.Links.Count);
        }

        [TestMethod]
        public void GetTraces_Config_PropertiesMatchExpected()
        {
            string appId = "arbitraryId";
            string configTraceJson = "[{  \"traceId\" : 217277,  \"uuid\" : \"YCTM-KK86-BXBT-SC4R\",  \"totalTracesRecieved\" : 2,  \"lastTimeSeen\" : 1417619036352,  \"firstTimeSeen\" : 1417465319846,  \"status\" : \"Reported\",  \"subStatus\" : \"\",  \"platform\" : \"\",  \"language\" : \".NET\",  \"title\" : \"Application Does Not Disable Version Header in web.config\",  \"subTitle\" : \"in web.config\",  \"reportedToBugTracker\" : false,  \"ruleName\" : \"version-header-enabled\",  \"severity\" : \"Note\",  \"likelihood\" : \"Low\",  \"impact\" : \"Low\",  \"confidence\" : \"High\",  \"request\" : {    \"port\" : 0,    \"headers\" : [ ],    \"parameters\" : [ ],    \"links\" : [ ]  },  \"events\" : [ ],  \"links\" : [ {    \"rel\" : \"self\",    \"href\" : \"https://localhost/Contrast/api/traces/c744888c-96e2-4e1d-926d-c3d715cedeeb/YCTM-KK86-BXBT-SC4R\"  }, {    \"rel\" : \"application\",    \"href\" : \"https://localhost/Contrast/api/applications/c744888c-96e2-4e1d-926d-c3d715cedeeb\"  } ]} ]";
            DateTime expectedDate = new DateTime(1970, 1, 1).AddMilliseconds(1417465319846);
            var mockSdkHttpClient = new Mock<IContrastRestClient>();
            mockSdkHttpClient.Setup(client => client.GetResponseStream("api/traces/" + appId)).Returns(
                new MemoryStream(Encoding.Unicode.GetBytes(configTraceJson))
                );

            var teamServerClient = new TeamServerClient(mockSdkHttpClient.Object);

            var traces = teamServerClient.GetTraces(appId);

            Assert.AreEqual(1, traces.Count);
            Trace trace = traces[0];
            Assert.AreEqual("217277", trace.TraceId);
            Assert.AreEqual("Application Does Not Disable Version Header in web.config", trace.Title);
            Assert.AreEqual(0, trace.Request.Parameters.Count);
            Assert.AreEqual(expectedDate, trace.FirstTimeSeen);
        }

    }
}
