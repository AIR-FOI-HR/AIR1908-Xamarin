﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Linq;
using database.Entities;
using System.Collections.Generic;
using database;
using Android.Views;
using Android.Support.V7.RecyclerView.Extensions;
using core;
using DiscountLocator19.loaders;
using Android.Support.V7.Widget;
using DiscountLocator19.adapter;
using XamDroid.ExpandableRecyclerView;
using DiscountLocator19.models;
using System.Threading;
using Android.Content;
using DiscountLocator19.helpers;
using Android.Preferences;

namespace DiscountLocator19
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, DataLoadedListener, ISharedPreferencesOnSharedPreferenceChangeListener
    {

        RecyclerView myRecyclerView;
        private Util util = new Util();

        public void onDataLoaded(List<Store> stores, List<Discount> discounts)
        {
            myRecyclerView = FindViewById<RecyclerView>(Resource.Id.main_recycler);
            myRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            var adapter = new MyExpandableRecyclerViewAdapter(this, InitData(stores, discounts));
            adapter.SetParentClickableViewAnimationDefaultDuration();
            adapter.ParentAndIconExpandOnClick = true;

            myRecyclerView.SetAdapter(adapter);
        }

        private List<IParentObject> InitData(List<Store> stores, List<Discount> discounts)
        {
            var titleCreator = TitleCreator.Get(stores);
            var titles = titleCreator.GetAll;
            var parentObject = new List<IParentObject>();
            int counter = 0;

            foreach (var title in titles)
            {

                var childList = new List<Object>();
                var store = stores[counter];
                List<Discount> discountsByStoreID = GetDiscountsByStoreID(store, discounts);

                foreach (var discount in discountsByStoreID)
                {
                    childList.Add(new TitleChild(discount.ID, discount.Name, discount.Description, discount.discount));
                }

                title.ChildObjectList = childList;
                parentObject.Add(title);
                counter++;
            }
            return parentObject;
        }

        private List<Discount> GetDiscountsByStoreID(Store store, List<Discount> discounts)
        {
            var discountsByStoreID = new List<Discount>();

            foreach (var discount in discounts)
            {
                if (store.ID == discount.storeId)
                {
                    discountsByStoreID.Add(discount);
                }
            }
            return discountsByStoreID;
        }

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            util.setLanguage(this);
            PreferenceManager.GetDefaultSharedPreferences(this).RegisterOnSharedPreferenceChangeListener(this);

            if (Database.DatabasePath.GetStores().Result.Count == 0)
            {
                Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(context: this);
                alertDialog.SetTitle("DB is empty. Data will be retrieved from a WS.");

                alertDialog.SetNeutralButton("OK", delegate
                {
                    DataLoader dataLoader = new WsDataLoader();
                    dataLoader.loadData(this);
                });

                alertDialog.Show();
            }
            else 
            {
                DataLoader dataLoader = new DbDataLoader();
                dataLoader.loadData(this);
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.nekepreference:
                    var intent = new Intent(Application.Context, typeof(SettingsActivity));
                    Application.Context.StartActivity(intent);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        [Obsolete]
        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            switch (key)
            {
                case "language":
                    Util util = new Util();
                    util.setLanguage(this);
                    Recreate();
                    break;
            }
        }
    }
}