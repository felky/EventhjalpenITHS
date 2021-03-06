﻿using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class Products
    {
        public Products()
        {
            Orderdetails = new HashSet<Orderdetails>();
            RecipeDetails = new HashSet<RecipeDetails>();
        }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public decimal Price { get; set; } 
        public virtual ICollection<Orderdetails> Orderdetails { get; set; }
        public virtual ICollection<RecipeDetails> RecipeDetails { get; set; }
    }
}
