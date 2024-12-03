using System.ComponentModel.DataAnnotations;

namespace CarStockAPI.Models{
    /**
     * Car model: Represents a car in the database
     * Attributes:
        *  - CarId: The unique identifier of the car
        *  - DealerId: The unique identifier of the dealer that owns the car
        *  - Make: The make of the car
        *  - Model: The model of the car
        *  - Year: The year the car was made
        *  - StockCount: The number of cars in stock
     */
    public class Car{
        
        [Key]
        public int CarId { get; set; }

        [Required]
        public int DealerId { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
        
        [Required]
        [Range(1900, 2024)]
        public int Year { get; set; }

        [Range(0, int.MaxValue)]
        public int StockCount { get; set; }
    }
}
