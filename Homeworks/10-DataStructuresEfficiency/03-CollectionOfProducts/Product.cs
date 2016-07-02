namespace _03_CollectionOfProducts
{
    using System;

    public class Product : IComparable<Product>
    {
        public Product(string id, string title, string supplier, decimal price)
        {
            this.Id = id;
            this.Title = title;
            this.Supplier = supplier;
            this.Price = price;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Supplier { get; set; }

        public decimal Price { get; set; }

        public int CompareTo(Product other)
        {
            return this.Id.CompareTo(other.Id);
        }

        public override string ToString()
        {
            return $"{this.Id}: {this.Title} ot {this.Supplier} za {this.Price:F2}lv";
        }
    }
}