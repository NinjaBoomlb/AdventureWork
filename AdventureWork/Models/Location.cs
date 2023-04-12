using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWork.Models
{
    [Table("Location", Schema = "Production")]
    public class Location
    {
        public Int16 LocationID { get; set; }
        public string Name { get; set; }
        public decimal CostRate { get; set; }
        public decimal Availabilty { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
