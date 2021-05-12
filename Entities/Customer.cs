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
    public class Customer
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public int Admin { get; set; }
    }
}
