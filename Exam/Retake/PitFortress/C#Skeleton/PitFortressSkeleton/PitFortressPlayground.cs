namespace PitFortress
{
    using System;
    using System.Runtime.ExceptionServices;

    public class PitFortressPlayground
    {
        public static void Main()
        {
            PitFortressCollection pit = new PitFortressCollection();

            pit.AddPlayer("first", 2);
            pit.AddPlayer("second", 2);
            pit.AddPlayer("third", 3);

            pit.AddMinion(1);
            pit.AddMinion(2);
            pit.AddMinion(3);

            pit.SetMine("first", 1, 2, 50);
            pit.SetMine("first", 2, 2, 50);
            pit.SetMine("second", 2, 2, 50);

            foreach (var mine in pit.GetMines())
            {
                Console.WriteLine(mine);
            }

            
        }
    }
}
