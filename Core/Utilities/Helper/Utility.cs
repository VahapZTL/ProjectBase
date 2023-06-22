using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Security.Policy;

namespace Core.Utilities.Helper
{
    public static class Utility
    {
        public static TResponse PostMethod<TRequest, TResponse>(string baseUrl, string method, TRequest request)
        {
            string _request = JsonConvert.SerializeObject(request, Formatting.Indented);
            var result = JsonConvert.DeserializeObject<TResponse>(HttpPostJson(baseUrl, method, _request));
            return result;
        }

        private static string HttpPostJson(string url, string method, string json)
        {
            if (string.IsNullOrEmpty(url) ||
                string.IsNullOrEmpty(method) ||
                string.IsNullOrEmpty(json))
            {
                throw new Exception(string.Format("Url : {0}, Method : {1}, json : {2}", url, method, json));
            }
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/" + method);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
    }
}
