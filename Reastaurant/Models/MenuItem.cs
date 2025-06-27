using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reastaurant.Models
{
    public class MenuItem
    {
        public int MenuId { get; set; }         
        public string DishName { get; set; }    
        public string Description { get; set; } 
        public decimal Price { get; set; }      
        public string ImageUrl { get; set; }    
        public string Category { get; set; }    
        public bool IsAvailable { get; set; }   
    }
}
