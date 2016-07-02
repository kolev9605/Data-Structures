namespace PitFortress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PitFortress.Classes;
    using PitFortress.Interfaces;
    using Wintellect.PowerCollections;

    public class PitFortressCollection : IPitFortress
    {
        private Dictionary<string, Player> playersByName;
        private OrderedDictionary<int, SortedSet<Minion>> minionsByCoordinate;
        private Dictionary<int, SortedSet<Mine>> minesByCoordinate;
        private SortedSet<Player> sortedPlayers;
        private SortedSet<Mine> sortedMines;
        private int currentMineId = 1;
        private int currentMinionId = 1;

        public PitFortressCollection()
        {
            this.playersByName = new Dictionary<string, Player>();
            this.minionsByCoordinate = new OrderedDictionary<int, SortedSet<Minion>>();
            this.minesByCoordinate = new Dictionary<int, SortedSet<Mine>>();
            this.sortedPlayers = new SortedSet<Player>();
            this.sortedMines = new SortedSet<Mine>();
        }

        public int PlayersCount { get { return this.playersByName.Count; } }

        public int MinionsCount
        {
            get { return this.minionsByCoordinate.SelectMany(x => x.Value).Count(); }
        }

        public int MinesCount
        {
            get { return this.sortedMines.Count; }
        }

        public void AddPlayer(string name, int mineRadius)
        {
            if (this.playersByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            ValidateMineRadius(mineRadius);

            Player newPlayer = new Player(name, mineRadius);
            this.playersByName[name] = newPlayer;
            this.sortedPlayers.Add(newPlayer);
        }

        private static void ValidateMineRadius(int mineRadius)
        {
            if (mineRadius < 0)
            {
                throw new ArgumentException();
            }
        }

        public void AddMinion(int xCoordinate)
        {
            this.ValidateXCoordinate(xCoordinate);
            Minion newMinion = new Minion(xCoordinate, this.currentMinionId++);

            if (!this.minionsByCoordinate.ContainsKey(xCoordinate))
            {
                this.minionsByCoordinate[xCoordinate] = new SortedSet<Minion>();
            }

            this.minionsByCoordinate[xCoordinate].Add(newMinion);
        }

        public void SetMine(string playerName, int xCoordinate, int delay, int damage)
        {
            this.ValidatePlayerExistence(playerName);
            this.ValidateXCoordinate(xCoordinate);
            this.ValidateDelay(delay);
            this.ValidateDamage(damage);

            Player player = this.playersByName[playerName];
            Mine newMine = new Mine(delay, damage, xCoordinate, player, this.currentMineId++);
            if (!this.minesByCoordinate.ContainsKey(xCoordinate))
            {
                this.minesByCoordinate[xCoordinate] = new SortedSet<Mine>();
            }

            this.minesByCoordinate[xCoordinate].Add(newMine);
            this.sortedMines.Add(newMine);
        }

        public IEnumerable<Minion> ReportMinions()
        {
            var result = this.minionsByCoordinate.SelectMany(x => x.Value);

            return result;
        }

        public IEnumerable<Player> Top3PlayersByScore()
        {
            this.ValidatePlayerCount();
            var result = this.sortedPlayers.Take(3);

            return result;
        }

        public IEnumerable<Player> Min3PlayersByScore()
        {
            this.ValidatePlayerCount();
            var result = this.sortedPlayers.Reverse().Take(3);

            return result;
        }

        public IEnumerable<Mine> GetMines()
        {
            return this.sortedMines;
        }

        public void PlayTurn()
        {
            this.ReduceMineDeley();
            List<Mine> minesToRemove = new List<Mine>();
            foreach (var mine in this.sortedMines)
            {
                if (mine.Delay > 0)
                {
                    break;
                }

                this.Explode(mine.XCoordinate, mine);
                minesToRemove.Add(mine);
            }

            foreach (var mine in minesToRemove)
            {
                this.sortedMines.Remove(mine);
                this.minesByCoordinate[mine.XCoordinate].Remove(mine);
            }

            List<Player> temp = new List<Player>(this.sortedPlayers);
            this.sortedPlayers.Clear();
            foreach (var mine in temp)
            {
                this.sortedPlayers.Add(mine);
            }
        }

        private void ReduceMineDeley()
        {
            foreach (var mine in this.sortedMines)
            {
                mine.Delay--;
            }
        }

        private void Explode(int xCoordinate, Mine mine)
        {
            var range = this.minionsByCoordinate.Range(
                xCoordinate - mine.Player.Radius,
                true,
                xCoordinate + mine.Player.Radius,
                true);

            string playerName = mine.Player.Name;
            List<Minion> minionsToDelete = new List<Minion>();
            foreach (var pair in range)
            {
                if (!this.minionsByCoordinate.ContainsKey(pair.Key))
                {
                    continue;
                }

                foreach (var minion in pair.Value)
                {
                    minion.Health -= mine.Damage;
                    if (minion.Health <= 0)
                    {
                        this.playersByName[playerName].Score++;
                        minionsToDelete.Add(minion);
                    }
                }
            }

            foreach (var minion in minionsToDelete)
            {
                this.minionsByCoordinate[minion.XCoordinate].Remove(minion);
            }
        }

        private void ValidateDamage(int damage)
        {
            if (damage < 0 || damage > 100)
            {
                throw new ArgumentException();
            }
        }

        private void ValidateDelay(int delay)
        {
            if (delay < 1 || delay > 10000)
            {
                throw new ArgumentException();
            }
        }

        private void ValidateXCoordinate(int xCoordinate)
        {
            if (xCoordinate < 0 || xCoordinate > 1000000)
            {
                throw new ArgumentException();
            }
        }

        private void ValidatePlayerExistence(string playerName)
        {
            if (!this.playersByName.ContainsKey(playerName))
            {
                throw new ArgumentException();
            }
        }

        private void ValidatePlayerCount()
        {
            if (this.playersByName.Count < 3)
            {
                throw new ArgumentException();
            }
        }
    }
}
