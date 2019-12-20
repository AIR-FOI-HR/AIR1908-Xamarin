using database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace core
{
    public interface DataLoadedListener
    {
        void onDataLoaded(List<Store> stores, List<Discount> discounts);
    }
}
