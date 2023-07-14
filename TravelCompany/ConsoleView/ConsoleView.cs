using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Models;

namespace TravelCompany.ConsoleView
{
    public static class ConsoleView
    {
        public static Driver EnterDriver()
        {
            Driver driver = new Driver();
            Console.Write("Enter Name: ");
            driver.Name = Console.ReadLine();

            Console.Write("Enter Surname: ");
            driver.Surname = Console.ReadLine();

            Console.Write("Enter Vehicle Type: ");
            driver.VehicleType = Console.ReadLine();

            // Set Email with validation
            bool validEmail = false;
            while (!validEmail)
            {
                Console.Write("Enter Email: ");
                string emailInput = Console.ReadLine();

                if (Validate.Validate.IsValidEmail(emailInput))
                {
                    driver.Email = emailInput;
                    validEmail = true;
                }
                else
                {
                    Console.WriteLine("Invalid email format. Please try again.");
                }
            }

            // Set BaseFarePrice and BaseFareDistance using Console.Read()
            bool validBaseFarePrice = false;
            while (!validBaseFarePrice)
            {
                Console.Write("Enter Base Fare Price: ");
                string baseFarePriceInput = Console.ReadLine();
                if (double.TryParse(baseFarePriceInput, out double baseFarePriceOutput))
                {
                    driver.BaseFarePrice = baseFarePriceOutput;
                    validBaseFarePrice = true;
                }
                else
                {
                    Console.WriteLine("Invalid type. Please try again.");
                }
            }

            bool validBaseFareDistance = false;
            while (!validBaseFareDistance)
            {
                Console.Write("Enter Base Fare Distance: ");
                string baseFareDistanceInput = Console.ReadLine();
                if (double.TryParse(baseFareDistanceInput, out double baseFareDistanceOutput))
                {
                    driver.BaseFareDistance = baseFareDistanceOutput;
                    validBaseFareDistance = true;
                }
                else
                {
                    Console.WriteLine("Invalid type. Please try again.");
                }
            }
            return driver;
        }
    }
}
