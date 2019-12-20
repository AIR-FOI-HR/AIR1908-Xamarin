using database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace webservice
{
    public class DiscountsHandler : MyWebServiceHandler
    {
        public bool discountsArrived = false;

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
            }
        }
    }
}
