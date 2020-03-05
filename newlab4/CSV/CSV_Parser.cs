using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace newlab4
{
    static public class CsvParser
    {
        static public Dictionary<int, Shop> ParseShops(StreamReader pathCsv)
        {
            Dictionary<int, Shop> shops = new Dictionary<int, Shop>();
            string line;
            while ((line = pathCsv.ReadLine()) != null)
            {
                string[] columns = line.Split(',');
                string id = columns[0];
                string name = columns[1];
                if (columns.Length != 2)
                {
                    Console.WriteLine("Bad csv Products file");
                    continue;
                }

                if (int.TryParse(id, out var shopId))
                {

                }
                Shop shop = new Shop(shopId, name);
                shops.Add(shopId, shop);
            }
            return shops;
        }

        static public List<Product> ParseProducts(StreamReader pathCsv, Dictionary<int, Shop> shops)
        {
            string line;
            List<Product> products = new List<Product>();
            while ((line = pathCsv.ReadLine()) != null)
            {

                string[] columns = line.Split(',');
                if ((columns.Length - 1) % 3 != 0)
                {
                    Console.WriteLine("Bad csv Products line");
                    continue;
                }
                for (var i = 1; i < columns.Length; i += 3)
                {
                    Product product = new Product
                    {
                        Name = columns[0]
                    };

                    if (Int32.TryParse(columns[i], NumberStyles.Number,
                     new CultureInfo("en-US"), out int shopId) &&
                         Int32.TryParse(columns[i + 1], NumberStyles.Number,
                     new CultureInfo("en-US"), out int count) &&
                         Double.TryParse(columns[i + 2], NumberStyles.Number,
                     new CultureInfo("en-US"), out double price))
                    {
                        product.ShopId = shopId;
                        product.Count = count;
                        product.Price = price;
                    }
                    else
                    {
                        continue;
                    }
                    if (shops.TryGetValue(shopId, out var outShop))
                    {
                        product.Shop = outShop;
                        if (outShop.Products == null)
                        {
                            outShop.Products = new List<Product>();
                        }
                        outShop.Products.Add(product);
                    }
                    products.Add(product);
                }

            }
            return products;

        }


    }

}