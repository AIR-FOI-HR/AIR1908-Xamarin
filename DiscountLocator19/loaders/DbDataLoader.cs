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

namespace DiscountLocator19.loaders
{
    public class DbDataLoader : DataLoader
    {
        public override void loadData(DataLoadedListener dataLoadedListener)
        {
            base.loadData(dataLoadedListener);
            try
            {
                stores = database.Database.DatabasePath.GetStores().Result;
                discounts = database.Database.DatabasePath.GetDiscounts().Result;
                mDataLoadedListener.onDataLoaded(stores, discounts);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}