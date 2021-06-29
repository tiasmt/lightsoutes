namespace App.Contracts.Entities
{
    public record Settings
    {
        public Settings(int boardSize)
        {
            BoardSize = boardSize;
        }
        public int BoardSize { get; }
    }
}
