using System;
namespace newlab4.Exceptions{
    public class ShopNoSellingAllProductsException : Exception{
        public ShopNoSellingAllProductsException() : base("No shops sell these products"){}
    }
}