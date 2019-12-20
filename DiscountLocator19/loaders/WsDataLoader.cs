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

            MyWebServiceCaller storesWs = new MyWebServiceCaller(storesHandler);
            MyWebServiceCaller discountsWs = new MyWebServiceCaller(discountsHandler);
            Dictionary<string, string> method = new Dictionary<string, string>();
            method.Add("method", "getAll");
            storesWs.getAll(method, typeof(Store));
            discountsWs.getAll(method, typeof(Store));

            MyWebServiceHandler storesHandler = null;

            MyWebServiceHandler discountsHandler = null;

            //Zasad još ne možemo pridružiti nikakvu vrijednost storesHandler i discountsHandler varijablama

        }
    }
}