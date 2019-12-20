using Android.App;
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

namespace DiscountLocator19
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, DataLoadedListener
    {
        Button button;
        ListView listView;

        public void onDataLoaded(List<Store> stores, List<Discount> discounts)
        { 
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            mockData();

            button = FindViewById<Button>(Resource.Id.test_button);
            listView = FindViewById<ListView>(Resource.Id.discount_list);
            button.Click += delegate{ onButtonClick(); };
        }

        private void onButtonClick()
        {
            List<Discount> discounts = Database.DatabasePath.GetDiscounts().Result;

            List<String> names = discounts.Select(d => d.Name).ToList();

            ArrayAdapter listAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, names);
            listView.Adapter = listAdapter;
        }

        private void mockData()
        {
            List<Store> stores = Database.DatabasePath.GetStores().Result;
            if (stores.Count != 0)
            {
                foreach (Store store in stores)
                {
                    Console.WriteLine("Store: " + store.Name);
                    List<Discount> discounts = Database.DatabasePath.GetDiscountByStoreId(store.ID).Result;
                    foreach (Discount discount in discounts)
                    {
                        Console.WriteLine("Discount: " + discount.Name);
                    }
                }
            }
            else
            {
                MockData.writeAll();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}