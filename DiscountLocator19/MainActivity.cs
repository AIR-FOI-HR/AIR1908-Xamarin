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
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using DiscountLocator19.managers;
using Android.Gms.Ads;

namespace DiscountLocator19
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ISharedPreferencesOnSharedPreferenceChangeListener, NavigationView.IOnNavigationItemSelectedListener
    {
        private Util util = new Util();

        Android.Support.V7.Widget.Toolbar toolbar;
        DrawerLayout drawerLayout;
        ActionBarDrawerToggle drawerToggle;
        NavigationView navigationView;
        AdView adView;

        public static MainActivity Instance { get; private set; }

        public MainActivity()
        {
            Instance = this;
        }


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

            initializeLayout();
            InitializeDataPresenterManager();

        }

        [Obsolete]
        private void InitializeDataPresenterManager()
        {
            DataPresenterManager dataPresenterManager = DataPresenterManager.getInstance();
            dataPresenterManager.setDrawerDependencies(this, navigationView, drawerLayout, Resource.Id.dynamic_group);
            dataPresenterManager.startMainModule();
        }

        private void initializeLayout()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            adView = FindViewById<AdView>(Resource.Id.reklama_banner);
            var adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);

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

        [Obsolete]
        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            switch(menuItem.ItemId)
            {
                case Resource.Id.menu_about:
                    Toast.MakeText(ApplicationContext, "Discount Locator", ToastLength.Short).Show();
                    break;
                default:
                    DataPresenterManager.getInstance().selectNavigationItem(menuItem);
                    break;
            }

            drawerLayout.CloseDrawer(Android.Support.V4.View.GravityCompat.Start);
            return true;
        }
        protected override void OnResume()
        {
            base.OnResume();
            if (adView != null)
            {
                adView.Resume();
            }
        }
    }
}