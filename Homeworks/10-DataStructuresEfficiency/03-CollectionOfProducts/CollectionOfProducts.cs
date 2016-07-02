namespace _03_CollectionOfProducts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class CollectionOfProducts
    {
        private Dictionary<string, Product> productsById;
        private OrderedDictionary<decimal, SortedSet<Product>> productsByRange;
        private Dictionary<string, SortedSet<Product>> productsByTitle;
        private Dictionary<Tuple<string, decimal>, SortedSet<Product>> productsByTitleAndPrice;
        private Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsByTitleAndRange;
        private Dictionary<Tuple<string, decimal>, SortedSet<Product>>  productsBySupplierAndPrice;
        private Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>> productsBySupplierAndRange;

        public CollectionOfProducts()
        {
            this.productsById = new Dictionary<string, Product>();
            this.productsByRange = new OrderedDictionary<decimal, SortedSet<Product>>();
            this.productsByTitle = new Dictionary<string, SortedSet<Product>>();
            this.productsByTitleAndPrice = new Dictionary<Tuple<string, decimal>, SortedSet<Product>>();
            this.productsByTitleAndRange = new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();
            this.productsBySupplierAndPrice = new Dictionary<Tuple<string, decimal>, SortedSet<Product>>();
            this.productsBySupplierAndRange = new Dictionary<string, OrderedDictionary<decimal, SortedSet<Product>>>();
        }

        //Add new product(if the id already exists, the new product replaces the old one)
        public void AddProduct(string id, string title, string supplier, decimal price)
        {
            var product = new Product(id, title, supplier, price);

            //add in productsById
            this.productsById[id] = product;

            //add in productsByRange
            if (!this.productsByRange.ContainsKey(price))
            {
                this.productsByRange[price] = new SortedSet<Product>();
            }

            this.productsByRange[price].Add(product);

            //add in productsByTitle
            if (!this.productsByTitle.ContainsKey(title))
            {
                this.productsByTitle[title] = new SortedSet<Product>();
            }

            this.productsByTitle[title].Add(product);

            //add in productsByTitleAndPrice
            var titlePrice = Tuple.Create(title, price);
            if (!this.productsByTitleAndPrice.ContainsKey(titlePrice))
            {
                this.productsByTitleAndPrice[titlePrice] = new SortedSet<Product>();
            }

            this.productsByTitleAndPrice[titlePrice].Add(product);

            //add in productsByTitleAndRange
            if (!this.productsByTitleAndRange.ContainsKey(title))
            {
                this.productsByTitleAndRange[title] = new OrderedDictionary<decimal, SortedSet<Product>>();
            }

            if (!this.productsByTitleAndRange[title].ContainsKey(price))
            {
                this.productsByTitleAndRange[title][price] = new SortedSet<Product>();
            }

            this.productsByTitleAndRange[title][price].Add(product);

            //add in productsBySupplierAndPrice
            var supplierPrice = Tuple.Create(supplier, price);
            if (!this.productsBySupplierAndPrice.ContainsKey(supplierPrice))
            {
                this.productsBySupplierAndPrice[supplierPrice] = new SortedSet<Product>();
            }

            this.productsBySupplierAndPrice[supplierPrice].Add(product);

            //add in productsBySupplierAndRange
            if (!this.productsBySupplierAndRange.ContainsKey(supplier))
            {
                this.productsBySupplierAndRange[supplier] = new OrderedDictionary<decimal, SortedSet<Product>>();
            }

            if (!this.productsBySupplierAndRange[supplier].ContainsKey(price))
            {
                this.productsBySupplierAndRange[supplier][price] = new SortedSet<Product>();
            }

            this.productsBySupplierAndRange[supplier][price].Add(product);
        }

        //Remove product by id – returns true or false
        public bool RemoveProduct(string id)
        {
            //remove from productsById
            if (!this.productsById.ContainsKey(id))
            {
                return false;
            }

            var product = this.productsById[id];
            this.productsById.Remove(id);

            //remove from productsByrange
            if (!this.productsByRange.ContainsKey(product.Price))
            {
                return false;
            }

            this.productsByRange[product.Price].Remove(product);

            //remove from productsByTitle
            if (!this.productsByTitle.ContainsKey(product.Title))
            {
                return false;
            }

            this.productsByTitle[product.Title].Remove(product);

            //remove from productsByTitleAndPrice
            var titlePrice = Tuple.Create(product.Title, product.Price);
            if (!this.productsByTitleAndPrice.ContainsKey(titlePrice))
            {
                return false;
            }

            this.productsByTitleAndPrice[titlePrice].Remove(product);

            //remove from productsByTitleAndRange
            if (!this.productsByTitleAndRange.ContainsKey(product.Title))
            {
                return false;
            }

            if (!this.productsByTitleAndRange[product.Title].ContainsKey(product.Price))
            {
                return false;
            }

            this.productsByTitleAndRange[product.Title][product.Price].Remove(product);

            //remove from productsBySupplierAndPrice
            var supplierPrice = Tuple.Create(product.Supplier, product.Price);
            if (!this.productsBySupplierAndPrice.ContainsKey(supplierPrice))
            {
                return false;
            }

            this.productsBySupplierAndPrice[supplierPrice].Remove(product);

            //remove from productsBySupplierAndRange
            if (!this.productsBySupplierAndRange.ContainsKey(product.Supplier))
            {
                return false;
            }

            if (!this.productsBySupplierAndRange[product.Supplier].ContainsKey(product.Price))
            {
                return false;
            }

            this.productsBySupplierAndRange[product.Supplier][product.Price].Remove(product);

            return true;
        }

        //Find products in given price range[x…y] – returns the products sorted by id
        public IEnumerable<Product> FindInPriceRange(decimal from, decimal to)
        {
            return this.productsByRange.Range(from, true, to, true).SelectMany(x => x.Value);
        }

        //Find products by title – returns the products sorted by id
        public IEnumerable<Product> FindByTitle(string title)
        {
            if (!this.productsByTitle.ContainsKey(title))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsByTitle[title];
        }

        //Find products by title + price – returns the products sorted by id
        public IEnumerable<Product> FindByTitleAndPrice(string title, decimal price)
        {
            var titlePrice = Tuple.Create(title, price);
            if (!this.productsByTitleAndPrice.ContainsKey(titlePrice))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsByTitleAndPrice[titlePrice];
        }

        //Find products by title + price range – returns the products sorted by id
        public IEnumerable<Product> FindByTitleAndPriceRange(string title, decimal from, decimal to)
        {
            if (!this.productsByTitleAndRange.ContainsKey(title))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsByTitleAndRange[title].Range(from, true, to, true).SelectMany(x => x.Value);
        }

        //Find products by supplier + price – returns the products sorted by id
        public IEnumerable<Product> FindBySupplierAndPrice(string supplier, decimal price)
        {
            var supplierPrice = Tuple.Create(supplier, price);
            if (!this.productsBySupplierAndPrice.ContainsKey(supplierPrice))
            {
                return Enumerable.Empty<Product>();
            }

            return this.productsBySupplierAndPrice[supplierPrice];
        }

        //Find products by supplier + price range – returns the products sorted by id
        public IEnumerable<Product> FindProductsBySupplierAndPriceRange(string supplier, decimal from, decimal to)
        {
            return this.productsBySupplierAndRange[supplier].Range(from, true, to, true).SelectMany(x => x.Value);
        }
    }
}
