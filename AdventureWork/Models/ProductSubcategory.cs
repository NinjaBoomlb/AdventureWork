using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWork.Models
{
    [Table("ProductSubcategory", Schema = "Production")]
    public class ProductSubcategory
    {
    public int ProductSubcategoryID {  get; set; }
    public int ProductCategoryID { get; set; }

    public string Name { get; set; }

    public Guid rowguid { get; set; }

    public DateTime ModifedDate { get; set; }
    }
}
