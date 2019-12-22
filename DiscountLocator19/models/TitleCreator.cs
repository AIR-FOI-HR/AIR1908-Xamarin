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
using database.Entities;

namespace DiscountLocator19.models
{
    public class TitleCreator
    {
        static TitleCreator _titleCreator;
        List<TitleParent> _titleParents;

        public TitleCreator(List<Store> stores)
        {
            _titleParents = new List<TitleParent>();

            foreach (var store in stores)
            {
                var title = new TitleParent()
                {
                    Title = store.Name,
                    Description = store.Description,
                    ImgUrl = store.ImgUrl
                };
                _titleParents.Add(title);
            }

        }

        public static TitleCreator Get(List<Store> stores)
        {
            if (_titleCreator == null)
            {
                _titleCreator = new TitleCreator(stores);
            }
            return _titleCreator;
        }

        public List<TitleParent> GetAll
        {
            get
            {
                return _titleParents;
            }
            private set
            {

            }
        }
    }
}