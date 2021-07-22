namespace App.Contracts.Models
{
    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Y { get; }
        public int X { get; }
    }
}