﻿namespace E_commerce_2.Models.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public int Count { get; set; }
    }
}
