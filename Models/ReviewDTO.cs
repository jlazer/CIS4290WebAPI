using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }
        public int ProductID { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string UserReview { get; set; }
    }
}
