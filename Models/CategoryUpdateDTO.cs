﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class CategoryUpdateDTO
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Parent { get; set; }
    }
}
