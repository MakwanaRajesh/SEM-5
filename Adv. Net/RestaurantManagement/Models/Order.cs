using System;
using System.Collections.Generic;
namespace RestaurantManagement.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
