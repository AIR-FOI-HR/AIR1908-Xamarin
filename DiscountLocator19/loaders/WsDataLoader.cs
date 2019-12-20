using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using core;
using database.Entities;
using webservice;

namespace DiscountLocator19.loaders
{
    public class WsDataLoader : DataLoader
    {
        public override void loadData(DataLoadedListener dataLoadedListener)
        {
            base.loadData(dataLoadedListener);

            MyWebServiceCaller storesWs = new MyWebServiceCaller(storesHandler, this);
            MyWebServiceCaller discountsWs = new MyWebServiceCaller(discountsHandler, this);
            Dictionary<string, string> method = new Dictionary<string, string>();
            method.Add("method", "getAll");
            storesWs.getAll(method, typeof(Store));
            discountsWs.getAll(method, typeof(Discount));
            
        }

        MyWebServiceHandler storesHandler = MyWebServiceHandlerFactory.GetHandler<Store>();

        MyWebServiceHandler discountsHandler = MyWebServiceHandlerFactory.GetHandler<Discount>();

        public void checkDataArrival()
        {
            if (storesHandler.hasDataArrived() && discountsHandler.hasDataArrived())
            {
                mDataLoadedListener.onDataLoaded(database.Database.DatabasePath.GetStores().Result, database.Database.DatabasePath.GetDiscounts().Result);
            }
        }

        public override void DataArrived()
        {
            checkDataArrival();
        }

    }
}