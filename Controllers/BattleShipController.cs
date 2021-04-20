using BattleShips.Application;
using BattleShips.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace BattleShips.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleShipController : ControllerBase
    {
        private readonly ILogger<BattleShipController> _logger;

        public BattleShipController(ILogger<BattleShipController> logger)
        {
            _logger = logger;
        }

        [HttpPost("initialize")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult Initialize()
        {
            Board.Reset();
            return Accepted();
        }

        [HttpPost("AddShip")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddShip([FromBody]AddShipRequest addShipRequest)
        {
            if (addShipRequest == null)
            {
                return BadRequest($"{nameof(addShipRequest)} must not be null");
            }
            
            if (addShipRequest.End == null || addShipRequest.Start == null)
            {
                return BadRequest("Coordinate must not be null");
            }
            var result = Board.Add(addShipRequest.Start, addShipRequest.End);
           if (!result.Valid)
               return BadRequest(result.ErrorMessage);
           return Ok();
        }

        [HttpPost("Hit")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult HitShip([FromBody] Coordinate coordinate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = Board.Hit(coordinate);
            return Ok(result);
        }
    }
}
