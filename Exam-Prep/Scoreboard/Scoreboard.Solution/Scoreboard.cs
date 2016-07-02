namespace Scoreboard.Solution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Wintellect.PowerCollections;

    public class ScoreboardMain
    {
        static void Main()
        {
            var commandExecutor = new ScoreboardCommandExecutor();
            while (true)
            {
                string command = Console.ReadLine();
                if (command == "End")
                {
                    break;
                }
                if (command != "")
                {
                    string commandResult = commandExecutor.ProcessCommand(command);
                    Console.WriteLine(commandResult);
                }
            }
        }
    }

    public class ScoreboardCommandExecutor
    {
        private Scoreboard scoreboard = new Scoreboard();

        public string ProcessCommand(string commandLine)
        {
            var tokens = commandLine.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            var command = tokens[0];
            switch (command)
            {
                case "RegisterUser":
                    return this.RegisterUser(tokens[1], tokens[2]);
                case "RegisterGame":
                    return this.RegisterGame(tokens[1], tokens[2]);
                case "AddScore":
                    return this.AddScore(tokens[1], tokens[2], tokens[3], tokens[4],
                        int.Parse(tokens[5]));
                case "ShowScoreboard":
                    return this.ShowScoreboard(tokens[1]);
                case "DeleteGame":
                    return this.DeleteGame(tokens[1], tokens[2]);
                case "ListGamesByPrefix":
                    return this.ListGamesByPrefix(tokens[1]);
                default:
                    return "Incorrect command";
            }
        }

        private string RegisterUser(string username, string userPassword)
        {
            if (this.scoreboard.RegisterUser(username, userPassword))
            {
                return "User registered";
            }

            return "Duplicated user";
        }

        private string RegisterGame(string gameName, string gamePassword)
        {
            if (this.scoreboard.RegisterGame(gameName, gamePassword))
            {
                return "Game registered";
            }

            return "Duplicated game";
        }

        private string AddScore(string username, string userPassword,
            string gameName, string gamePassword, int score)
        {
            if (this.scoreboard.AddScore(username, userPassword,
                gameName, gamePassword, score))
            {
                return "Score added";
            }

            return "Cannot add score";
        }

        private string ShowScoreboard(string gameName)
        {
            var scoreboardEntries = this.scoreboard.ShowScoreboard(gameName);
            if (scoreboardEntries == null)
            {
                return "Game not found";
            }

            if (scoreboardEntries.Any())
            {
                var result = new StringBuilder();
                int counter = 0;
                foreach (var entry in scoreboardEntries)
                {
                    counter++;
                    result.AppendFormat("#{0} {1} {2}",
                        counter, entry.Username, entry.Score);
                    result.AppendLine();
                }
                result.Length -= Environment.NewLine.Length;
                return result.ToString();
            }

            return "No score";
        }

        private string DeleteGame(string gameName, string gamePassword)
        {
            if (this.scoreboard.DeleteGame(gameName, gamePassword))
            {
                return "Game deleted";
            }

            return "Cannot delete game";
        }

        private string ListGamesByPrefix(string namePrefix)
        {
            var matchedGames = this.scoreboard.ListGamesByPrefix(namePrefix);
            if (matchedGames.Any())
            {
                return string.Join(", ", matchedGames);
            }

            return "No matches";
        }
    }

    public class Scoreboard
    {
        private SortedDictionary<string, OrderedBag<ScoreboardEntry>> elements;
        private Dictionary<string, string> users;
        private OrderedDictionary<string, string> games;

        public Scoreboard()
        {
            this.elements = new SortedDictionary<string, OrderedBag<ScoreboardEntry>>();
            this.users = new Dictionary<string, string>();
            this.games = new OrderedDictionary<string, string>(StringComparer.Ordinal);
        }

        public bool RegisterUser(string username, string password)
        {
            if (this.users.ContainsKey(username))
            {
                return false;
            }

            this.users[username] = password;
            return true;
        }

        public bool RegisterGame(string game, string password)
        {

            if (this.games.ContainsKey(game))
            {
                return false;
            }

            this.games[game] = password;
            return true;
        }

        public bool AddScore(string username, string userPassword,
            string game, string gamePassword, int score)
        {
            bool invalidAdding = !this.users.ContainsKey(username) ||
                                 this.users[username] != userPassword ||
                                 !this.games.ContainsKey(game) ||
                                 this.games[game] != gamePassword;

            if (invalidAdding)
            {
                return false;
            }

            if (!this.elements.ContainsKey(game))
            {
                this.elements[game] = new OrderedBag<ScoreboardEntry>();
            }

            var entry = new ScoreboardEntry(score, username);

            this.elements[game].Add(entry);

            return true;
        }

        public IEnumerable<ScoreboardEntry> ShowScoreboard(string game)
        {
            if (!this.games.ContainsKey(game))
            {
                return null;
            }

            if (!this.elements.ContainsKey(game))
            {
                return Enumerable.Empty<ScoreboardEntry>();
            }

            return this.elements[game].Take(10);
        }

        public bool DeleteGame(string game, string gamePassword)
        {
            if (!this.games.ContainsKey(game))
            {
                return false;
            }

            if (this.games[game] != gamePassword)
            {
                return false;
            }

            this.games.Remove(game);
            this.elements.Remove(game);

            return true;
        }

        public IEnumerable<string> ListGamesByPrefix(string gameNamePrefix)
        {
            string upperRange = gameNamePrefix + char.MaxValue;

            return this.games.Range(gameNamePrefix, true, upperRange, true).Keys.Take(10);
        }
    }

    public class ScoreboardEntry : IComparable<ScoreboardEntry>
    {
        public ScoreboardEntry(int score, string username)
        {
            this.Score = score;
            this.Username = username;
        }

        public int Score { get; set; }

        public string Username { get; set; }


        public int CompareTo(ScoreboardEntry other)
        {
            if (this.Score.CompareTo(other.Score) == 0)
            {
                return this.Username.CompareTo(other.Username);
            }

            return other.Score.CompareTo(this.Score);
        }
    }
}