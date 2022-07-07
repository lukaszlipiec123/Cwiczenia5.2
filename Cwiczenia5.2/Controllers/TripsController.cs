using System;
using System.Threading.Tasks;
using Cwiczenia5._2.DataAccessLayer;
using Cwiczenia5._2.Models;
using Cwiczenia5._2.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia5._2.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {

        private readonly ITripsDataAccess _tripsDataAccess;
        public TripsController(ITripsDataAccess tripsDataAccess)
        {
            _tripsDataAccess = tripsDataAccess;

        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            try
            {
                return Ok(await _tripsDataAccess.GetTripsAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            try
            {
                return Ok(await _tripsDataAccess.DeleteClient(idClient));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssociateClientToTrip(ClientRequest clientRequest)
        {

            try
            {
                return Ok(await _tripsDataAccess.AssociateClientToTrip(clientRequest));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
