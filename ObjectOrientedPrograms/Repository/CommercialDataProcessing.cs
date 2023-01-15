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
    public class CommercialDataProcessing
    {
        public string FilePath = @"D:\RFP-244\Object-Oriented-Programs\Object-Oriented-Programs\ObjectOrientedPrograms\JsonFile\CustomerData.json";
        public static string filePath = @"D:\RFP-244\Object-Oriented-Programs\Object-Oriented-Programs\ObjectOrientedPrograms\JsonFile\StockData.json";

        List<StockAccount> objstockAccounts = new List<StockAccount>();
        StockModel objstockData = new StockModel();

        string customerName;
        int totalBalance = 0;
        int marketSharePrice = 0;
        string marketCompanyName = " ";
        bool customerExists = false;

        public void CompanyStockAccount() //Company Stock Data
        {
            var companyAccountData = File.ReadAllText(filePath);
            objstockData = JsonConvert.DeserializeObject<StockModel>(companyAccountData);
        }

        public void StockAccount() //Customer Stock Data
        {
            var customerAccountData = File.ReadAllText(FilePath);
            objstockAccounts = JsonConvert.DeserializeObject<List<StockAccount>>(customerAccountData);
        }

        public void Buy() // To Buy the stocks
        {
            int valueOfSharesBought = 0;
            Console.Write("\nEnter Customer Name: ");
            customerName = Console.ReadLine();
            Console.Write("\nEnter the Name of Stock you want to Buy from the Company Stock list: ");
            string stockName = Console.ReadLine();
            Console.Write("\nEnter Number of Shares you want to Buy: ");
            int numOfShares = Convert.ToInt32(Console.ReadLine());

            CompanyStockAccount();
            StockAccount();
            foreach (var item in objstockData.Stock) //Stock Data debited from company account
            {
                if (item.StockName == stockName)
                {
                    foreach (var item2 in objstockAccounts)
                    {
                        if (item2.CustomerInfo.CustomerName == customerName)
                        {
                            if (item.NumOfShares >= numOfShares)
                            {
                                item.NumOfShares -= numOfShares;
                                valueOfSharesBought = numOfShares * item.SharePrice;
                                marketSharePrice = item.SharePrice;
                                marketCompanyName = item.StockName;
                            }
                            else
                            {
                                Console.WriteLine("\nNumber of Share limit exceeded. Please buy from the given Number of Shares");
                            }
                        }
                    }
                    break;
                }
            }
            saveCompany();

            //StockAccount();
            foreach (var item in objstockAccounts) //Stock Data credited to Customer Account
            {
                if (item.CustomerInfo.CustomerName == customerName)
                {
                    customerExists = true;
                    if (item.CustomerInfo.CustomerAccountBalance >= valueOfSharesBought)
                    {
                        item.CustomerInfo.CustomerAccountBalance -= valueOfSharesBought;
                        totalBalance = item.CustomerInfo.CustomerAccountBalance;
                        if (stockName == marketCompanyName)
                        {

                            foreach (ShareDetails item2 in item.ShareDetails)
                            {
                                if (item2.CompanyName == stockName)
                                {

                                    item2.NoOfShares += numOfShares;
                                    break;
                                }
                            }

                            Console.WriteLine($"\nTransaction Completed. Bought.");

                        }
                        else
                        {
                            Console.WriteLine("\nShare Details not found");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInsufficient Balance in Account");
                    }
                }
            }
            if (!customerExists)
            {
                Console.WriteLine("\nCustomer does not Exist. Try another Name");
            }
            saveCustomer();
        }

        public void Sell() // To Sell The Stocks
        {
            int valueOfSharesSold = 0;
            Console.Write("\nEnter Customer Name: ");
            customerName = Console.ReadLine();
            Console.Write("\nEnter the Name of Stock you want to Sell from the Customer Stock list: ");
            string stockName = Console.ReadLine();
            Console.Write("\nEnter Number of Shares you want to sell: ");
            int numOfShares = Convert.ToInt32(Console.ReadLine());

            StockAccount();
            foreach (var item in objstockAccounts) //Stock data debited from customer account
            {
                if (item.CustomerInfo.CustomerName == customerName)
                {
                    customerExists = true;
                    foreach (ShareDetails item2 in item.ShareDetails)
                    {
                        if (item2.CompanyName == stockName)
                        {
                            if (item2.NoOfShares >= numOfShares)
                            {
                                item2.NoOfShares -= numOfShares;
                                marketSharePrice = item2.PricePerShare;

                            }
                            else
                            {
                                Console.WriteLine("Share limit exceeded, try again");
                            }
                            break;
                        }
                    }
                    item.CustomerInfo.CustomerAccountBalance += numOfShares * marketSharePrice;
                    totalBalance = item.CustomerInfo.CustomerAccountBalance;
                    valueOfSharesSold = numOfShares * marketSharePrice;
                    break;
                }
            }
            if (!customerExists)
            {
                Console.WriteLine("\nCustomer Name does not Exist. Try another name.");
            }
            saveCustomer();

            CompanyStockAccount();
            foreach (var item in objstockData.Stock) // Stock data credited to customer account
            {
                if (item.StockName == stockName)
                {
                    foreach (var item2 in objstockAccounts)
                    {
                        if (item2.CustomerInfo.CustomerName == customerName)
                        {
                            item.NumOfShares += numOfShares;
                            break;
                        }
                    }
                }
            }
            saveCompany();
            Console.WriteLine($"\nTransaction Completed. Sold.");

        }

        public void saveCompany()
        {
            string stockDebited = JsonConvert.SerializeObject(objstockData);
            File.WriteAllText(filePath, stockDebited);
        }

        public void saveCustomer()
        {
            string newStockAdded = JsonConvert.SerializeObject(objstockAccounts);
            File.WriteAllText(FilePath, newStockAdded);
        }

        public void valueOf() //To calculate total value of all the stocks of a Customers Account
        {
            StockAccount();
            int totalValue = 0;
            Console.Write("\nEnter Customer Name: ");
            string customerName = Console.ReadLine();

            foreach (var item in objstockAccounts)
            {
                if (item.CustomerInfo.CustomerName == customerName)
                {
                    foreach (var shares in item.ShareDetails)
                    {
                        totalValue += shares.NoOfShares * shares.PricePerShare;
                    }
                }
            }
            Console.WriteLine($"\nTotal Value of {customerName} is : Rs.{totalValue}");
        }

        public void CompanyStockList()
        {
            CompanyStockAccount();
            Console.WriteLine("\n==========Company Stock List==========");

            foreach (var stocks in objstockData.Stock)
            {
                Console.WriteLine(
                "\nStock Name        : " + stocks.StockName + "\n" +
                "Number of Shares  : " + stocks.NumOfShares + "\n" +
                "Price per Share   : " + stocks.SharePrice
                );
            }
        }

        public void CustomerStockList()
        {
            StockAccount();
            Console.WriteLine("\n==========Customer Stock List==========");

            foreach (var info in objstockAccounts)
            {
                Console.WriteLine(
                "\nStock Name        : " + info.CustomerInfo.CustomerName + "\n" +
                "Number of infos  : " + info.CustomerInfo.CustomerPhoneNo + "\n" +
                "Price per info   : " + info.CustomerInfo.CustomerEmail + "\n" +
                "Price per info   : " + info.CustomerInfo.CustomerAddress + "\n" +
                "Price per info   : " + info.CustomerInfo.CustomerAccountBalance
                );

                foreach (var shares in info.ShareDetails)
                {
                    Console.WriteLine(
                    "\nStock Name        : " + shares.CompanyName + "\n" +
                    "Number of infos  : " + shares.NoOfShares + "\n" +
                    "Price per info   : " + shares.PricePerShare
                    );
                }
            }
        }
    }

}