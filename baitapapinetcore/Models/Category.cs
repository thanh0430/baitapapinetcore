using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace baitapapinetcore.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string producttype { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Image { get; set; }
        public virtual ICollection<Product>? Products { get; set; }// navigation properties
    }
    
}
