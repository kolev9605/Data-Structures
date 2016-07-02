namespace Escape_from_Labyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class EscapeFromLabyrinth
    {
        private const char VisitedCell = 's';

        private static int width;
        private static int height;

        private static char[,] labyrinth;

        public static void Main()
        {
            ReadLabyrinth();
            string shortestPathToExit = FindShortestPathToExit();
            if (shortestPathToExit == null)
            {
                Console.WriteLine("No exit!");
            }
            else if (shortestPathToExit == "")
            {
                Console.WriteLine("Start is at the exit.");
            }
            else
            {
                Console.WriteLine("Shortest exit: {0}", shortestPathToExit);
            }
        }

        static string FindShortestPathToExit()
        {
            var queue = new Queue<Point>();
            var startingPosition = FindStartPosition();
            if (startingPosition == null)
            {
                return null;
            }

            queue.Enqueue(startingPosition);
            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();
                //Console.WriteLine("Visited cell: ({0}, {1})", currentCell.X, currentCell.Y);
                if (IsExit(currentCell))
                {
                    return TracePathBack(currentCell);
                }

                TryDirection(queue, currentCell, "U", 0, -1);
                TryDirection(queue, currentCell, "R", +1, 0);
                TryDirection(queue, currentCell, "D", 0, +1);
                TryDirection(queue, currentCell, "L", -1, 0);
            }

            return null;
        }

        private static void TryDirection(Queue<Point> queue, Point currentCell, string direction, int deltaX, int deltaY)
        {
            int newX = currentCell.X + deltaX;
            int newY = currentCell.Y + deltaY;
            if (newX >= 0 &&
                newX < width &&
                newY >= 0 &&
                newY < height &&
                labyrinth[newY, newX] == '-')
            {
                labyrinth[newY, newX] = VisitedCell;
                var nextCell = new Point()
                {
                    X = newX,
                    Y = newY,
                    Direction = direction,
                    PreviousPoint = currentCell
                };

                //PrintLabyrinth();
                queue.Enqueue(nextCell);
            }
        }

        private static void PrintLabyrinth()
        {
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    Console.Write(labyrinth[i,j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static string TracePathBack(Point currentCell)
        {
            var path = new StringBuilder();
            while (currentCell.PreviousPoint != null)
            {
                path.Append(currentCell.Direction);
                currentCell = currentCell.PreviousPoint;
            }

            var pathReversed = new StringBuilder();
            for (int i = path.Length - 1; i >= 0; i--)
            {
                pathReversed.Append(path[i]);
            }

            return pathReversed.ToString();
        }

        private static bool IsExit(Point currentCell)
        {
            bool isOnBorderX = currentCell.X == 0 || currentCell.Y == height - 1;
            bool isOnBorderY = currentCell.Y == 0 || currentCell.X == width - 1;

            return isOnBorderY || isOnBorderX;
        }

        private static Point FindStartPosition()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (labyrinth[y, x] == VisitedCell)
                    {
                        return new Point() { X = x, Y = y };
                    }
                }
            }

            return null;
        }

        static void ReadLabyrinth()
        {
            width = int.Parse(Console.ReadLine());
            height = int.Parse(Console.ReadLine());
            labyrinth = new char[height,width];
            for (int i = 0; i < height; i++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int j = 0; j < width; j++)
                {
                    labyrinth[i, j] = line[j];
                }
            }
        }
    }
}
