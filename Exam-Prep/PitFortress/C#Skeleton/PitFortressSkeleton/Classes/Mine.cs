namespace PitFortress.Classes
{
    using PitFortress.Interfaces;

    public class Mine : IMine
    {
        public Mine(int delay, int damage, int xCoordinate, Player player, int currentId)
        {
            this.Delay = delay;
            this.Damage = damage;
            this.XCoordinate = xCoordinate;
            this.Player = player;
            this.Id = currentId;
        }

        public int Id { get; private set; }

        public int Delay { get; set; }

        public int Damage { get; private set; }

        public int XCoordinate { get; private set; }

        public Player Player { get; private set; }

        public int CompareTo(Mine other)
        {
            if (this.Delay.CompareTo(other.Delay) == 0)
            {
                return this.Id.CompareTo(other.Id);
            }

            return this.Delay.CompareTo(other.Delay);
        }
    }
}
