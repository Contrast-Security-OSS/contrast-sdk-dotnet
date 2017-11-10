using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using contrast_rest_dotnet.Serialization;

namespace sdk_tests
{
    [TestClass]
    public class ConverterTest
    {
        const long TEST_TIME = 1509926400000;
        readonly DateTime TEST_DATE = new DateTime(2017, 11, 6, 0, 0, 0, DateTimeKind.Utc);

        [TestMethod]
        public void TestUnixTimeToDateTime()
        {
            DateTime output = (DateTime) DateTimeConverter.ConvertToDateTime(TEST_TIME);
            Assert.AreEqual(TEST_DATE, output);
        }

        [TestMethod]
        public void TestDateTimeToUnixTime()
        {
            long output = (long) DateTimeConverter.ConvertToUnixTime(TEST_DATE);
            Assert.AreEqual(TEST_TIME, output);
        }
    }
}
