using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using core;
using DiscountLocator19.fragments;
using maps;

namespace DiscountLocator19.managers
{
    public class DataPresenterManager : AppCompatActivity
    {
        [Obsolete]
        private static DataPresenterManager ourInstance = new DataPresenterManager();

        public List<DataPresenter> modules { get; set; } = null;

        private DrawerLayout drawerLayout;
        private AppCompatActivity activity;
        private NavigationView navigationView;
        private int dynamicGroupId;

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

        internal void setDrawerDependencies(AppCompatActivity activity, NavigationView navigationView, DrawerLayout drawerLayout, int dynamicGroupId)
        {
            this.activity = activity;
            this.navigationView = navigationView;
            this.drawerLayout = drawerLayout;
            this.dynamicGroupId = dynamicGroupId;

            setupDrawerMenu();
        }

        private void setupDrawerMenu()
        {
            for (int i = 0; i < modules.Count; i++)
            {
                DataPresenter module = modules.ElementAt(i);
                navigationView.Menu.Add(dynamicGroupId, i, i + 1, module.getName(activity)).SetIcon(module.getIcon(activity)).SetCheckable(true);
            }
        }

        [Obsolete]
        internal void startMainModule()
        {
            DataPresenter mainModule = modules != null ? modules.ElementAt(0) : null;
            if (mainModule != null)
            {
                startModule(mainModule);
            }
        }

        [Obsolete]
        private void startModule(DataPresenter module)
        {
            FragmentManager fragmentManager = activity.FragmentManager;

            if (!activity.IsFinishing && !activity.IsDestroyed)
            {
                fragmentManager.BeginTransaction().Replace(Resource.Id.main_fragment, module.getFragment()).SetTransition(FragmentTransit.FragmentOpen).Commit();
            }

            DataLoaderManager.getInstance().sendData(module);
        }
    }
}