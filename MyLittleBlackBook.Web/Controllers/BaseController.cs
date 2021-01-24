using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleBlackBook.Web.Controllers
{
    public class BaseController : Controller
    {
        public async Task<string> HttpClientGet(string endPoint)
        {
            var result = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44309/");

                var response = await client.GetAsync(endPoint);

                //if (response.IsSuccessStatusCode)
                //{
                    result = response.Content.ReadAsStringAsync().Result;

                //}
                if (!response.IsSuccessStatusCode)
                    RedirectToAction("Index", "Error");

            }

            return result;
        }

        public async Task<string> HttpClientPost(string endPoint, string content)
        {
            var result = string.Empty;
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44309/");

                var response = await client.PostAsync(endPoint, httpContent);

                //if (response.IsSuccessStatusCode)
                //{
                result = response.Content.ReadAsStringAsync().Result;

                //}
                if (!response.IsSuccessStatusCode)
                    RedirectToAction("Index", "Error");

            }

            return result;
        }

        public async Task<string> HttpClientPut(string endPoint, string content)
        {
            var result = string.Empty;
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44309/");

                var response = await client.PutAsync(endPoint, httpContent);
                result = response.Content.ReadAsStringAsync().Result;

                if (!response.IsSuccessStatusCode)
                    RedirectToAction("Index", "Error");            }

            return result;
        }
    }
}
