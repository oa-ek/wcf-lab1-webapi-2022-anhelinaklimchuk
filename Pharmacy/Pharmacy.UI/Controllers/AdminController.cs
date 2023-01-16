using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core;
using Pharmacy.Repos;
using Pharmacy.Repos.Dto;
using System.Data;
using System.IO;
using static Pharmacy.Core.Pictures;

namespace Pharmacy.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly SubCategoryRepository _subcategoryRepository;
        private readonly CatalogRepository _catalogRepository;
        private readonly MedicamentsRepository _medicamentsRepository;
        private readonly SubCategoryMedicamentsRepository _subcategorymedicamentsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly 

        public AdminController(CategoryRepository categoryRepository, SubCategoryRepository subcategoryRepository,
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            return View(await _categoryRepository.GetAllCategory());
        }

        // EDIT CATEGORY

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            ViewBag.Catalog = await _catalogRepository.GetAllCatalog();
            return View(await _categoryRepository.GetCategory(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditCategory(Category model, string[] catalog, IFormFile? picture)
        {
            var catalogs = new List<Catalog>();
            foreach (var item in catalog)
            {
                catalogs.Add(await _catalogRepository.GetCatalogS(item));
            }
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    string picturePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "catalogue", picture.FileName);
                    using (FileStream fileStream = new FileStream(picturePath, FileMode.Create))
                        picture.CopyTo(fileStream);

                    var path = Path.Combine("img", "catalogue", picture.FileName);

                    model.Image = path;
                }
                else { model.Image = null; }

                await _categoryRepository.UpdateAsync(model,catalogs);
            }
            return RedirectToAction("Categories"); ;
        }

        // DELETE CATEGORY

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            if (string.Equals(category.Image, "no-photo.jpg"))
            {
                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "catalogue", category.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            ViewBag.Subcategory = await _subcategoryRepository.GetAllSubCategoryFromCategory(category);
            return View(await _categoryRepository.GetCategory(id));
        }

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            ViewBag.Subcategory = await _subcategoryRepository.GetAllSubCategoryFromCategory(category);
            var subcategories = ViewBag.Subcategory;
            if (subcategories != null) 
            {
                await _categoryRepository.Delete(id);
                return RedirectToAction("Categories");
            }
            return View(category);            
        }

        // CREATE CATEGORY
        [HttpGet]
        public async Task<IActionResult> CreateCategoryAsync()
        {
            ViewBag.Catalog = await _catalogRepository.GetAllCatalog();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateCategory(Category model, string[] catalog, IFormFile? picture)
        {
            var catalogs = new List<Catalog>();
            foreach (var item in catalog)
            {
                catalogs.Add(await _catalogRepository.GetCatalogS(item)); 
            }
            if (ModelState.IsValid)
            {
                string picturePath;
                string path;
                if (picture != null)
                {
                    picturePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "catalogue", picture.FileName);
                    using (FileStream fileStream = new FileStream(picturePath, FileMode.Create))
                        picture.CopyTo(fileStream);
                    path = Path.Combine("img", "catalogue", picture.FileName);
                }
                else
                {
                    //picturePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "catalogue", "no-photo.jpg");
                    path = Path.Combine("img", "catalogue", "no-photo.jpg");
                }


                model.Image = path;

                Category ct = await _categoryRepository.CreateCategory(model.Name, catalogs, model.Image);
                //await _categoryRepository.AddToCatalog(ct, catalog);
                return RedirectToAction("Categories");
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Catalogs()
        {
            return View(await _catalogRepository.GetAllCatalog());
        }

        // EDIT CATALOG

        [HttpGet]
        public async Task<IActionResult> EditCatalog(int id)
        {
            return View(await _catalogRepository.GetCatalog(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditCatalog(Catalog model)
        {
            if (ModelState.IsValid)
            {
                await _catalogRepository.UpdateAsync(model);
            }
            return RedirectToAction("Catalogs"); ;
        }

        // DELETE CATALOG

        public async Task<IActionResult> DeleteCatalog(int id)
        {
            var catalog = await _catalogRepository.GetCatalog(id);
            ViewBag.Categories = await _categoryRepository.GetCategoryCatalogWithSub(catalog.Id);
            return View(await _catalogRepository.GetCatalog(id));
        }

        [HttpPost, ActionName("DeleteCatalog")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteC(int id)
        {
            var catalog = await _catalogRepository.GetCatalog(id);
            ViewBag.Categories = await _categoryRepository.GetCategoryCatalogWithSub(catalog.Id);
            var categories = ViewBag.Categories;
            if (categories == null)
            {
                await _catalogRepository.Delete(id);
                return RedirectToAction("Catalogs");
            }
            return View(catalog);
        }


        // CREATE CATALOG

        [HttpGet]
        public async Task<IActionResult> CreateCatalogAsync()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateCatalog(Catalog model)
        {
            if (ModelState.IsValid)
            {
                Catalog ct = await _catalogRepository.CreateCatalog(model.Name);
                return RedirectToAction("Catalogs");
            }
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> SubCategories()
        {
            return View(await _subcategoryRepository.GetAllSubCategory());
        }

        // EDIT SUBCATEGORY

        [HttpGet]
        public async Task<IActionResult> EditSubCategory(int id)
        {
            ViewBag.Categories = await _categoryRepository.GetAllCategory();
            return View(await _subcategoryRepository.GetSubCategory(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditSubCategory(SubCategory model, string[] category)
        {
            var categories = new List<Category>();
            foreach (var item in category)
            {
                categories.Add(await _categoryRepository.GetCategoryS(item));
            }
            if (ModelState.IsValid)
            {
                await _subcategoryRepository.UpdateAsync(model, categories);
            }
            return RedirectToAction("SubCategories"); ;
        }

        // DELETE SUBCATEGORY

        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subcategory = await _subcategoryRepository.GetSubCategory(id);
            ViewBag.SubcategoryMed = await _subcategorymedicamentsRepository.GetAllMedicamentsFromSubCategory(subcategory.SubCategoryId);
            return View(await _subcategoryRepository.GetSubCategory(id));
        }

        [HttpPost, ActionName("DeleteSubCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSub(int id)
        {
            var subcategory = await _subcategoryRepository.GetSubCategory(id);
            ViewBag.SubcategoryMed = await _subcategorymedicamentsRepository.GetAllMedicamentsFromSubCategory(subcategory.SubCategoryId);
            var meds = ViewBag.SubcategoryMed;
            if (meds != null)
            {
                await _subcategoryRepository.Delete(id);
                return RedirectToAction("SubCategories");
            }
            return View(subcategory);
        }

        // CREATE SUBCATEGORY
        [HttpGet]
        public async Task<IActionResult> CreateSubCategoryAsync()
        {
            ViewBag.Category = await _categoryRepository.GetAllCategory();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateSubCategory(SubCategory model, string[] category)
        {
            var categories = new List<Category>();
            foreach (var item in category)
            {
                categories.Add(await _categoryRepository.GetCategoryS(item));
            }
            if (ModelState.IsValid)
            {
                SubCategory ct = await _subcategoryRepository.CreateSubCategory(model.Name, categories);
                //await _categoryRepository.AddToCatalog(ct, catalog);
                return RedirectToAction("SubCategories");
            }
            return View(model);
        }
    }
}
