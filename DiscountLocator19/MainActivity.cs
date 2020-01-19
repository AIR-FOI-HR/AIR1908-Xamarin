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
using DiscountLocator19.loaders;
using Android.Support.V7.Widget;
using DiscountLocator19.adapter;
using XamDroid.ExpandableRecyclerView;
using DiscountLocator19.models;
using System.Threading;
using Android.Content;
using DiscountLocator19.helpers;
using Android.Preferences;
using DiscountLocator19.fragments;

namespace DiscountLocator19
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ISharedPreferencesOnSharedPreferenceChangeListener
    {
       
        private Util util = new Util();    

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            CurrentActivity.setActivity(this);
            SetContentView(Resource.Layout.activity_main);

            util.setLanguage(this);
            PreferenceManager.GetDefaultSharedPreferences(this).RegisterOnSharedPreferenceChangeListener(this);

            showMainFragment();

        }

        [Obsolete]
        private void showMainFragment()
        {
            FragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment, new ListViewFragment()).Commit();
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