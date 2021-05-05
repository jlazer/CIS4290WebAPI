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
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewID { get; set; }
        public int ProductID { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string UserReview { get; set; }
    }
}
