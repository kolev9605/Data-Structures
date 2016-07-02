namespace PitFortress.Classes
{
    using PitFortress.Interfaces;

    public class Player : IPlayer
    {
        public Player(string name, int radius)
        {
            this.Name = name;
            this.Radius = radius;
            this.Score = 0;
        }

        public string Name { get; private set; }

        public int Radius { get; private set; }

        public int Score { get; set; }

        public int CompareTo(Player other)
        {
            if (this.Score.CompareTo(other.Score) == 0)
            {
                return other.Name.CompareTo(this.Name);
            }

            return other.Score.CompareTo(this.Score);
        }
    }
}
