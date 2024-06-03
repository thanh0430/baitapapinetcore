using System.ComponentModel.DataAnnotations;

namespace baitapapinetcore.ViewModels
{
    public class ViewAccount
    {
        public int? Id { get; set; }   
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public int SCCCD { get; set; }
        public string Email { get; set; }// Sử dụng để làm tk đăng nhập
        public string Password { get; set; }
        public string? Role { get; set; } //1: admin, 2:USER
    }
}
