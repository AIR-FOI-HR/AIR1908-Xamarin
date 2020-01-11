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
using CheeseBind;

namespace DiscountLocator19
{
    [Activity(Label = "DiscountDetailsActivity")]
    public class DiscountDetailsActivity : Activity
    {
        [BindView(Resource.Id.discount_details_name)]
        TextView txtName;

        [BindView(Resource.Id.discount_details_description)]
        TextView txtDescription;

        [BindView(Resource.Id.discount_details_start)]
        TextView txtStartDate;

        [BindView(Resource.Id.discount_details_end)]
        TextView txtEndDate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_discount_details);

            Cheeseknife.Bind(this);
        }


        protected override void OnStart()
        {
            base.OnStart();

            int discountId = Intent.Extras.GetInt("discountId");
            var selectedDiscount = database.Database.DatabasePath.GetDiscountById(discountId).Result;

            txtName.Text = selectedDiscount[0].Name;
            txtDescription.Text = selectedDiscount[0].Description;
            txtStartDate.Text = selectedDiscount[0].startDate.ToString("dd-MM-yyyy");
            txtEndDate.Text = "--    " + selectedDiscount[0].endDate.ToString("dd-MM-yyyy");

        }
    }
}