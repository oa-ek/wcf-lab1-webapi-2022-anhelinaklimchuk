using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core;
using Pharmacy.Repos;

namespace Pharmacy.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly PharmacyDbContext _dbcontext;
        private readonly CategoryRepository _categoryRepository;
        private readonly SubCategoryRepository _subcategoryRepository;
        private readonly CatalogRepository _catalogRepository;
        private readonly MedicamentsRepository _medicamentsRepository;
        private readonly SubCategoryMedicamentsRepository _subcategorymedicamentsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        //private readonly 

        public CategoryController(CategoryRepository categoryRepository, SubCategoryRepository subcategoryRepository, 
            CatalogRepository catalogRepository, MedicamentsRepository medicamentsRepository,
            SubCategoryMedicamentsRepository subcategorymedicamentsRepository, IWebHostEnvironment webHostEnvironment)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _catalogRepository = catalogRepository;
            _medicamentsRepository = medicamentsRepository;
            _subcategorymedicamentsRepository = subcategorymedicamentsRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> IndexAsync(int id)
        {
            ViewData["id"] = id;
            var catalog = await _catalogRepository.GetCatalog(id);
            ViewData["catalog"] = catalog;
            return View(await _categoryRepository.GetCategoryCatalogWithSub(id));
        }

        [HttpGet]
        public async Task<FileContentResult> GetImage(int id)
        {
            var item = await _categoryRepository.GetCategory(id);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, item.Image);
            var byteArray = System.IO.File.ReadAllBytes(path);
            return new FileContentResult(byteArray, "image/jpeg");
        }

        [HttpGet]
        public async Task<IActionResult> CategoryProducts(int id)
        {
            ViewData["id"] = id;
            var subcategory = await _subcategoryRepository.GetSubCategory(id);
            ViewData["title"] = subcategory.Name;
            ViewData["subcategory"] = subcategory.SubCategoryId;
            var medicaments = await _subcategorymedicamentsRepository.GetAllMedicamentsFromSubCategory(subcategory.SubCategoryId);
            return View( await _medicamentsRepository.ListMedicaments(medicaments));
        }

        [HttpPost, ActionName("CategoryProductsS")]
        public async Task<IActionResult> CategoryProducts(int id, string customerName)
        {  
            ViewData["id"] = id;
            var subcategory = await _subcategoryRepository.GetSubCategory(id);
            ViewData["title"] = subcategory.Name;
            ViewData["subcategory"] = subcategory.SubCategoryId;
            var medicaments = await _subcategorymedicamentsRepository.GetAllMedicamentsFromSubCategory(subcategory.SubCategoryId);
            return View("CategoryProducts",await _medicamentsRepository.ListSearchMedicaments(medicaments, customerName));
        }
        [HttpGet]
        public async Task<IActionResult> CategoryAllProducts(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            ViewData["title"] = category.Name;
            var subcategories = await _subcategoryRepository.GetAllSubCategoryFromCategory(category);
            List<SubCategoryMedicaments> medicaments = new List<SubCategoryMedicaments>();
            foreach (var ct in subcategories)
            {
                medicaments.AddRange(await _subcategorymedicamentsRepository.GetAllMedicamentsFromSubCategory(ct.SubCategoryId));
            }
            return View("CategoryProducts",await _medicamentsRepository.ListMedicaments(medicaments));
        }
        [HttpGet]
        public IActionResult SubCategory(int id)
        {
            ViewData["id"] = id;
            var subcategory = _subcategoryRepository.GetSubCategory(id);
            ViewData["subcategory"] = subcategory;
            return View();//_subcategoryRepository.GetAllSubCategoryFromCategory(subcategory));
        }
    }
}
