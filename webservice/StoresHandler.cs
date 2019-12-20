using database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace webservice
{
    class StoresHandler : MyWebServiceHandler
    {
        public bool storesArrived = false;


        public void onDataArrived(Object result, bool ok, long timestamp)
        {
            if (ok)
            {
                List<Store> stores = (List<Store>)result;
                foreach (var store in stores)
                {
                    database.Database.DatabasePath.InsertStores(store);
                }
                storesArrived = true;

            }
        }



    }
}
