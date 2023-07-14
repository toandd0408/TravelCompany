using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Models
{
    public class InfoFare
    {
        /// <summary>
        /// Fare
        /// </summary>
        public double Fare { get; set; }
        /// <summary>
        /// Cover Total Travel
        /// </summary>
        /// <returns> false if the distance is not covered, true if the distance is covered</returns>
        public bool CoverTotalTravel { get; set; }
    }
}
