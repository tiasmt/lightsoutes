using System.Threading.Tasks;

namespace App.Contracts.Interfaces
{
    public interface IGameService
    {
        Task CreateGame(string connectionId, int size, string gameName, string playerName);
        Task ToggleLight(string connectionId, string gameName, int x, int y);
        Task Replay(string connectionId, string gameName, int end);
    }
}