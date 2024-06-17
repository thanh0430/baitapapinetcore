using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace baitapapinetcore.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "SCCCD của bạn nhập chưa đúng.")]
        public string  SCCCD { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    /*    public string GoogleId { get; set; }
        public string Avatar {  get; set; }*/
        [Required]
        [EmailAddress(ErrorMessage = "Email của bạn nhập chưa đúng định dạng.")]
        public string Email { get; set; }// Sử dụng để làm tk đăng nhập
        [Required]
        public string Password { get; set; }           
        public string Role {  get; set; } //1: admin, 2:USER
    }
}
