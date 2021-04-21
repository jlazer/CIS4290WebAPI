using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal UnitPrice { get; set; }
        public int MainCategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ProductCaption { get; set; }
        public int ProductRating { get; set; }
        public int FeaturedProduct { get; set; }
        public string ProductInfo { get; set; }


    }
}
