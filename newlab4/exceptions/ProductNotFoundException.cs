using System;
namespace newlab4.Exceptions{
    public class ProductNotFoundException : Exception{
        public ProductNotFoundException() : base("Product not found"){}
    }
}