using Android.Support.V7.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace core
{
    public class CurrentActivity
    {
        private static AppCompatActivity activity;

        public static void setActivity(AppCompatActivity activity)
        {
            CurrentActivity.activity = activity;
        }

        public static AppCompatActivity getActivity()
        {
            return activity;
        }
    }
}
