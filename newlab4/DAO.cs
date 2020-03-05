using newlab4.Exceptions;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace newlab4{
    class DAO{
        public IDataWorker Data { get; }
        public DAO(){
            switch (Settings.Default.WorkerType){
                case "CSV":
                    using (StreamReader productsPath = new StreamReader(Settings.Default.ProductsPath),
                        shopsPath = new StreamReader(Settings.Default.ShopsPath)){
                        Data = new CsvWorker(productsPath, shopsPath);
                    }
                    break;
                case "DB":
                    Data = new DatabaseWorker();
                    break;
                default:
                    throw new Exception("Wrong Config: 'WorkerType'");
            }
        }
        public void CsvToDb(){
            using StreamReader productsPath = new StreamReader(Settings.Default.ProductsPath),
                       shopsPath = new StreamReader(Settings.Default.ShopsPath);
            var shops = CsvParser.ParseShops(shopsPath);
            var products = CsvParser.ParseProducts(productsPath, shops);
            IDataWorker db = new DatabaseWorker();
            foreach (var shop in shops){
                db.CreateShop(shop.Key, shop.Value.Name);
            }
            foreach (var product in products){
                db.CreateProduct(product.Name, product.Price, product.Count, product.ShopId);
            }
        }
        public void DbToCsv(){
            using StreamWriter productsFile = new StreamWriter(Settings.Default.ProductsPath, false),
                       shopsFile = new StreamWriter(Settings.Default.ShopsPath, false);
            string productCSV = "";
            string shopCSV = "";
            var worker = new DatabaseWorker();
            var products = worker.DB.Products.Include(prod => prod.Shop).ToList();
            var shops = worker.DB.Shops.Include(shop => shop.Products).ToList();
            foreach (var shop in shops){
                shopCSV += $"{shop.Id},{shop.Name}{Environment.NewLine}";
            }
            var productsByName = worker.GetProductByName();
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
        public void TryConnect(){
            try{
                SqlConnection connection = new SqlConnection(Settings.Default.DbConnectString);
            }
            catch{
                throw new DatabaseNotExistException();
            }
        }
    }
}