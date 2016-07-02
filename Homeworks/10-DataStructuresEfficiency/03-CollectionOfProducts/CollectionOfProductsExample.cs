namespace _03_CollectionOfProducts
{
    using System;

    public class CollectionOfProductsExample
    {
        public static void Main(string[] args)
        {
            CollectionOfProducts collection = new CollectionOfProducts();
            collection.AddProduct("1", "baloni", "durex-China", 3.5m);
            collection.AddProduct("3", "mlqko", "gosho dilara", 10m);
            collection.AddProduct("2", "jonka", "gosho dilara", 5m);
            collection.AddProduct("6", "bastuni", "chicho gosho dyrvodeleca", 30m);
            collection.AddProduct("5", "morkovi", "jaka", 20m);
            collection.AddProduct("4", "jonka", "pesho dilara", 4.5m);

            Console.WriteLine(string.Join("", collection.FindByTitle("jonka")));
            Console.WriteLine(string.Join("\n", collection.FindBySupplierAndPrice("gosho dilara", 5m)));
            collection.RemoveProduct("5"); //remove jaka
            Console.WriteLine(string.Join("\n", collection.FindByTitle("morkovi"))); //should be empty
            Console.WriteLine(string.Join("\n", collection.FindByTitleAndPrice("jonka", 5m)));
            Console.WriteLine(string.Join("\n", collection.FindByTitleAndPriceRange("jonka", 4, 5)));
            Console.WriteLine(string.Join("\n", collection.FindInPriceRange(2, 10)));
            Console.WriteLine(string.Join("\n", collection.FindProductsBySupplierAndPriceRange("gosho dilara", 6, 10)));
        }
    }
}