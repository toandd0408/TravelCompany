using ConsoleTables;
using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using TravelCompany.FareCaculator;
using TravelCompany.Models;
using TravelCompany.Repositories;

namespace TravelCompany
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IRepository<Driver> driverManager = new Repository<Driver>();
            IFareCalculator calculator = new FareCalculator();
            Driver driver1 = new Driver
            {
                Name = "Tu",
                Surname = "Nguyen",
                Email = "nguyentu@gmail.com",
                VehicleType = "Car",
                BaseFarePrice = 200,
                BaseFareDistance = 150
            };

            Driver driver2 = new Driver
            {
                Name = "Tuan",
                Surname = "Le",
                Email = "letuan@gmail.com",
                VehicleType = "Bus",
                BaseFarePrice = 300,
                BaseFareDistance = 250
            };
            driverManager.Register(driver1);
            driverManager.Register(driver2);


            int option;
            do
            {
                Console.WriteLine("***************MAIN MENU**********");
                Console.WriteLine("1.   Get All Driver");
                Console.WriteLine("2.   Resignter");
                Console.WriteLine("3.   Delete");
                Console.WriteLine("4.   Update");
                Console.WriteLine("5.   Calculator and Save the driver with the cheapest fare");
                Console.WriteLine("6.   Get the driver with the cheapest fare");
                Console.WriteLine("7.   Exit");
                Console.Write("Enter the number:");

                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        #region Get all driver
                        var lsDriver = driverManager.GetAll();
                        var table = new ConsoleTable("Id", "Name", "Surname", "Email", "Vehicle Type", "Base Fare Price", "Base Fare Distance");

                        foreach (Driver driver in lsDriver)
                        {
                            table.AddRow(driver.Id, driver.Name, driver.Surname, driver.Email, driver.VehicleType, driver.BaseFarePrice, driver.BaseFareDistance);
                        }

                        Console.WriteLine(table);
                        Console.WriteLine();
                        #endregion
                        break;
                    case 2:
                        #region Resignter
                        Driver newDriver = ConsoleView.ConsoleView.EnterDriver();
                        if (driverManager.Register(newDriver))
                        {
                            Console.WriteLine("Resign Success");
                        }
                        else
                        {
                            Console.WriteLine("Resign Success fail");
                        }
                        Console.WriteLine();
                        #endregion
                        break;
                    case 3:
                        #region Delete
                        Console.Write("Enter Id to delete: ");
                        if (Guid.TryParse(Console.ReadLine(), out Guid driverId))
                        {
                            if(driverManager.DeleteById(driverId))
                            {
                                Console.WriteLine("Driver deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Driver deleted fail.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Id format. Driver not deleted.");
                        }
                        Console.WriteLine();
                        #endregion
                        break;
                    case 4:
                        #region Update
                        Console.Write("Enter Id to Update: ");
                        if (Guid.TryParse(Console.ReadLine(), out Guid driverIdUpdate))
                        {
                            Driver driverToUpdate = driverManager.GetAll().Find(d => d.Id == driverIdUpdate);
                            if (driverToUpdate != null)
                            {
                                Driver updateDriver = ConsoleView.ConsoleView.EnterDriver();
                                updateDriver.Id = driverIdUpdate;
                                if (driverManager.UpdateDriver(updateDriver))
                                {
                                    Console.WriteLine("Update OK");
                                }
                                else
                                {
                                    Console.WriteLine("Update Fail");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Driver not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Id format.");
                        }
                        Console.WriteLine();
                        #endregion
                        break;
                    case 5:
                        #region Calculator and Save the driver with the cheapest fare
                        if (calculator.CalculateFares(string.Format(ConfigurationManager.AppSettings["CSV_File"] ?? ""), driverManager.GetAll()))
                        {
                            Console.WriteLine("Save OKE");
                        }
                        else
                        {
                            Console.WriteLine("Fail");
                        }
                        Console.WriteLine();
                        #endregion
                        break;
                    case 6:
                        #region Get the driver with the cheapest fare
                        var cheapestPrice = calculator.GetCheapestPrice();                       
                        if (cheapestPrice.Count() > 0)
                        {
                            foreach(var dic in cheapestPrice)
                            {
                                var driver = driverManager.GetAll().Where(s => s.Id == dic.Key).FirstOrDefault();
                                Console.WriteLine($"Driver's name: {driver.Name}, fare: {dic.Value.Fare}");

                            }
                        }
                        else
                        {
                            Console.WriteLine($"No driver");
                        }
                        Console.WriteLine();
                        #endregion
                        break;
                    default:
                        break;
                }

            } while (option != 7);
        }
    }
}