using database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace database
{
    public class MockData
    {
        public static void writeAll()
        {
           
            Store store = new Store();
            store.Name = "Ducan1";
            store.ID = Database.DatabasePath.InsertStores(store).Result;

            Discount discount = new Discount();
            discount.Name = "Popust 1";
            discount.Description = "Badjune ovo cu ti zapamtit";
            discount.storeId = store.ID;

            Database.DatabasePath.InsertDiscounts(discount);


            Discount discount2 = new Discount();
            discount2.Name = "Popust 2";
            discount2.Description = "Badjune ovo cu ti necu zaboravit";
            discount2.storeId = store.ID;

            Database.DatabasePath.InsertDiscounts(discount2);
        }
    }
}
