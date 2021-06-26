using System.Threading.Tasks;

namespace App.Contracts.Interfaces
{
    public interface IGameService
    {
        Task CreateGame(int size, string gameName, string playerName);
        Task StartGame();
        Task ToggleLight(int gameId, int x, int y);
    }
}