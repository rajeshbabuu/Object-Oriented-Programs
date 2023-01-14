using Newtonsoft.Json;
using ObjectOrientedPrograms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ObjectOrientedPrograms.Repository
{
    public class StockManager
    {
        public string FilePath = @"D:\RFP-244\Object-Oriented-Programs\Object-Oriented-Programs\ObjectOrientedPrograms\JsonFile\StockData.json";

        public void CalcStockValue()
        {
            var jsonData = File.ReadAllText(FilePath);
            
            var stockData = JsonConvert.DeserializeObject<StockModel>(jsonData);
            int valueOfEachStock;
            long valueOfAllStocks = 0;

            Console.WriteLine("\n==========Stock Report==========\n");

            foreach (var Stocks in stockData.Stocks)
            {
                Console.WriteLine(
                "Stock Name        : " + Stocks.StockName + "\n" +
                "Number of Shares  : " + Stocks.NumOfShares + "\n" +
                "Price per Share   : " + Stocks.SharePrice
                );

                valueOfEachStock = Stocks.NumOfShares * Stocks.SharePrice;
                Console.WriteLine($"Total Price of {Stocks.StockName} is : Rs. {valueOfEachStock}\n");

                valueOfAllStocks = valueOfEachStock + valueOfAllStocks;
            }
            Console.WriteLine($"The value of Total Stocks is Rs. {valueOfAllStocks}");

            Console.WriteLine("\n==========Inventory(NumOfShares, Price)==========\n" + jsonData);
            Console.ReadLine();
        }
    }
}