using Newtonsoft.Json;
using ObjectOrientedPrograms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ObjectOrientedPrograms.Repository
{
    public class InventoryManager
    {
        public string FilePath = @"D:\RFP-244\Object-Oriented-Programs\Object-Oriented-Programs\ObjectOrientedPrograms\JsonFile\InventoryData.json";

        public void CalcInventoryValue()
        {
            var jsonData = File.ReadAllText(FilePath);
            var inventoryData = JsonConvert.DeserializeObject<InventoryModel>(jsonData);

            foreach (var Rice in inventoryData.Rice)
            {
                Console.WriteLine(
                    Rice.Name + "\n" +
                    Rice.Weight + "\n" +
                    Rice.PricePerKG
                    );
                Console.WriteLine($"Total Price of {Rice.Name} is   : Rs. {Rice.PricePerKG * Rice.Weight}\n");
            }
            foreach (var Pulses in inventoryData.Pulses)
            {
                Console.WriteLine(
                    Pulses.Name + "\n" +
                    Pulses.Weight + "\n" +
                    Pulses.PricePerKG
                    );
                Console.WriteLine($"Total Price of {Pulses.Name} is : Rs. {Pulses.PricePerKG * Pulses.Weight}\n");
            }
            foreach (var Wheats in inventoryData.Wheats)
            {
                Console.WriteLine(
                    Wheats.Name + "\n" +
                    Wheats.Weight + "\n" +
                    Wheats.PricePerKG
                    );
                Console.WriteLine($"Total Price of {Wheats.Name} is : Rs. {Wheats.PricePerKG * Wheats.Weight}\n");
            }

            Console.WriteLine("\n==========Inventory(Weight in Kg, Price in Rs.)==========\n" + jsonData);
            Console.ReadLine();
        }
    }
}