namespace _08_DistanceInLabyrinth
{
    public class Cell
    {
        public Cell(int row, int col, int level, string value)
        {
            this.Level = level;
            this.Value = value;
            this.Row = row;
            this.Col = col;
        }

        public int Level { get; set; }

        public string Value { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}