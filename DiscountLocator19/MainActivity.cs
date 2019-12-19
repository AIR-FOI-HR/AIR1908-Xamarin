using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using database.Entities;
using System.Collections.Generic;
using database;

namespace DiscountLocator19
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            mockData();
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