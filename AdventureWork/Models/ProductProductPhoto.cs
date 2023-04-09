using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWork.Models
{
    [Table("ProductProductPhoto", Schema = "Production")]
    public class ProductProductPhoto
    {
        public int ProductID { get; set; }
        public int ProductPhotoID { get; set; }
        public int Primary { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
