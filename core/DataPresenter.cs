using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace core
{
    public interface DataPresenter
    {
        Drawable getIcon(Context context);
        String getName(Context context);
        [Obsolete]
        Fragment getFragment();
        void setData(List<Store> stores, List<Discount> discounts);
    }
}
