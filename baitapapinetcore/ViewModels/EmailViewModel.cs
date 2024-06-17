using System.ComponentModel.DataAnnotations;

namespace baitapapinetcore.ViewModels
{
    public class EmailViewModel
    {
        [Required]
        [EmailAddress]
        public string ToEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
