using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonePOS.Entities
{
    class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(8)]
        public string ProductDescription { get; set; }
    }
}
