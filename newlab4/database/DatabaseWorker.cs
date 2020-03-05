using newlab4.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace newlab4{
    public class DatabaseWorker : IDataWorker{
        public Database DB { get; }
        public DatabaseWorker(){
            DB = new Database();
            DB.Products.Load();
        }
        public void CreateShop(int id, string name){
            if (DB.Shops.Find(id) == null){
                var shop = new Shop(id, name);
                DB.Shops.Add(shop);
                DB.SaveChanges();
            }
        }
        public void CreateProduct(string name, double price, int count, int shopId){
            var product = new Product(name, price, count){
                ShopId = shopId
            };
            var shop = DB.Shops.Single(shop => shop.Id == shopId);
            product.Shop = shop ?? throw new Exception("Shop Not Found");
            DB.Products.Add(product);
            DB.SaveChanges();
        }
        public void RestockProducts(List<Product> products){
            foreach (var product in products){
                var editingProducts = DB.Products.Include(prod => prod.Shop)
                    .Where(prod => prod.ShopId == product.ShopId && prod.Name == product.Name)
                    .AsEnumerable()
                    .Select(prod =>{
                        prod.Price = product.Price;
                        prod.Count += product.Count;
                        return prod;
                        });
                foreach (var prod in editingProducts){
                    DB.Entry(prod).State = EntityState.Modified;
                }
            }
            DB.SaveChanges();
        }
        public Shop FindLowestPriceShop(Dictionary<string, int> products){
            Dictionary<string, List<Product>> productsByName = new Dictionary<string, List<Product>>();
            foreach (var product in products){
                if (!DB.Products.Any(prod => prod.Name == product.Key)){
                    throw new ProductNotFoundException();
                }
                var matchedProduct = DB.Products.Include("Shop")
                    .Where(prod => prod.Name == product.Key)
                    .Where(prod => prod.Count >= product.Value).ToList();
                if (matchedProduct.Count > 0){
                    matchedProduct.Sort();
                    productsByName.Add(product.Key, matchedProduct);
                }
                else{
                    throw new ProductNotEnoughException();
                }
            }
            Dictionary<Shop, double> resultShopList = new Dictionary<Shop, double>();
            Dictionary<int, List<Product>> productsByShopId = new Dictionary<int, List<Product>>();
            foreach (var list in productsByName){
                foreach (var innerListItem in list.Value){
                    if (productsByShopId.TryGetValue(innerListItem.ShopId, out var prod)){
                        prod.Add(innerListItem);
                    }
                    else{
                        List<Product> newList = new List<Product>{
                            innerListItem
                        };
                        productsByShopId.Add(innerListItem.ShopId, newList);
                    }
                }
            }
            foreach (var list in productsByShopId){
                double sum = 0;
                foreach (var innerListItem in list.Value){
                    if (products.TryGetValue(innerListItem.Name, out int count)){
                        sum += count * innerListItem.Price;
                    }
                }
                resultShopList.Add(list.Value[0].Shop, sum);
            }
            (Shop shop, double sum) min = (null, Double.MaxValue);
            foreach (var shop in resultShopList){
                if (min.sum > shop.Value){
                    min.sum = shop.Value;
                    min.shop = shop.Key;
                }
            }
            return min.shop ?? throw new ShopNoSellingAllProductsException();
            ;
        }
        public List<(int count, Product prod)> GetHowMuchCanBuy(int shopId, double money){
            var products = DB.Products.Include(prod => prod.Shop)
                .Where(prod => prod.ShopId == shopId).ToList();
            List<(int count, Product prod)> howMuch = new List<(int count, Product prod)>();
            if (products != null && products.Count > 0){
                foreach (var prod in products){
                    int count = (int)(money / prod.Price);
                    if (count > prod.Count){
                        howMuch.Add((prod.Count, prod));
                    }
                    else{
                        howMuch.Add((count, prod));
                    }
                }
                return howMuch.Count > 0
                     ? howMuch
                     : null;
            }
            else{
                return null;
            }
        }
        public double BuyOneProduct(string name, int count){
            double price = 0;
            int accumulated = 0;
            var products = GetSortedProductList(name);
            int index = 0;
            if (products != null){
                for (var i = 0; i < products.Count; i++, index++){
                    if (products[i].Count < count - accumulated){
                        accumulated += products[i].Count;
                        price += products[i].Count * products[i].Price;
                    }
                    else{
                        price += (count - accumulated) * products[i].Price;
                        accumulated += count - accumulated;
                        products[i].Count = count - accumulated;
                        index--;
                    }
                }
                if (accumulated < count){
                    throw new ProductNotEnoughException();
                }
                for (var i = 0; i < index; i++){
                    products[i].Count = 0;
                }
                DB.SaveChanges();
                return price;
            }
            else{
                throw new ProductNotFoundException();
            }
        }
        public double BuyOneProduct(string name, int count, int shopId){
            var products = DB.Products.Include(prod => prod.Shop)
                .Where(prod => prod.Name == name)
                .Where(prod => prod.ShopId == shopId).ToList();
            if (products != null && products.Count > 0){
                foreach (var prod in products){
                    if (prod.Count >= count){
                        prod.Count -= count;
                        this.Save();
                        return prod.Price * count;
                    }
                }
                throw new ProductNotEnoughException();
            }
            else{
                throw new ProductNotFoundException();
            }
        }
        public Shop FindLowestPriceShop(string name){
            try{
                Product product = GetSortedProductList(name)[0];
                return product.Shop;
            }
            catch (ProductNotFoundException e){
                throw e;
            }
        }
        public List<Product> GetSortedProductList(string name){
            Product result = new Product{
                Price = Double.MaxValue
            };
            var products = DB.Products.Include(prod => prod.Shop)
                .Where(prod => prod.Name == name).ToList();
            products.Sort();
            return products.Count() > 0
                 ? products
                 : null;
        }
        public double BuyListProduct(Dictionary<string, (int count, int shopId)> buyList){
            double sum = 0;
            foreach (var product in buyList){
                try{
                    sum += BuyOneProduct(product.Key, product.Value.count, product.Value.shopId);
                }
                catch (Exception e){
                    throw e;
                }
            }
            return sum;
        }
        public Dictionary<string, List<Product>> GetProductByName(){
            var products = DB.Products.Include(prod => prod.Shop);
            Dictionary<string, List<Product>> productByName = new Dictionary<string, List<Product>>();
            foreach (var product in products){
                if (productByName.TryGetValue(product.Name, out var outProductList)){
                    outProductList.Add(product);
                }
                else{
                    var initList = new List<Product>{
                        product
                    };
                    productByName.Add(product.Name, initList);
                }
            }
            return productByName;
        }
        public void Save(){
            DB.SaveChanges();
        }
    }
}