using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reastaurant.Models
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string ItemName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public string ImageURL { get; set; }

        public bool IsAvailable { get; set; } = true;

        public bool IsVegetarian { get; set; }

        public bool IsVegan { get; set; }

        public bool IsSpicy { get; set; }

        public string SpiceLevel { get; set; } // Mild, Medium, Hot, Extra Hot

        public int PreparationTime { get; set; } // in minutes

        public string Ingredients { get; set; }

        public string Allergens { get; set; }

        public int Calories { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int SortOrder { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsPopular { get; set; }

        // Navigation property
        public virtual Category Category { get; set; }
    }
}
