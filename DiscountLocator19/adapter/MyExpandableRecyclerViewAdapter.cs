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
using DiscountLocator19.models;
using DiscountLocator19.viewHolder;
using XamDroid.ExpandableRecyclerView;

using Square.Picasso;

namespace DiscountLocator19.adapter
{
    public class MyExpandableRecyclerViewAdapter : ExpandableRecyclerAdapter<TitleParentViewHolder, TitleChildViewHolder>
    {
        LayoutInflater _inflater;

        public MyExpandableRecyclerViewAdapter(Context context, List<IParentObject> itemList) : base(context, itemList)
        {
            _inflater = LayoutInflater.From(context);
        }

        public override void OnBindChildViewHolder(TitleChildViewHolder childViewHolder, int position, object childObject)
        {
            var title = (TitleChild)childObject;
            childViewHolder.name.Text = title.Name;
            childViewHolder.description.Text = title.Description;
            childViewHolder.value.Text = title.Value.ToString() + "%";
            childViewHolder.id.Text = title.Id.ToString();
        }

        public override void OnBindParentViewHolder(TitleParentViewHolder parentViewHolder, int position, object parentObject)
        {
            var title = (TitleParent)parentObject;
            parentViewHolder._textViewName.Text = title.Title;
            parentViewHolder._textViewDesc.Text = title.Description;
            Picasso.With(this._context).Load(title.ImgUrl).Into(parentViewHolder._imageViewImg);
        }

        public override TitleChildViewHolder OnCreateChildViewHolder(ViewGroup childViewGroup)
        {
            var view = _inflater.Inflate(Resource.Layout.discount_list_item, childViewGroup, false);
            return new TitleChildViewHolder(view);
        }

        public override TitleParentViewHolder OnCreateParentViewHolder(ViewGroup parentViewGroup)
        {
            var view = _inflater.Inflate(Resource.Layout.store_list_item, parentViewGroup, false);
            return new TitleParentViewHolder(view);
        }
    }
}