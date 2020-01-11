using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace database.Entities
{
    public class Discount
    {
        [PrimaryKey]
        public int ID { get; set; }

        [ForeignKey(typeof(Store))]
        public int storeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int discount { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
    }
}
