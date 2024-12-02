using System.ComponentModel.DataAnnotations;

namespace CarStockAPI.Models{
    public class Dealer{
        public int DealerId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
