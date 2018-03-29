using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMaps.Dtos
{
    /// <summary>
    /// Simple object to send o Google Maps api
    /// </summary>
    public class GoogleMapPointDto
    {
        public string Id { get; set; }
        public string PlaceName { get; set; }
        public string GeoLong { get; set; }
        public string GeoLat { get; set; }
    }
}