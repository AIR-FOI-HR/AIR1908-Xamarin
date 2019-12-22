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
    public class TitleParentViewHolder : ParentViewHolder
    {
        public TextView _textViewName;
        public TextView _textViewDesc;
        public ImageView _imageViewImg;
        public TitleParentViewHolder(View itemView) : base(itemView)
        {
            _textViewName = itemView.FindViewById<TextView>(Resource.Id.store_name);
            _textViewDesc = itemView.FindViewById<TextView>(Resource.Id.store_desc);
            _imageViewImg = itemView.FindViewById<ImageView>(Resource.Id.store_image);
        }
    }
}