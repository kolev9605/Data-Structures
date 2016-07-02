namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private OrderedDictionary<int, HashSet<Bunny>> bunniesByRoomId;
        private Dictionary<int, OrderedSet<Bunny>> bunniesByTeamId;
        private Dictionary<string, Bunny> allBunnies;
        private OrderedDictionary<string, Bunny> allBunniesReversed;
        private OrderedSet<int> allRooms;
        private Dictionary<int, Dictionary<int, HashSet<Bunny>>> bunniesByRoomIdAndTeamId;

        public BunnyWarsStructure()
        {
            this.bunniesByRoomId = new OrderedDictionary<int, HashSet<Bunny>>();
            this.bunniesByTeamId = new Dictionary<int, OrderedSet<Bunny>>();
            this.allBunnies = new Dictionary<string, Bunny>();
            this.allBunniesReversed = new OrderedDictionary<string, Bunny>(StringComparer.Ordinal);
            this.allRooms = new OrderedSet<int>();
            this.bunniesByRoomIdAndTeamId = new Dictionary<int, Dictionary<int, HashSet<Bunny>>>();
        }

        public int BunnyCount { get { return this.allBunnies.Count; } }

        public int RoomCount { get { return this.bunniesByRoomId.Count; } }

        public void AddRoom(int roomId)
        {
            if (this.bunniesByRoomId.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            this.bunniesByRoomId[roomId] = new HashSet<Bunny>();
            this.allRooms.Add(roomId);
            this.bunniesByRoomIdAndTeamId[roomId] = new Dictionary<int, HashSet<Bunny>>();
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (this.allBunnies.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            if (!this.bunniesByRoomId.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            this.ValidateTeamId(team);

            Bunny newBunny = new Bunny(name, team, roomId);
            //add in rooms
            this.bunniesByRoomId[roomId].Add(newBunny);
            //add in teams
            if (!this.bunniesByTeamId.ContainsKey(team))
            {
                this.bunniesByTeamId[team] = new OrderedSet<Bunny>();
            }

            this.bunniesByTeamId[team].Add(newBunny);
            //add in bunnies
            this.allBunnies[name] = newBunny;
            string reversedName = this.Reverse(name);
            this.allBunniesReversed[reversedName] = newBunny;
            //add in bunniesByRommIdAndTeamId
            if (!this.bunniesByRoomIdAndTeamId[roomId].ContainsKey(team))
            {
                this.bunniesByRoomIdAndTeamId[roomId][team] = new HashSet<Bunny>();
            }

            this.bunniesByRoomIdAndTeamId[roomId][team].Add(newBunny);
        }

        public void Remove(int roomId)
        {
            if (!this.bunniesByRoomId.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            foreach (var bunny in this.bunniesByRoomId[roomId])
            {
                //remove from all bunnies
                this.allBunnies.Remove(bunny.Name);
                string reversedName = this.Reverse(bunny.Name);
                this.allBunnies.Remove(reversedName);
                //remove from bunnies by teamId
                this.bunniesByTeamId[bunny.Team].Remove(bunny);
            }

            //remove from bunnies by roomId
            this.bunniesByRoomId.Remove(roomId);
            //remove from rooms
            this.allRooms.Remove(roomId);
            //remove from bunnies by roomId and teamId
            this.bunniesByRoomIdAndTeamId.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            if (!this.allBunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            Bunny bunny = this.allBunnies[bunnyName];
            int currentRoomIndex = this.allRooms.IndexOf(bunny.RoomId);
            this.RemoveBunny(bunnyName);

            if (currentRoomIndex + 1 >= this.allRooms.Count)
            {
                //move to front
                int targetRoomId = this.allRooms[0];
                this.AddBunny(bunnyName, bunny.Team, targetRoomId);
            }
            else
            {
                //normal move to next room
                int targetRoomId = this.allRooms[currentRoomIndex + 1];
                this.AddBunny(bunnyName, bunny.Team, targetRoomId);
            }
        }

        public void Previous(string bunnyName)
        {
            if (!this.allBunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            Bunny bunny = this.allBunnies[bunnyName];
            int currentRoomIndex = this.allRooms.IndexOf(bunny.RoomId);
            this.RemoveBunny(bunnyName);

            if (currentRoomIndex - 1 < 0)
            {
                //move to back
                int targetRoomId = this.allRooms[this.allRooms.Count - 1];
                this.AddBunny(bunnyName, bunny.Team, targetRoomId);
            }
            else
            {
                //normal move to prev room
                int targetRoomId = this.allRooms[currentRoomIndex - 1];
                this.AddBunny(bunnyName, bunny.Team, targetRoomId);
            }
        }

        public void Detonate(string bunnyName)
        {
            if (!this.allBunnies.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            Bunny bunny = this.allBunnies[bunnyName];

            var targetBunnies = new List<Bunny>(this.bunniesByRoomIdAndTeamId[bunny.RoomId]
                .Where(x => x.Key != bunny.Team)
                .SelectMany(x => x.Value));

            foreach (var targetBunny in targetBunnies)
            {
                targetBunny.Health -= 30;
                if (targetBunny.Health <= 0)
                {
                    this.allBunnies[bunnyName].Score++;
                    this.RemoveBunny(targetBunny.Name);
                }
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            this.ValidateTeamId(team);
            return this.bunniesByTeamId[team].Reversed();
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            string reversed = this.Reverse(suffix);

            var result = this.allBunniesReversed.Range(reversed, true, reversed + char.MaxValue, true).Values;

            return result;
        }

        private void ValidateTeamId(int team)
        {
            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void RemoveBunny(string bunnyName)
        {
            var bunny = this.allBunnies[bunnyName];

            //remove from bunniesByRoomId
            this.bunniesByRoomId[bunny.RoomId].Remove(bunny);
            //remove from bunniesByTeamId
            this.bunniesByTeamId[bunny.Team].Remove(bunny);
            //remove from allBunnies
            this.allBunnies.Remove(bunnyName);
            //remove from bunniesByRoomIdAndTeamId
            this.bunniesByRoomIdAndTeamId[bunny.RoomId][bunny.Team].Remove(bunny);
        }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
