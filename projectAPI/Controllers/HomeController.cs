using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace projectAPI.Controllers
{
    public class HomeController : Controller
    {
        private static string WebAPIURL = "http://localhost:53224/";
        public async Task<ActionResult> Index()
        {
            var tokenBased = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await client.GetAsync(requestUri:"Account/ValidLogin?userName=admin&userPassword=admin");
                if(responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStreamAsync().Result;
                    tokenBased = JsonConvert.DeserializeObject<string>(resultMessage.ToString());
                    Session["TokenNumber"] = tokenBased;
                }
            }

                return Content(tokenBased);
        }
    }
}
