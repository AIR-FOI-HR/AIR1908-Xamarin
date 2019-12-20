using database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace core
{
    public abstract class DataLoader : IDataArrived
    {

        public List<Store> stores;
        public List<Discount> discounts;

        protected DataLoadedListener mDataLoadedListener;

        public virtual void loadData(DataLoadedListener dataLoadedListener)
        {
            mDataLoadedListener = dataLoadedListener;
        }

        public bool dataLoaded()
        {
            if (stores == null || discounts == null)
                return false;
            return true;
        }

        public abstract void DataArrived();

    }
}
