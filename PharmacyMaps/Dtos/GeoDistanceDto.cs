using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyMaps.Dtos
{
    public class GeoDistanceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Icon { get; set; }
    }
}