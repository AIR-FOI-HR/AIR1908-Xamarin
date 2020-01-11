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

namespace DiscountLocator19
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.settings_activity);

            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.settings, new SettingsFragment()).Commit();

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
                throw new NotImplementedException();
            }
        }

    }
}