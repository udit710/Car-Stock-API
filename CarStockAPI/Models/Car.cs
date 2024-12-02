using System.ComponentModel.DataAnnotations;

namespace CarStockAPI.Models{
    public class Car{
        public int CarId { get; set; }

        [Required]
        public int DealerId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(0, int.MaxValue)]
        public int StockCount { get; set; }
    }
}
