using DP.HebCal;
using DP.Imagga;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using RestSharp;

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImaggaController : ControllerBase
    {
        // Gives the weather
        // GET /api/
        [HttpGet("GetPicTag/{ImgUrl}")]
        public async Task<ActionResult<IEnumerable<string>>> GetPicTag(string ImgUrl)
        {
            string apiKey = "acc_af6f7e71795036a";
            string apiSecret = "f449b3beb945a5bf37a4673b6b667edd";
            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));
            var client = new RestClient("https://api.imagga.com/v2/tags");
            var request = new RestRequest(new Uri("https://api.imagga.com/v2/tags"), Method.Get);
            request.AddParameter("image_url", ImgUrl);
            request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));
            RestResponse response = client.Execute(request);
            
            if (response == null)
            {
                return NotFound();
            }
            string myTags = response.Content;

            string TagL = "";
            if (myTags != null)
            {
                imagaInfo myPic = JsonConvert.DeserializeObject<imagaInfo>(myTags);
                foreach (var item in myPic.result.tags)
                {
                    if (item.tag.en == "food" && item.confidence > 70)
                    {
                        TagL += item.tag.en + "\n";
                    }
                }
            }
            if (TagL != "") 
            {
                TagL = "true\n" + TagL;
                return Ok(TagL);
            }

            return Ok("false");
        }
    }
}
