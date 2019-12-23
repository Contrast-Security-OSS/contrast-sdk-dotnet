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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contrast.Http;

namespace sdk_tests
{
    [TestClass]
    public class FilterTest
    {
        [TestMethod]
        public void TestServerFilter()
        {
            ServerFilter filter = new ServerFilter();
            filter.Limit = 10;
            filter.Expand = new List<ServerExpandValue>();
            filter.Expand.Add(ServerExpandValue.applications);
            filter.IncludeArchived = false;
            filter.Status = "Denied";
            filter.QueryParam = "any";

            string query = filter.ToString();

            Assert.IsTrue(query.Contains("includeArchived"));
            Assert.IsTrue(query.Contains("limit"));
            Assert.IsTrue(query.Contains("expand=applications"));
            Assert.IsTrue(query.Contains("status=Denied"));
            Assert.IsTrue(query.Contains("q=any"));
            Assert.IsFalse(query.Contains("applicationIds"));
            Assert.IsFalse(query.Contains("logLevels"));
            Assert.IsFalse(query.Contains("offset"));
        }

        [TestMethod]
        public void TestTraceFilter()
        {
            TraceFilter filter = new TraceFilter();
            filter.Offset = 0;
            filter.StartDate = DateTime.Now;
            filter.Urls = new List<string>();
            filter.Urls.Add("http://dummytest");
            filter.Sort = "any";
            filter.Expand = new List<TraceExpandValue>();
            filter.Expand.Add(TraceExpandValue.application);

            string qs = filter.ToString();

            Assert.IsTrue(qs.Contains("offset=0"));
            Assert.IsTrue(qs.Contains("startDate"));
            Assert.IsTrue(qs.Contains("urls=http://dummytest"));
            Assert.IsTrue(qs.Contains("sort=any"));
            Assert.IsTrue(qs.Contains("expand=application"));

            Assert.IsFalse(qs.Contains("limit"));
            Assert.IsFalse(qs.Contains("endDate"));
            Assert.IsFalse(qs.Contains("filterTags"));
            Assert.IsFalse(qs.Contains("servers"));
        }
    }
}
