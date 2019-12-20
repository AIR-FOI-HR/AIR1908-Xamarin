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
                var objektiDucana = storesHandler.haveStoresArrived();
                var objektiPopusta = discountsHandler.haveDiscountsArrived();
                List<Store> ducani = new List<Store>();
                List<Discount> popusti = new List<Discount>();

                foreach (Object item in objektiDucana)
                {
                    ducani.Add((Store) item);
                }

                foreach (Object item in objektiPopusta)
                {
                    popusti.Add((Discount)item);
                }

                mDataLoadedListener.onDataLoaded(ducani, popusti);
            }
        }

        public override void DataArrived()
        {
            checkDataArrival();
        }

    }
}