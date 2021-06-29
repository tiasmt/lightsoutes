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
        public async Task<IActionResult> CreateGame(int boardSize, string gameName, string playerName)
        {
            await _gameService.CreateGame(boardSize, gameName, playerName);
            return Ok();
        }
        [HttpPost("ToggleLight")]
        public async Task<IActionResult> ToggleLight(string gameName, int x, int y)
        {
            await _gameService.ToggleLight(gameName, x, y);
            return Ok();
        }
    }
}
