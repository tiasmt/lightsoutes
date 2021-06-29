using System.Threading.Tasks;

namespace App.Contracts.Interfaces
{
    public interface IGameService
    {
        Task CreateGame(int size, string gameName, string playerName);
        Task ToggleLight(string gameName, int x, int y);
    }
}