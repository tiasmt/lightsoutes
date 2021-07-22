using App.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace App.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameService _gameService;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpPost("CreateGame")]
        public async Task<IActionResult> CreateGame(int boardSize, string gameName, string playerName, string connectionId)
        {
            await _gameService.CreateGame(connectionId, boardSize, gameName, playerName);
            return Ok();
        }
        [HttpPost("ToggleLight")]
        public async Task<IActionResult> ToggleLight(string connectionId, string gameName, int x, int y)
        {
            await _gameService.ToggleLight(connectionId, gameName, x, y);
            return Ok();
        }

        [HttpPost("ReplayEvents")]
        public async Task<IActionResult> ReplayEvents(string connectionId, string gameName, int end)
        {
            await _gameService.Replay(connectionId, gameName, end);
            return Ok();
        }
    }
}
