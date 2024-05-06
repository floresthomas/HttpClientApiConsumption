using HttpClientConsumeApi.Interface;
using HttpClientConsumeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientConsumeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpCallController : ControllerBase
    {
        private readonly IHttpCallService _httpCallService;

        public HttpCallController(IHttpCallService httpCallService)
        {
            _httpCallService = httpCallService;
        }

        [HttpGet]
        [Route("GetDataFromApi")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _httpCallService.GetData<DataModel>();
                return(response is null) ? NotFound() : Ok(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
