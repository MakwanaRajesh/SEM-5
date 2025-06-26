using Reastaurant.Models;

namespace Reastaurant.Models.ViewModels
{
    public class MenuViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<MenuItem> FeaturedItems { get; set; } = new List<MenuItem>();
        public List<MenuItem> PopularItems { get; set; } = new List<MenuItem>();
        public string SelectedCuisine { get; set; }
        public string SearchTerm { get; set; }
        public bool ShowVegetarianOnly { get; set; }
        public bool ShowVeganOnly { get; set; }
        public string PriceRange { get; set; }
        public string SpiceLevel { get; set; }
    }

    public class MenuCategoryViewModel
    {
        public Category Category { get; set; }
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SortBy { get; set; }
        public string FilterBy { get; set; }
    }

    public class MenuItemDetailViewModel
    {
        public MenuItem MenuItem { get; set; }
        public List<MenuItem> RelatedItems { get; set; } = new List<MenuItem>();
        public List<MenuItem> SimilarItems { get; set; } = new List<MenuItem>();
    }
}
