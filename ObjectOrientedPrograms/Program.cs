using ObjectOrientedPrograms.Repository;

namespace ObjectOrientedPrograms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Object Oriented Program\n");


            Console.WriteLine("\nPress 1 to Enter");
            int run = Convert.ToInt32(Console.ReadLine());
            while (run == 1)
            {
                CommercialDataProcessing objDataProcessing = new CommercialDataProcessing();
                Console.WriteLine("\nSelect an Option from below: \n1.Buy \n2.Sell \n3.Total Value of Account");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        {
                            objDataProcessing.CompanyStockList();
                            objDataProcessing.Buy();
                            break;
                        }
                    case 2:
                        {
                            objDataProcessing.CustomerStockList();
                            objDataProcessing.Sell();
                            break;
                        }
                    case 3:
                        {
                            objDataProcessing.valueOf();
                            break;
                        }
                }
                Console.WriteLine("\nPress 1 to Enter \nPress 2 to Exit");
                run = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}