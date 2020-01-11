using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace database.Entities
{
    public class Store
    {
        [PrimaryKey]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public long Latitude { get; set; }

        public long Longitude { get; set; }
    }
}
