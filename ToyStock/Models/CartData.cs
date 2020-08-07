using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToyStock.Models
{
    public class CartData
    {
        public Product Product { get; set; }
        public int Count { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }

        public CartData(Product product, int count, string size = null, string color = null)
        {
            this.Product = product;
            this.Count = count;
            this.Size = size;
            this.Color = color;
        }
    }
}