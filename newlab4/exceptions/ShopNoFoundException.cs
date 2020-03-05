using System;
namespace newlab4.Exceptions{
    public class ShopNoFoundException : Exception{
        public ShopNoFoundException() : base("Shop not found"){}
    }
}