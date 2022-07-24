#nullable disable

namespace Bank.Models
{
    public class Card
    {
        public int CardId { get; set; }
        public decimal Score { get; set; }
        public ICollection<Order> Order { get; set; }

        public Card()
        {
            Order = new List<Order>();
        }
    }
}