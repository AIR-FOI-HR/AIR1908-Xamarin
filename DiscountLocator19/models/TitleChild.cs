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

namespace DiscountLocator19.models
{
    public class TitleChild
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }

        public TitleChild(int id, string name, string description, int value)
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
        }
    }
}