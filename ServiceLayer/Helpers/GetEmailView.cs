using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToolsLayer.Http;
using ToolsLayer.Validation;

namespace ServiceLayer.Helpers
{
    public static class GetEmailView
    {
        private static async Task<string> GetView(HttpContext context, HttpRequestMessage request, Uri url)
        {
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            {
                using (HttpClient client = new HttpClient(handler) { BaseAddress = url })
                {
                    context.Request.Cookies.ToList().ForEach(x => cookieContainer.Add(url, new Cookie(x.Key, x.Value)));
                    var result = await client.SendAsync(request);
                    return await result.Content.ReadAsStringAsync();
                }
            }
        }
        public async static Task<string> GetConfirmationEmailViewAsync(HttpContext context,string userId, string token)
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            return await GetView(context, request, new Uri(context.GetOriginUrl() + "/api/Email/ConfirmationEmail?token=" +WebUtility.UrlEncode( token)+"&userId="+WebUtility.UrlEncode( userId)));
        }
        public async static Task<string> GetOrderEmailViewAsync(HttpContext context, int id)
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            return await GetView(context, request, new Uri(context.GetOriginUrl() + "/api/Email/OrderEmail?id=" +WebUtility.UrlEncode( id.ToString())));
        }
        public async static Task<string> GetResetPasswordEmailView(HttpContext context, string userId, string token)
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            return await GetView(context, request, new Uri(context.GetOriginUrl() + "/api/Email/ResetPasswordEmail?token=" +WebUtility.UrlEncode( token) + "&userId=" +WebUtility.UrlEncode( userId)));
        }
    }
}
