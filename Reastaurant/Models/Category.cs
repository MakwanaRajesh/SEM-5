using System.ComponentModel.DataAnnotations;

namespace Reastaurant.Models
{
    public class Category
    {
        //public int CategoryID { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string CategoryName { get; set; }

        //[StringLength(500)]
        //public string Description { get; set; }

        //public string ImageURL { get; set; }

        //public bool IsActive { get; set; } = true;

        //public DateTime CreatedDate { get; set; } = DateTime.Now;

        //public string CuisineType { get; set; }

        //public int SortOrder { get; set; }

        //public int TotalPages { get; set; }

        //public int CurrentPage { get; set; }

        //public int TotalItems { get; set; }

        //// Navigation property
        //public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public string RestaurantName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Rating { get; set; }
        public string DeliveryTime { get; set; } // e.g. "20-25 mins"
        public string FoodTypes { get; set; } // e.g. "Pizzas, Chinese, Thalis"
        public string Location { get; set; }
        public string OfferText { get; set; } // e.g. "₹99 ITEMS" or "65% OFF"
    }
}
