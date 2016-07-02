namespace _03_RideTheHorse
{
    public class Cell
    {
        public Cell(int row, int col, int value, bool isVisited)
        {
            this.Row = row;
            this.Col = col;
            this.Value = value;
            this.IsVisited = isVisited;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public int Value { get; set; }

        public bool IsVisited { get; set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}