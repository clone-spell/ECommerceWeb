using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; }
        public string? Image { get; set; }
    }
}
