using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Models;

namespace TravelCompany.FareCaculator
{
    public interface IFareCalculator
    {
        bool CalculateFares(string filePath, List<Driver> drivers);
        Dictionary<Guid, InfoFare> GetCheapestPrice();
    }
}
