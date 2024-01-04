using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class SubCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string Category { get; set; }

        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; }
        public string? Image { get; set; }
    }
}
