using database.Entities;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace webservice
{
    public class MyWebServiceCaller
    {
        private const String baseUrl = "http://cortex.foi.hr/mtl/courses/air/";

        private static MyWebService _api;

        public MyWebServiceCaller()
        {

        }

        public async void getAll(Dictionary<String, String> method, Type entityType)
        {
            _api = RestService.For<MyWebService>(baseUrl);

            var callStores = await _api.getStores(method);
            var callDiscounts = await _api.getDiscounts(method);

            if (callStores != null)
            {
                if (entityType == typeof(Store))
                {
                    Console.WriteLine("Got stores!");
                    handleStores(callStores);
                }
                else if (entityType == typeof(Discount))
                {
                    Console.WriteLine("Got discounts!");
                    handleDiscounts(callDiscounts);
                }
                else
                {
                    Console.WriteLine("Unrecognized class.");
                }
            }
        }

        private void handleDiscounts(MyWebServiceResponse responseStores)
        {
            Store[] storeItems = JsonConvert.DeserializeObject<Store[]>(responseStores.Items);
        }

        private void handleStores(MyWebServiceResponse responseDiscounts)
        {
            Discount[] discountItems = JsonConvert.DeserializeObject<Discount[]>(responseDiscounts.Items, new JsonSerializerSettings 
            { 
                DateFormatString = "yyyy-MM-dd"
            });
        }
    }
}
