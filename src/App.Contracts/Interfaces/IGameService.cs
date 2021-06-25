using System.Threading.Tasks;

namespace App.Contracts.Interfaces
{
    public interface IGameService
    {
        Task CreateGame(int size);
        Task StartGame();
        Task CreateBoard();
        Task ToggleLight(int x, int y);
    }
}