namespace _08_DistanceInLabyrinth
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DistanceInLabyrinth
    {
        public const string StartingPosition = "*";
        public const string FullCells = "x";
        public const string EmptyCells = "0";

        public static void Main(string[] args)
        {
            string[,] matrix =
            {
                {"0", "0", "0", "x", "0", "x"},
                {"0", "x", "0", "x", "0", "x"},
                {"0", "*", "x", "0", "x", "0"},
                {"0", "x", "0", "0", "0", "0"},
                {"0", "0", "0", "x", "x", "0"},
                {"0", "0", "0", "x", "0", "x"}
            };

            var startingCell = LocateStartingPoint(matrix);

            var queue = new Queue<Cell>();
            queue.Enqueue(startingCell);

            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();
                TryEnqueueAdjacents(queue, currentCell, currentCell.Level + 1, matrix);
                while (queue.Count > 0)
                {
                    var adjecent = queue.Peek();
                    if (adjecent.Level != currentCell.Level + 1)
                    {
                        break;
                    }

                    adjecent = queue.Dequeue();
                    TryEnqueueAdjacents(queue, adjecent, adjecent.Level + 1, matrix);
                }
            }

            MarkUnreachableCells(matrix);
            PrintMatrix(matrix);
        }

        private static void MarkUnreachableCells(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == EmptyCells)
                    {
                        matrix[i, j] = "u";
                    }
                }
            }
        }

        private static void PrintMatrix(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "  ");
                }

                Console.WriteLine();
            }
        }

        private static void TryEnqueueAdjacents(Queue<Cell> queue, Cell currentCell, int level, string[,] matrix)
        {
            var left = CreateCell(new Cell(currentCell.Row, currentCell.Col - 1, level, level.ToString()), matrix);
            var right = CreateCell(new Cell(currentCell.Row, currentCell.Col + 1, level, level.ToString()), matrix);
            var up = CreateCell(new Cell(currentCell.Row - 1, currentCell.Col, level, level.ToString()), matrix);
            var down = CreateCell(new Cell(currentCell.Row + 1, currentCell.Col, level, level.ToString()), matrix);

            if (left != null && left.Value == EmptyCells)
            {
                queue.Enqueue(left);
                matrix[left.Row, left.Col] = level.ToString();
            }

            if (right != null && right.Value == EmptyCells)
            {
                queue.Enqueue(right);
                matrix[right.Row, right.Col] = level.ToString();
            }

            if (up != null && up.Value == EmptyCells)
            {
                queue.Enqueue(up);
                matrix[up.Row, up.Col] = level.ToString();

            }

            if (down != null && down.Value == EmptyCells)
            {
                queue.Enqueue(down);
                matrix[down.Row, down.Col] = level.ToString();
            }
        }

        private static Cell CreateCell(Cell cell, string [,] matrix)
        {
            if (cell.Row >= 0 && cell.Row < matrix.GetLength(0) && cell.Col >= 0 && cell.Col < matrix.GetLength(1))
            {
                cell.Value = matrix[cell.Row, cell.Col];
                return cell;
            }

            return null;
        }

        private static Cell LocateStartingPoint(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == StartingPosition)
                    {
                        var cell = new Cell(i, j, 0, StartingPosition);

                        return cell;

                    }
                }
            }

            return null;
        }
    }
}