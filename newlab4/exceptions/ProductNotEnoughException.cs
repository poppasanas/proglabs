using System;
namespace newlab4.Exceptions{
    public class ProductNotEnoughException : Exception{
        public ProductNotEnoughException() : base("Not enough products"){}
    }
}