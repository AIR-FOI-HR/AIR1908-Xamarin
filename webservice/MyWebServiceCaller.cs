using database.Entities;
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
            var callDiscount = await _api.getDiscounts(method);

            if (callStores != null)
            {
                if (entityType == typeof(Store))
                {
                    Console.WriteLine("Got stores!");
                }
                else if (entityType == typeof(Discount))
                {
                    Console.WriteLine("Got discounts!");
                }
                else
                {
                    Console.WriteLine("Unrecognized class.");
                }
            }
        }
    }
}
