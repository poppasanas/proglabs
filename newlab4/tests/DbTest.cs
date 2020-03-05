using newlab4.Exceptions;
using System;
using System.Collections.Generic;

namespace newlab4.Tests{
    static class DbTest{
        public static void Start(){
            DAO dao = new DAO();
            var data = dao.Data;
            try{
                dao.TryConnect();
            }
            catch (DatabaseNotExistException e){
                Console.WriteLine("MSSQL LocalDB doesn't install");
            }
            data.CreateShop(100, "Перекресток");
            //data.CreateProduct("Молоко Домик в Деревне", 72, 185, 100);
            //data.CreateProduct("Телевизор PHILIPS", 22000, 5, 1);
            //data.CreateProduct("Телевизор PHILIPS", 23000, 5, 100);
            var restockList = new List<Product>
            {
                new Product("Шоколад ‘Аленка’", 50, 121) { ShopId = 1 },
                new Product("Шоколад ‘Аленка’", 60, 121) { ShopId = 2 },
                new Product("Телевизор PHILIPS", 21000, 153) { ShopId = 2 },
                new Product("Телевизор PHILIPS", 23000, 145) { ShopId = 100 },
                new Product("Телевизор PHILIPS", 22000, 138) { ShopId = 1 },
                new Product("Молоко Домик в Деревне", 72, 83) { ShopId = 2 },
            };
            //data.RestockProducts(restockList);
            //Console.WriteLine(data.BuyOneProduct("Телевизор PHILIPS", 1));
            var howMuch = data.GetHowMuchCanBuy(1, 5000);
            Dictionary<string, int> buyList = new Dictionary<string, int>();
            buyList.Add("Телевизор PHILIPS", 10);
            buyList.Add("Шоколад ‘Аленка’", 22);
            var lowestPriceShop = data.FindLowestPriceShop(buyList);
            dao.DbToCsv();
        }
    }
}