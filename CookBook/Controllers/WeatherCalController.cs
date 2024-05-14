using DP.Spoonacular;
using DP.WeatherCal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherCalController : ControllerBase
    {

        // Gives the weather
        // GET /api/
        [HttpGet("GetWeather")]
        public async Task<ActionResult<IEnumerable<string>>> GetWeather()
        {
            string City = "Tel Aviv";
            string url = $" https://api.openweathermap.org/data/2.5/weather?q={City}&appid=2ee95b40f4f126c98f6986d56d50ddb8&units=metric";
            var client = new RestClient(url);
            var request = new RestRequest(new Uri(url), Method.Get);
            RestResponse response = client.Execute(request);

            if (response == null)
            {
                return NotFound();
            }
            string myCon = response.Content;
            WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(myCon);
            string degr = "Degrees: " + weatherInfo.Main.Temp + "\nDescription: " + weatherInfo.Weather[0].Description;

            return Ok(degr);
        }


    }
}
