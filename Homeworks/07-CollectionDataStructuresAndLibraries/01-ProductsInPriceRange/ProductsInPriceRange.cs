namespace _01_ProductsInPriceRange
{
    using System;
    using Wintellect.PowerCollections;

    class ProductsInPriceRange
    {
        static void Main()
        {
            OrderedMultiDictionary<decimal, string> products = new OrderedMultiDictionary<decimal, string>(true);

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split();
                string productName = input[0];
                decimal productPrice = decimal.Parse(input[1]);
                products.Add(productPrice, productName);
            }

            string[] range = Console.ReadLine().Split();
            decimal from = decimal.Parse(range[0]);
            decimal to = decimal.Parse(range[1]);

            foreach (var product in products.Range(from, true, to, true))
            {
                Console.WriteLine("{0} {1}", product.Key, string.Join(", ", product.Value));
            }
        }
    }
}
