using System;
using System.Collections.Generic;
using System.Text;

namespace seleniumProjectt.Model.Data
{
    public class Product
    {
        public Guid Id { get; set; }
        public int ListingId { get; set; }
        public string Title{ get; set; }
        public string Url { get; set; }
        public Guid SellerId { get; set; }
    }
}
