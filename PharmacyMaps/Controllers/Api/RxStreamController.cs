using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using PharmacyMaps.Dtos;
using RestSharp;

namespace PharmacyMaps.Controllers.Api
{
    public class RxStreamController : ApiController
    {

        
        public IHttpActionResult Estimate()
        {
             List<GeoDistanceDto> pharmacyMapPoints = new List<GeoDistanceDto>();

            string url = ConfigurationSettings.AppSettings["url"].ToString();
            string tenantId = ConfigurationSettings.AppSettings["tenantId"].ToString();
            string apiKey = ConfigurationSettings.AppSettings["apiKey"].ToString();

            //get the methods you are going to call
            var client = new RestClient(url);
            var request = new RestRequest("Prescription/Estimate", Method.POST);
            
            //Add the keys to the request header, these are requred for ever call
            request.AddHeader("TenantId", tenantId);
            request.AddHeader("ApiKey", apiKey);

            //Start building the Prescripton Dto 

            //put together the header with the minimal info
            var prescriptionDto = new PrescriptionDto
            {
                ClientId = Guid.NewGuid().ToString(),
                PatZip = "80012",
                DocZip = "80012",
                PharmacyId = ""
            };

            //Create / Add a prescription line item
            prescriptionDto.LineItems.Add(new PrescriptionItemDto
            {
                PrescriptionNumber = Guid.NewGuid().ToString(), //Normally this would come from the client and you would use it to match back in the response
                Ndc = "00378362793",
                Quantity = 30
            });


            //Add the Json body
            var json = JsonConvert.SerializeObject(prescriptionDto);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            

            //Call RxStream
            var restResponse = client.Execute(request);
            
            //try to make the call and report any errors
            try
            {
                //Deserialize object
                var responseList = JsonConvert.DeserializeObject<List<EstimateDto>>(restResponse.Content);


                //convert this into a google map Dto you might want to add some of your own source data here like address and phone number
                string lowestCost = string.Empty;
                for (int i = 0; i < responseList.Count; i++)
                {
                    string icon = "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";

                    if (i == 0 || lowestCost == responseList[i].TotalCost)
                    {
                        lowestCost = responseList[i].TotalCost;
                        icon = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
                    }

                    if (i == responseList.Count - 2)
                    {
                        icon = "http://maps.google.com/mapfiles/ms/icons/red-dot.png";
                    }
                        
                    
                    pharmacyMapPoints.Add(new GeoDistanceDto
                    {
                        Id = responseList[i].Npi,
                        Name = responseList[i].PharmacyName + " $" + responseList[i].TotalCost,
                        Icon = icon,
                        Latitude = responseList[i].Latitude,
                        Longitude = responseList[i].Longitude
                    });
                }

                
            }
            catch (Exception e)
            {
                
            }
            
            return Json(pharmacyMapPoints);
        }

    }
}
