using newlab4.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace newlab4{
    class CsvWorker : IDataWorker{
        Dictionary<int, Shop> Shops { get; }
        List<Product> Products { get; }
        public CsvWorker(StreamReader productFile, StreamReader shopsFile){
            Shops = CsvParser.ParseShops(shopsFile);
            Products = CsvParser.ParseProducts(productFile, Shops);
        }
        public void CreateProduct(string name, double price, int count, int shopId){
            var product = new Product(name, price, count){
                ShopId = shopId
            };

            if (Shops.TryGetValue(shopId, out var shop)){
                product.Shop = shop;
            }
            else{
                throw new ShopNotFoundException();
            }
            Products.Add(product);
        }

        public void CreateShop(int id, string name){
            if (!Shops.TryGetValue(id, out var outShop)){
                var shop = new Shop(id, name);
                Shops.Add(shop.Id, shop);
            }
        }
        public Shop FindLowestPriceShop(Dictionary<string, int> products){
            Dictionary<string, List<Product>> productsByName = new Dictionary<string, List<Product>>();
            foreach (var product in products){
                if (!Products.Any(prod => prod.Name == product.Key)){
                    throw new ProductNotFoundException();
                }
                var matchedProduct = Products.Where(prod => prod.Name == product.Key)
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
        public Shop FindLowestPriceShop(string name){
            try{
                Product product = GetSortedProductList(name)[0];
                return product.Shop;
            }
            catch (ProductNotFoundException e){
                throw e;
            }
        }
        public List<(int count, Product prod)> GetHowMuchCanBuy(int shopId, double money){
            var products = Products.Where(prod => prod.ShopId == shopId).ToList();
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
        public List<Product> GetSortedProductList(string name){
            var products = Products.Where(prod => prod.Name == name).ToList();
            products.Sort();
            return products.Count() > 0
                 ? products
                 : throw new ProductNotFoundException();
        }
        public void RestockProducts(List<Product> products){
            foreach (var product in products){
                Products.Add(product);
            }
        }

        public double BuyOneProduct(string name, int count){
            double price = 0;
            int accumulated = 0, index = 0;
            var products = GetSortedProductList(name);
            if (products != null){
                for (var i = 0; i < products.Count; i++){
                    if (products[i].Count < count - accumulated){
                        accumulated += products[i].Count;
                        price += products[i].Count * products[i].Price;
                        index = i;
                    }
                    else{
                        price += (count - accumulated) * products[i].Price;
                        accumulated += count - accumulated;
                        products[i].Count -= count - accumulated; 
                    }
                }
                for(var i = 0; i < index; i++){
                    products[i].Count = 0;
                }
                this.Save();
                if (accumulated < count){
                    throw new ProductNotEnoughException();
                }
                return price;
            }
            else{
                throw new ProductNotFoundException();
            }
        }
        public double BuyOneProduct(string name, int count, int shopId){
            var products = Products.Where(prod => prod.Name == name)
                .Where(prod => prod.ShopId == shopId)
                .ToList();
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

        public double BuyListProduct(Dictionary<string, (int count, int shopId)> buyList){
            double sum = 0;
            foreach (var product in buyList){
                try{
                    sum += BuyOneProduct(product.Key, product.Value.count, product.Value.shopId);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return sum;
        }
        public Dictionary<string, List<Product>> GetProductByName(){
            Dictionary<string, List<Product>> productByName = new Dictionary<string, List<Product>>();
            foreach (var product in Products){
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
            using StreamWriter productsFile = new StreamWriter(Settings.Default.ProductsPath, false),
                       shopsFile = new StreamWriter(Settings.Default.ShopsPath, false);
            string shopCSV = "", productCSV = "";
            
            foreach (var shop in Shops){
                shopCSV += $"{shop.Key},{shop.Value.Name}{Environment.NewLine}";
            }
            var productsByName = this.GetProductByName();
            foreach (var productList in productsByName){
                string name = productList.Key;
                productCSV += name;
                foreach (var product in productList.Value){
                    productCSV += $",{product.ShopId},{product.Count},{product.Price}";
                }
                productCSV += Environment.NewLine;
            }
            productsFile.Write(productCSV);
            shopsFile.Write(shopCSV);
        }
    }
}