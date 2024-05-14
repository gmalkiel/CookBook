using DP.HebCal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Kiota.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HebCalController : ControllerBase
    {
        // Gives the weather
        // GET /api/
        [HttpGet("GetDateCal")]
        public async Task<ActionResult<IEnumerable<string>>> GetDateCal()
        {
            string City = "IL-Tel Aviv";

            DateTime Date = DateTime.Now;
            string Dts = Date.ToString("yyyy-MM-dd");

            DateTime Date1 = DateTime.Now.AddDays(30);
            string Dte = Date1.ToString("yyyy-MM-dd");

            string url = $"https://www.hebcal.com/hebcal?v=1&cfg=json&maj=on&min=on&mod=on&nx=on&year=now&month=x&ss=on&mf=on&c=on&city={City}&M=on&s=on&start={Dts}&end={Dte}";
            var client = new RestClient(url);
            var request = new RestRequest(new Uri(url), RestSharp.Method.Get);
            RestResponse response = client.Execute(request);

            if (response == null)
            {
                return NotFound();
            }
            string myEve = response.Content;

            var hebcalEvent = JsonConvert.DeserializeObject<HebCalInfo>(myEve);
            
            string HolidayString = "";
            foreach (var item in hebcalEvent.items)
            {
                if (item.category == "holiday")
                {
                    HolidayString += item.title + "\n";
                }
            }

            return Ok(HolidayString);
        }
    }
}
