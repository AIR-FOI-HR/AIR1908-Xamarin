using core;
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
        MyWebServiceHandler mMyWebServiceHandler;
        private IDataArrived dataArrived;
        public MyWebServiceCaller()
        {

        }

        public MyWebServiceCaller(MyWebServiceHandler myWebServiceHandler, IDataArrived dataArrived)
        {
            this.mMyWebServiceHandler = myWebServiceHandler;
            this.dataArrived = dataArrived;
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

        private void handleStores(MyWebServiceResponse responseStores)
        {
            Store[] storeItems = JsonConvert.DeserializeObject<Store[]>(responseStores.Items);
            if (mMyWebServiceHandler != null)
            {
                mMyWebServiceHandler.onDataArrived(new List<Store>(storeItems), true, responseStores.TimeStamp);
                dataArrived.DataArrived();
            }
        }

        private void handleDiscounts(MyWebServiceResponse responseDiscounts)
        {
            Discount[] discountItems = JsonConvert.DeserializeObject<Discount[]>(responseDiscounts.Items, new JsonSerializerSettings 
            { 
                DateFormatString = "yyyy-MM-dd"
            });

            if (mMyWebServiceHandler != null)
            {
                mMyWebServiceHandler.onDataArrived(new List<Discount>(discountItems), true, responseDiscounts.TimeStamp);
                dataArrived.DataArrived();
            }
        }
    }
}
