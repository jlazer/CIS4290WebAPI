﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Parent { get; set; }
    }
}
