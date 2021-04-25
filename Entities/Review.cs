using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI.Entities
{
    public class Review
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
