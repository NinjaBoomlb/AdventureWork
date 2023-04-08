using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWork.Models
{
    [Table("Product", Schema = "Production")]
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProductNumber { get; set; }
        [Required]
        public bool MakeFlag { get; set; }
        [Required]
        public bool FinishedGoodsFlag { get; set; }
        public string? Color { get; set; }
        [Required]
        public Int16 SafetyStockLevel { get; set; }
        [Required]
        public Int16 ReorderPoint { get; set; }
        [Required]
        public decimal StandardCost { get; set; }
        [Required]
        public decimal ListPrice { get; set; }
        public string? Size { get; set; }
        public string? SizeUnitMeasureCode { get; set; }
        public string? WeightUnitMeasureCode { get; set; }
        public decimal? Weight { get; set; }
        [Required]
        public int DaysToManufacture { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }
        public int? ProductSubcategoryID { get; set; }
        public int? ProductModelID { get; set; }
        [Required]
        public DateTime SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }

    }
}
