using E_commerce_2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_commerce_2.Models

{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri ImageURL { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }

        // Navigation Proparites
        public List<CategoriesProduct>? categoriesProducts { get; set; }
        public List<CartProduct>? cartProducts { get; set; }
        public List<OrderProduct>? orderProduct { get; set; }
    }
}