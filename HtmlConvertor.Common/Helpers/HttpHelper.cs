using System;
using System.Net;

namespace CommonHtmlConverter.Helpers
{
    public class HttpHelper
    {
        public static void CheckUri(Uri uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "HEAD";
            request.GetResponse();
        }
    }
}