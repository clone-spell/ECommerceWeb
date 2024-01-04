using ECommerceWeb.Models;
using ECommerceWeb.Repository;
using ECommerceWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _catRepo;
        private readonly IRepository<SubCategory> _subCatRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(IRepository<Category> catRepo, IRepository<SubCategory> subCatRepo, IWebHostEnvironment webHostEnvironment)
        {
            _catRepo=catRepo;
            _subCatRepo=subCatRepo;
            _webHostEnvironment=webHostEnvironment;
        }

        public IActionResult Index()
        {
            var cats = _catRepo.GetAll();
            return View(cats);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var cat = vm.Category;
                cat.Image = UploadImage(vm.Image);

                _catRepo.Add(cat);
                TempData["success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateSubCategory(string id)
        {
            var vm = new SubCategoryViewModel();
            var subCat = new SubCategory();
            subCat.Id = Guid.NewGuid();
            subCat.Category = id;
            vm.SubCategory = subCat;

            ViewBag.catName = _catRepo.Get(x=>x.Id.ToString() == id).Name;

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateSubCategory(SubCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var subCategory = vm.SubCategory;
                subCategory.Image = UploadImage(vm.Image);
                _subCatRepo.Add(subCategory);
                TempData["success"] = "Sub-Category Created Successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }


        //other function
        private string? UploadImage(IFormFile? image)
        {
            if(image== null)
                return null;

            string dir = Path.Combine(_webHostEnvironment.WebRootPath, "CategoryImages");

            if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string fileName = Guid.NewGuid().ToString()+".jpg";
            string filePath = Path.Combine(dir, fileName);
            image.CopyTo(new FileStream(filePath, FileMode.Create));

            return fileName;
        }
    }
}
