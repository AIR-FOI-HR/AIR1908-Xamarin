using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using CheeseBind;

namespace DiscountLocator19.fragments
{
    public class DiscountDetailsFragment : Fragment
    {

        [BindView(Resource.Id.discount_details_name)]
        TextView txtName;

        [BindView(Resource.Id.discount_details_description)]
        TextView txtDescription;

        [BindView(Resource.Id.discount_details_start)]
        TextView txtStartDate;

        [BindView(Resource.Id.discount_details_end)]
        TextView txtEndDate;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_discount_details, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            Cheeseknife.Bind(this.Activity);
            ShowDiscountDetails();
        }

        private void ShowDiscountDetails()
        {
            base.OnStart();

            int discountId = Arguments.GetInt("discountId");
            var selectedDiscount = database.Database.DatabasePath.GetDiscountById(discountId).Result;

            txtName.Text = selectedDiscount[0].Name;
            txtDescription.Text = selectedDiscount[0].Description;
            txtStartDate.Text = selectedDiscount[0].startDate.ToString("dd-MM-yyyy");
            txtEndDate.Text = "--    " + selectedDiscount[0].endDate.ToString("dd-MM-yyyy");

        }

    }
}