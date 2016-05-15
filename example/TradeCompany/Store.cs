namespace DataStructuresEfficiency
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Store
    {
        public OrderedMultiDictionary<decimal, Product> Products { get; set; } = new OrderedMultiDictionary<decimal, Product>(true);

        public void AddProduct(Product product)
        {
            this.Products[product.Price].Add(product);
        }

        public ICollection<Product> SearchInPriceRange(decimal from, decimal to)
        {
            return this.Products.Range(from, true, to, true).Values;
        }
    }
}