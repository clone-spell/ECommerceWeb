using ECommerceWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceWeb.ViewModels
{
    public class SubCategoryViewModel
    {
        public SubCategory SubCategory { get; set; }
        public IFormFile? Image { get; set; }
    }
}
