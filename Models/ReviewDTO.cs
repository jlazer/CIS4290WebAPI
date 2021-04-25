using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ReviewDTO
    {
        public int ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public int MainCategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public string ProductCaption { get; set; }
        public int ProductRating { get; set; }
    }
}
