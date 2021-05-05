using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPI.Models
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
    }
}
