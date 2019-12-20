using database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace webservice
{
    public class DiscountsHandler : MyWebServiceHandler
    {
        public bool discountsArrived = false;
        List<Discount> popusti = new List<Discount>();
        List<Object> objekti = new List<Object>();

        public bool hasDataArrived()
        {
            return discountsArrived;
        }

        public List<Object> haveStoresArrived()
        {
            throw new NotImplementedException();
        }

        public List<Object> haveDiscountsArrived()
        {
            foreach (Discount item in popusti)
            {
                objekti.Add((Object)item);
            }
            return objekti;
        }

        public void onDataArrived(Object result, bool ok, long timestamp)
        {
            if (ok)
            {
                List<Discount> discounts = (List<Discount>)result;
                foreach (var discount in discounts)
                {
                    database.Database.DatabasePath.InsertDiscounts(discount);
                }
                discountsArrived = true;
                popusti = discounts;
            }
        }
    }
}
