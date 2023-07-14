using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Models;
using TravelCompany.Repositories;

namespace TravelCompany.FareCaculator
{
    public class FareCalculator : IFareCalculator
    {
        private Dictionary<Guid, InfoFare> _driverCheapestPrice;

        public FareCalculator()
        {
            _driverCheapestPrice = new Dictionary<Guid, InfoFare>();
        }
        public bool CalculateFares(string filePath, List<Driver> drivers)
        {
            try
            {
                Dictionary<Guid, InfoFare> driverFares = new Dictionary<Guid, InfoFare>();

                string[] fareData = File.ReadAllLines(filePath);
                foreach (string data in fareData)
                {
                    if (fareData[0] == data)
                    {
                        continue;
                    }
                    string[] fareInfo = data.Split(',');

                    double distanceTraveled = double.Parse(fareInfo[0]);
                    double traveledUnit = double.Parse(fareInfo[1]);
                    double costPerDistance = double.Parse(fareInfo[2]);

                    foreach (Driver driver in drivers)
                    {
                        InfoFare infoFare = new InfoFare();
                        double distanceTraveledUnits = distanceTraveled - driver.BaseFareDistance;
                        if (distanceTraveledUnits >= 0)
                        {
                            infoFare.Fare = driver.BaseFarePrice + (distanceTraveledUnits / traveledUnit * costPerDistance);
                            infoFare.CoverTotalTravel = false;
                        }
                        else
                        {
                            infoFare.Fare = driver.BaseFarePrice;
                            infoFare.CoverTotalTravel = true;
                        }
                        if (!driverFares.ContainsKey(driver.Id))
                        {
                            driverFares.Add(driver.Id, infoFare);
                        }
                        else if (infoFare.Fare < driverFares[driver.Id].Fare)
                        {
                            driverFares[driver.Id].Fare = infoFare.Fare;
                        }
                    }
                }
                if (driverFares.Count() == 0) return true;
                // Find the driver with the cheapest fare and save
                var valueCheapest = driverFares.OrderBy(x => x.Value.Fare).FirstOrDefault().Value.Fare;
                var driverIdWithCheapestFareAll = driverFares.Where(s => s.Value.Fare == valueCheapest);
                var driverNotCoverTravel = driverIdWithCheapestFareAll.Where(s => s.Value.CoverTotalTravel == false);
                if (driverNotCoverTravel.Count() == 0)
                {
                    foreach (var driver in driverIdWithCheapestFareAll)
                    {
                        _driverCheapestPrice.Add(driver.Key, driver.Value);
                    }
                }
                else
                {
                    foreach (var driver in driverNotCoverTravel)
                    {
                        _driverCheapestPrice.Add(driver.Key, driver.Value);
                    }
                }
                return true;
            }
            catch
            { 
                return false;
            }
        }
        public Dictionary<Guid, InfoFare> GetCheapestPrice()
        {
            return _driverCheapestPrice;
        }

    }
}
