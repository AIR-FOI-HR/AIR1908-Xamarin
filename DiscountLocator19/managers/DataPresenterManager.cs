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
using DiscountLocator19.fragments;
using maps;

namespace DiscountLocator19.managers
{
    public class DataPresenterManager
    {
        [Obsolete]
        private static DataPresenterManager ourInstance = new DataPresenterManager();

        public List<DataPresenter> modules { get; set; } = null;

        [Obsolete]
        public static DataPresenterManager getInstance()
        {
            return ourInstance;
        }

        [Obsolete]
        private DataPresenterManager()
        {
            ListViewModule listViewModule = new ListViewModule();
            MapModule mapModule = new MapModule();
            modules = new List<DataPresenter>();

            modules.Add(listViewModule);
            modules.Add(mapModule);
        }

    }
}