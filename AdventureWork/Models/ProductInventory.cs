using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWork.Models
{
    [Table("ProductInventory", Schema = "Production")]
    public class ProductInventory
    {
        public int ProductID { get; set; }
        public Int16 LocationID { get; set; }
        public string Shelf { get; set; }
        public Byte Bin { get; set; }
        public Int16 Quantity { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
