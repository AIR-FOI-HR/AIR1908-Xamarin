using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamDroid.ExpandableRecyclerView;

namespace DiscountLocator19.viewHolder
{
    public class TitleChildViewHolder : ChildViewHolder
    {
        public TextView name, description, value, id;

        public TitleChildViewHolder(View itemView) : base(itemView)
        {
            name = itemView.FindViewById<TextView>(Resource.Id.discount_name);
            description = itemView.FindViewById<TextView>(Resource.Id.discount_desc);
            value = itemView.FindViewById<TextView>(Resource.Id.discount_value);
            id = itemView.FindViewById<TextView>(Resource.Id.discount_id);

            itemView.Click += delegate
            {
                var intent = new Intent(Application.Context, typeof(DiscountDetailsActivity));
                intent.PutExtra("discountId", Int32.Parse(id.Text));
                Application.Context.StartActivity(intent);
            };

        }
    }
}