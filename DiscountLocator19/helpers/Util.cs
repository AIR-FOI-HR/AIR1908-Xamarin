using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DiscountLocator19.helpers
{
    public class Util
    {
        [Obsolete]
        public void setLanguage(Context context)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

            String lang = preferences.GetString("language", "system");

            Configuration config = new Configuration(context.Resources.Configuration);
            if (lang == "system")
            {
                config.SetLocale(Java.Util.Locale.Default);
            }
            else
            {
                config.SetLocale(new Java.Util.Locale(lang));
            }

            context.Resources.UpdateConfiguration(config, context.Resources.DisplayMetrics);
        }
    }
}