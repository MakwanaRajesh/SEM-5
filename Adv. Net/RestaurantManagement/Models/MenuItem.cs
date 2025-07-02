using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }

}
