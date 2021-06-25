namespace App.Contracts.Models
{
    public class Position
    {
        public Position()
        {}
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Y {get; set;}
        public int X {get; set;}
    }
}