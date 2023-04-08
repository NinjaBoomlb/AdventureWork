using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWork.Models
{
    [Table("ProductInventory", Schema = "Production")]
    public class ProductInventory
    {
        public int ProductID { get; set; }
        public int LocationID { get; set; }
        public string Shelf { get; set; }
        public int Bin { get; set; }
        public int Quantity { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
