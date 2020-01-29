using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using core;
using database.Entities;

namespace maps
{
    [Obsolete]
    public class MapModule : Fragment, IOnMapReadyCallback, DataPresenter
    {
        GoogleMap map;
        MapFragment mapFragment;

        private List<Store> stores;
        private List<Discount> discounts;

        private bool moduleReadyFlag = false;
        private bool dataReadyFlag = false;

        public Fragment getFragment()
        {
            return this;
        }

        public Drawable getIcon(Context context)
        {
            return context.GetDrawable(Android.Resource.Drawable.IcMenuMyLocation);
        }

        public string getName(Context context)
        {
            return context.GetString(Resource.String.map_view);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_map, container, false);

        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;

            moduleReadyFlag = true;
            tryToDisplayData();
        }

        private void tryToDisplayData()
        {
            if (moduleReadyFlag && dataReadyFlag)
            {
                displayData();
            }
        }

        private void displayData()
        {
            bool cameraReady = false;
            if (stores != null)
            {
                foreach (var store in stores)
                {
                    LatLng position = new LatLng(store.Latitude / 1000000.0, store.Longitude / 1000000.0);
                    map.AddMarker(new MarkerOptions().SetPosition(position).SetTitle(store.Name));

                    if (!cameraReady)
                    {
                        map.MoveCamera(CameraUpdateFactory.NewLatLng(position));
                        map.MoveCamera(CameraUpdateFactory.ZoomTo(12));
                        cameraReady = true;
                    }
                }

            }
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            mapFragment = (MapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }

        public void setData(List<Store> stores, List<Discount> discounts)
        {
            this.stores = stores;
            this.discounts = discounts;

            dataReadyFlag = true;
            tryToDisplayData();
        }
    }
}