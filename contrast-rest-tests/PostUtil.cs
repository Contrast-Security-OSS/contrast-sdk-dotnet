using System;
using System.IO;
using System.Text;

namespace sdk_tests
{
    public static class PostUtil
    {
        public static System.Net.Http.HttpResponseMessage GetPostResponse(System.Net.HttpStatusCode statusCode, string responseJson)
        {
            var response = new System.Net.Http.HttpResponseMessage(statusCode);
            response.Content = new System.Net.Http.StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(responseJson)));
            return response;
        }
    }
}
