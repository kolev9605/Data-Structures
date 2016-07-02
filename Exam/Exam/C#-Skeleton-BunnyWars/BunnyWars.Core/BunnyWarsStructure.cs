namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private OrderedDictionary<int, OrderedSet<Bunny>> roomsById;
        private Dictionary<int, OrderedSet<Bunny>> bunniesByTeamId;
        private OrderedDictionary<string, Bunny> bunniesByName;
        private OrderedSet<int> roomSet;
        private OrderedDictionary<string, Bunny> bunniesByReversedName;

        public BunnyWarsStructure()
        {
            this.bunniesByName = new OrderedDictionary<string, Bunny>();
            this.bunniesByTeamId = new Dictionary<int, OrderedSet<Bunny>>();
            this.roomsById = new OrderedDictionary<int, OrderedSet<Bunny>>();
            this.roomSet = new OrderedSet<int>();
            this.bunniesByReversedName = new OrderedDictionary<string, Bunny>(StringComparer.Ordinal);
        }

        public int BunnyCount
        {
            get { return this.bunniesByName.Count; }
        }

        public int RoomCount
        {
            get { return this.roomsById.Count; }
        }

        public void AddRoom(int roomId)
        {
            if (this.roomsById.ContainsKey(roomId))
            {
                throw new ArgumentException("Room with given ID already exists.");
            }

            this.roomsById[roomId] = new OrderedSet<Bunny>();
            this.roomSet.Add(roomId);
        }

        public void AddBunny(string name, int team, int roomId)
        {
            this.ValidateTeamId(team);
            this.ValidateRoomId(roomId);

            Bunny bunny = new Bunny(name, team, roomId);

            //add in roomsById
            this.roomsById[roomId].Add(bunny);

            //add in bynniesByTeamId
            if (!this.bunniesByTeamId.ContainsKey(team))
            {
                this.bunniesByTeamId[team] = new OrderedSet<Bunny>();
            }

            this.bunniesByTeamId[team].Add(bunny);

            //add in buninesByTeamId
            if (this.bunniesByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            this.bunniesByName[name] = bunny;

            var naneToChararr = name.ToCharArray();
            Array.Reverse(naneToChararr);
            this.bunniesByReversedName[string.Join("", naneToChararr)] = bunny;
        }

        private void ValidateRoomId(int roomId)
        {
            if (!this.roomsById.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }
        }

        private void ValidateTeamId(int team)
        {
            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public void Remove(int roomId)
        {
            if (!this.roomsById.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            foreach (var bunny in this.roomsById[roomId])
            {
                var team = bunny.Team;
                var name = bunny.Name;

                //remove by team
                this.bunniesByTeamId[team].Remove(bunny);
                //remove by name
                this.bunniesByName.Remove(name);
                this.bunniesByReversedName.Remove(name);
            }

            //remove by room
            this.roomsById[roomId].Clear();
            this.roomsById.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            var bunny = this.bunniesByName[bunnyName];
            var currentRoomIndex = this.roomSet.IndexOf(bunny.RoomId);

            if (currentRoomIndex + 1 >= this.roomSet.Count)
            {
                //to the front
                var currentRoom = this.roomSet[currentRoomIndex];
                var firstRoom = this.roomSet[0];
                this.roomsById[currentRoom].Remove(bunny);
                this.roomsById[firstRoom].Add(bunny);
                bunny.RoomId = firstRoom;
            }
            else
            {
                //move to next
                var currentRoomId = this.roomSet[currentRoomIndex];
                var nextRoom = this.roomSet[currentRoomIndex + 1];
                this.roomsById[currentRoomId].Remove(bunny);
                this.roomsById[nextRoom].Add(bunny);
                bunny.RoomId = nextRoom;
            }
        }

        public void Previous(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            var bunny = this.bunniesByName[bunnyName];
            var currentRoomIndex = this.roomSet.IndexOf(bunny.RoomId);

            if (currentRoomIndex - 1 < 0)
            {
                //to the back
                var currentRoom = this.roomSet[currentRoomIndex];
                var lastRoom = this.roomSet[this.roomSet.Count - 1];
                this.roomsById[currentRoom].Remove(bunny);
                this.roomsById[lastRoom].Add(bunny);
                bunny.RoomId = lastRoom;
            }
            else
            {
                //move to previous
                var currentRoom = this.roomSet[currentRoomIndex];
                var prevRoom = this.roomSet[currentRoomIndex - 1];
                this.roomsById[currentRoom].Remove(bunny);
                this.roomsById[prevRoom].Add(bunny);
                bunny.RoomId = prevRoom;
            }
        }

        public void Detonate(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            var bunny = this.bunniesByName[bunnyName];

            List<Bunny> bunniesToBeDeleted = new List<Bunny>();

            foreach (var currentBunny in this.roomsById[bunny.RoomId])
            {
                if (currentBunny.Name != bunny.Name && currentBunny.Team != bunny.Team)
                {
                    currentBunny.Health -= 30;
                    if (currentBunny.Health <= 0)
                    {
                        //increment score
                        this.bunniesByName[bunnyName].Score++;
                        bunniesToBeDeleted.Add(currentBunny);
                        this.bunniesByName.Remove(currentBunny.Name);
                    }
                }
            }

            foreach (var currentBunny in bunniesToBeDeleted)
            {
                //remove the dead bunny
                this.bunniesByTeamId[currentBunny.Team].Remove(currentBunny);
                this.roomsById[bunny.RoomId].Remove(currentBunny);
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            this.ValidateTeamId(team);
            return this.bunniesByTeamId[team].Reversed();
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            var suffixCharArr = suffix.ToCharArray();
            Array.Reverse(suffixCharArr);
            var suffixV2 = string.Join("", suffixCharArr);

            var uppeRane = suffixV2 + char.MaxValue;
            var kur = this.bunniesByReversedName.Range(suffixV2, true, uppeRane, true).Values;
            return kur;
        }
    }
}
