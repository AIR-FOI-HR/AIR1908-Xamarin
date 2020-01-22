using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using CheeseBind;
using core;
using database;
using database.Entities;
using DiscountLocator19.adapter;
using DiscountLocator19.loaders;
using DiscountLocator19.models;
using XamDroid.ExpandableRecyclerView;

namespace DiscountLocator19.fragments
{

    [Obsolete]
    public class ListViewModule : Fragment, DataLoadedListener, DataPresenter
    {
        RecyclerView myRecyclerView;
        private List<Store> stores;
        private List<Discount> discounts;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Cheeseknife.Bind(this.Activity);

            if (Database.DatabasePath.GetStores().Result.Count == 0)
            {
                Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(context: this.Activity);
                alertDialog.SetTitle("DB is empty. Data will be retrieved from a WS.");

                alertDialog.SetNeutralButton("OK", delegate
                {
                    DataLoader dataLoader = new WsDataLoader();
                    dataLoader.loadData(this);
                });

                alertDialog.Show();
            }
            else
            {
                DataLoader dataLoader = new DbDataLoader();
                dataLoader.loadData(this);
            }
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Cheeseknife.Bind(this.Activity);
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_list_view, null);
            myRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.main_recycler);
            return view;
            
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public void onDataLoaded(List<Store> stores, List<Discount> discounts)
        
        {
            
            myRecyclerView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            var adapter = new MyExpandableRecyclerViewAdapter(this.Activity, InitData(stores, discounts));
            adapter.SetParentClickableViewAnimationDefaultDuration();
            adapter.ParentAndIconExpandOnClick = true;

            myRecyclerView.SetAdapter(adapter);
        }

        private List<IParentObject> InitData(List<Store> stores, List<Discount> discounts)
        {
            var titleCreator = TitleCreator.Get(stores);
            var titles = titleCreator.GetAll;
            var parentObject = new List<IParentObject>();
            int counter = 0;

            foreach (var title in titles)
            {

                var childList = new List<Object>();
                var store = stores[counter];
                List<Discount> discountsByStoreID = GetDiscountsByStoreID(store, discounts);

                foreach (var discount in discountsByStoreID)
                {
                    childList.Add(new TitleChild(discount.ID, discount.Name, discount.Description, discount.discount));
                }

                title.ChildObjectList = childList;
                parentObject.Add(title);
                counter++;
            }
            return parentObject;
        }

        private List<Discount> GetDiscountsByStoreID(Store store, List<Discount> discounts)
        {
            var discountsByStoreID = new List<Discount>();

            foreach (var discount in discounts)
            {
                if (store.ID == discount.storeId)
                {
                    discountsByStoreID.Add(discount);
                }
            }
            return discountsByStoreID;
        }

        public Drawable getIcon(Context context)
        {
            return context.GetDrawable(Android.Resource.Drawable.IcMenuAgenda);
        }

        public string getName(Context context)
        {
            return context.GetString(Resource.String.list_view);
        }

        public Fragment getFragment()
        {
            return this;
        }

        public void setData(List<Store> stores, List<Discount> discounts)
        {
            this.stores = stores;
            this.discounts = discounts;
        }
    }
}