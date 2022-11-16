using DoINeedUmbrellaToday.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DoINeedUmbrellaToday.Controllers
{
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetLocationByQuery([Required] String query)
        {
            try
            {
                var result = await locationService.GetLocationAsync(query);


                if (!result.Validated)
                {
                    throw new InvalidLocationException();

                }

                return Ok(result);
            }
            catch (InvalidLocationException e)
            {
                return BadRequest(new
                {
                    error = e.Message
                });
            }
        }

        public ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }
    }
}
