using Microsoft.AspNetCore.Mvc;

namespace OTM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StreetController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly IUserProvider userProvider;

        public StreetController(HttpClient httpClient, IUserProvider userProvider)
        {
            this.httpClient = httpClient;
            this.userProvider = userProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var users = userProvider.Get();
            var response = await httpClient.GetAsync("https://localhost:7039/weatherforecast");
            return new string[] { "value1", "value2" };
        }
    }
}
