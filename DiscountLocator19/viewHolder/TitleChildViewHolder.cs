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
                
            };

            itemView.LongClick += delegate
            {
                AlertDialog.Builder alertDialog = new AlertDialog.Builder(itemView.Context);
                alertDialog.SetTitle("Do you wish to remove the selected item?");

                alertDialog.SetNegativeButton("No", delegate
                {
                    alertDialog.Dispose();
                });

                alertDialog.SetPositiveButton("Yes", delegate
                {
                    var selectedDiscount = database.Database.DatabasePath.GetDiscountById(Int32.Parse(id.Text)).Result;
                    database.Database.DatabasePath.DeleteDiscount(selectedDiscount[0]);

                    int discountCount = (database.Database.DatabasePath.GetAllDiscountsByStoreId(selectedDiscount[0].storeId).Result).Count;

                    if (discountCount == 0)
                    {
                        var store = database.Database.DatabasePath.GetStoreById(selectedDiscount[0].storeId).Result;
                        database.Database.DatabasePath.DeleteStore(store[0]);
                    }


                    var intent = new Intent(Application.Context, typeof(MainActivity));
                    Application.Context.StartActivity(intent);


                });

                alertDialog.Show();

            };
        }
    }
}