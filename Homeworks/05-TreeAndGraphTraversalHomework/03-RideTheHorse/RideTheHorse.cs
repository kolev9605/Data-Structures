namespace _03_RideTheHorse
{
    using System;
    using System.Collections.Generic;

    public class RideTheHorse
    {
        private static List<Tuple<int, int>> horseMovements = new List<Tuple<int, int>>
        {
            Tuple.Create(-2, -1),
            Tuple.Create(-1, -2),
            Tuple.Create(+1, -2),
            Tuple.Create(+2, -1),
            Tuple.Create(+2, +1),
            Tuple.Create(+1, +2),
            Tuple.Create(-1, +2),
            Tuple.Create(-2, +1)
        };

        public static void Main()
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            int startRow = int.Parse(Console.ReadLine());
            int startCol = int.Parse(Console.ReadLine());

            Cell[,] matrix = new Cell[rows, cols];
            Queue<Cell> queue = new Queue<Cell>();
            Cell startCell = new Cell(startRow, startCol, 1, true);

            matrix[startRow, startCol] = startCell;
            queue.Enqueue(startCell);

            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();
                Spread(currentCell, matrix, queue);
            }

            Print(matrix);
        }

        private static void Print(Cell[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j]);
                }

                Console.WriteLine();
            }
        }

        private static void Spread(Cell currenCell, Cell[,] matrix, Queue<Cell> queue)
        {
            foreach (var horseMovement in horseMovements)
            {
                var cell = TryPlace(currenCell, matrix, horseMovement.Item1, horseMovement.Item2);
                if (cell != null)
                {
                    queue.Enqueue(cell);
                    matrix[cell.Row, cell.Col] = cell;
                }
            }
        }

        private static Cell TryPlace(Cell currenCell, Cell[,] matrix, int deltaRow, int deltaCol)
        {
            var cell = new Cell(currenCell.Row + deltaRow, currenCell.Col + deltaCol, currenCell.Value + 1, true);
            bool canPlace = ValidateRange(cell, matrix);
            if (canPlace && matrix[cell.Row, cell.Col] == null)
            {
                return cell;
            }

            return null;
        }

        private static bool ValidateRange(Cell cell, Cell[,] matrix)
        {
            bool isInRows = cell.Row >= 0 && cell.Row < matrix.GetLength(0);
            bool isInCols = cell.Col >= 0 && cell.Col < matrix.GetLength(1);

            return isInRows && isInCols;
        }
    }
}