﻿using System;

namespace YetAnotherECommerce.Modules.Products.Core.DAL.Mongo.Documents
{
    public class ProductDocument
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}