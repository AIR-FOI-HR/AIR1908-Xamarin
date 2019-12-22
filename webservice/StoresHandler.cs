using database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace webservice
{
    class StoresHandler : MyWebServiceHandler
    {
        public bool storesArrived = false;
        List<Store> ducani = new List<Store>();
        List<Object> objekti = new List<Object>();

        public bool hasDataArrived()
        {
            return storesArrived;
        }

        public List<Object> haveStoresArrived()
        {
            foreach (Store item in ducani)
            {
                objekti.Add((Object) item);
            }
            return objekti;
        }

        public List<Object> haveDiscountsArrived()
        {
            throw new NotImplementedException();
        }

        public void onDataArrived(Object result, bool ok, long timestamp)
        {
            if (ok)
            {
                List<Store> stores = (List<Store>)result;

                storesArrived = true;
                ducani = stores;
            }
        }



    }
}
