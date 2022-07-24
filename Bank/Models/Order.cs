using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Bank.Models
{
    public enum Status
    {
        Process,
        Success,
        Failure
    }
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal DebitAmount { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
        public Status ResultStatus { get; set; }
    }
}