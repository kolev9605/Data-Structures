namespace PitFortress.Classes
{
    using PitFortress.Interfaces;

    public class Minion : IMinion
    {
        public Minion(int xCoordinate, int currentId)
        {
            this.XCoordinate = xCoordinate;
            this.Id = currentId;
            this.Health = 100;
        }

        public int Id { get; private set; }

        public int XCoordinate { get; private set; }

        public int Health { get; set; }

        public int CompareTo(Minion other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}
