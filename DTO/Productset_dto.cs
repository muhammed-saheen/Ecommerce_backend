﻿using Ecommerce_app.Models;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_app.DTO
{
    public class Productset_dto
    {
        public Guid Id { get; set; }


        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int quantity { get; set; }

        public string Category_name { get; set; }

    }
}