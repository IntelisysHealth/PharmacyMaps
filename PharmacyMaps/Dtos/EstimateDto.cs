using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMaps.Dtos
{
    /// <summary>
    /// api/Estimate returns this dto
    /// Initial estimate to display the different total costs by pharmacy
    /// </summary>
    public class EstimateDto
    {
        public string Npi { get; set; }

        public string Ncpdp { get; set; }

        public string PharmacyName { get; set; }

        /// <summary>
        /// Total cost for all the medications
        /// </summary>
        public string TotalCost { get; set; }

        /// <summary>
        /// If All medications where found at this pharmacy
        /// </summary>
        public string AllItemsFound { get; set; }

        public string Status { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}