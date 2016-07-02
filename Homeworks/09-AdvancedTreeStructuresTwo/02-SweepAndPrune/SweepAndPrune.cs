namespace _02_SweepAndPrune
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _01_QuadTree;

    class SweepAndPrune
    {
        static void Main(string[] args)
        {
            var gameObjects = ReadGameObjects();

            int turnCount = 1;

            while (true)
            {
                string command = Console.ReadLine();
                if (command != "tick")
                {
                    ExecuteMoveCommand(gameObjects, command);
                }
                
                GetCollisions(gameObjects, turnCount);
                turnCount++;
            }
        }

        private static List<GameObject> ReadGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            while (true)
            {
                string command = Console.ReadLine();
                if (command == "start")
                {
                    break;
                }

                var tokens = command.Split(' ').ToArray();
                var name = tokens[1];
                var x1 = int.Parse(tokens[2]);
                var y1 = int.Parse(tokens[3]);

                var gameObject = new GameObject(name, x1, y1);
                gameObjects.Add(gameObject);
            }

            return gameObjects;
        }

        private static void ExecuteMoveCommand(List<GameObject> gameObjects, string command)
        {
            var tokens = command.Split(' ');
            var name = tokens[1];
            var newX = int.Parse(tokens[2]);
            var newY = int.Parse(tokens[3]);
            var gameObject = gameObjects.FirstOrDefault(x => x.Name == name);
            int index = gameObjects.IndexOf(gameObject);
            gameObjects[index].Bounds.X1 = newX;
            gameObjects[index].Bounds.Y1 = newY;
        }

        private static void GetCollisions(List<GameObject> gameObjects, int turnCount)
        {
            gameObjects.Sort();
            for (int i = 0; i < gameObjects.Count; i++)
            {
                var currentObject = gameObjects[i];
                for (int j = i + 1; j < gameObjects.Count; j++)
                {
                    var candidateObject = gameObjects[j];
                    if (currentObject.Bounds.X2 < candidateObject.Bounds.X1)
                    {
                        break;
                    }

                    bool intersects =
                        currentObject.Bounds.X1 <= candidateObject.Bounds.X2 &&
                        candidateObject.Bounds.X1 <= currentObject.Bounds.X2 &&
                        currentObject.Bounds.Y1 <= candidateObject.Bounds.Y2 &&
                        candidateObject.Bounds.Y1 <= currentObject.Bounds.Y2;

                    if (intersects)
                    {
                        Console.WriteLine("({0}) {1} collides with {2}", turnCount, currentObject.Name, candidateObject.Name);
                    }
                }
            }
        }
    }

    class GameObject : IBoundable, IComparable<GameObject>
    {
        public GameObject(string name, int x, int y)
        {
            this.Name = name;
            this.Bounds = new Rectangle(x, y, 10, 10);
        }

        public string Name { get; set; }

        public Rectangle Bounds { get; set; }

        public int CompareTo(GameObject other)
        {
            return this.Bounds.X1.CompareTo(other.Bounds.X1);
        }
    }
}
