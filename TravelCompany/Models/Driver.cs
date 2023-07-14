using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Models
{
    public class Driver : ModelBase
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Surname
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// VehicleType
        /// </summary>
        public string VehicleType { get; set; }
        /// <summary>
        /// Base Fare Price
        /// </summary>
        public double BaseFarePrice { get; set; }
        /// <summary>
        /// Base Fare Distance
        /// </summary>
        public double BaseFareDistance { get; set; }
    }
}
