using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Preferences;
using Android.Views;
using Android.Widget;
using DiscountLocator19.helpers;

namespace DiscountLocator19
{
    [Activity(Label = "@string/title_activity_settings")]
    public class SettingsActivity : AppCompatActivity, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        private Util util = new Util();

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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.settings_activity);

            util.setLanguage(this);
            PreferenceManager.GetDefaultSharedPreferences(this).RegisterOnSharedPreferenceChangeListener(this);

            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.settings, new SettingsFragment()).Commit();

            SupportActionBar.SetTitle(Resource.String.title_activity_settings);
            var actionBar = SupportActionBar;
            if (actionBar != null)
            {
                actionBar.SetDefaultDisplayHomeAsUpEnabled(true);
            }
        }

        public class SettingsFragment : PreferenceFragmentCompat
        {
            public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
            {
                SetPreferencesFromResource(Resource.Xml.root_preferences, rootKey);
            }
        }

    }
}