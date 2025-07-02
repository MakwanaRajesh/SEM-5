namespace RestaurantManagement.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int MenuItemId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; }

        public MenuItem MenuItem { get; set; }

        public decimal TotalPrice => Quantity * MenuItem?.Price ?? 0;
    }

}
