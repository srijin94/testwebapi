using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestWebApi.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Desc { get; private set; }
        public decimal Cost { get; private set; }
        private Product() { } // EF Core

        public Product(string name_val, decimal price_val, int stock_val,string desc_val, decimal cost_val )
        {
            if (string.IsNullOrWhiteSpace(name_val))
                throw new ArgumentException("Product name is required");

            if (price_val <= 0)
                throw new ArgumentException("Price must be greater than zero");

            if (stock_val < 0)
                throw new ArgumentException("Stock cannot be negative");

            // id = 1;
            Name = name_val;
            Price = price_val;
            Stock = stock_val;
            Desc = desc_val;
            Cost = cost_val;
        }

        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            if (Stock < quantity)
                throw new InvalidOperationException("Not enough stock");

            Stock -= quantity;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Price must be greater than zero");

            Price = newPrice;
        }
        public void Update(string name_val, decimal price_val, int stock_val)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name is required");

            if (Price <= 0)
                throw new ArgumentException("Price must be greater than zero");

            if (Stock < 0)
                throw new ArgumentException("Stock cannot be negative");

            Name = name_val;
            Price = price_val;
            Stock = stock_val;
        }
    }
}

