using System.Collections.Generic;

namespace newlab4{
    interface IDataWorker{
        public void CreateShop(int id, string name);
        public void CreateProduct(string name, double price, int count, int shopId);
        public void RestockProducts(List<Product> products);
        public Shop FindLowestPriceShop(Dictionary<string, int> products);
        public List<(int count, Product prod)> GetHowMuchCanBuy(int shopId, double money);
        public double BuyOneProduct(string name, int count);
        public double BuyOneProduct(string name, int count, int shopId);
        public double BuyListProduct(Dictionary<string, (int count, int shopId)> buyList);
        public Shop FindLowestPriceShop(string name);
        public List<Product> GetSortedProductList(string name);
        public Dictionary<string, List<Product>> GetProductByName();
        public void Save();
    }
}