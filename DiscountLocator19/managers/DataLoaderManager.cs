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
using database;
using database.Entities;
using DiscountLocator19.loaders;

namespace DiscountLocator19.managers
{
    public class DataLoaderManager : DataLoadedListener
    {

        public DataPresenter myDataPresenter { get; set; } = null;

        private static DataLoaderManager ourInstance = new DataLoaderManager();

        public static DataLoaderManager getInstance()
        {
            return ourInstance;
        }

        private DataLoaderManager()
        {
        }

        public void sendData(DataPresenter module)
        {
            DataLoader dataLoader = null;
            myDataPresenter = module;

            if (Database.DatabasePath.GetStores().Result.Count == 0)
            {
                dataLoader = new WsDataLoader();
            }
            else
            {
                dataLoader = new DbDataLoader();
            }

            dataLoader.loadData(this);

        }

        public void onDataLoaded(List<Store> stores, List<Discount> discounts)
        {
            myDataPresenter.setData(stores, discounts);
        }
    }
}