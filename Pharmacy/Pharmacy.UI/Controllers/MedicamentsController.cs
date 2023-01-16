
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Pharmacy.Core;
using Pharmacy.Repos;
using Pharmacy.Repos.Dto;
using static Pharmacy.Core.Pictures;

namespace Pharmacy.UI.Controllers
{
    public class MedicamentsController : Controller
    {
        private readonly PharmacyDbContext _dbcontext;
        private readonly CategoryRepository _categoryRepository;
        private readonly SubCategoryRepository _subcategoryRepository;
        private readonly CatalogRepository _catalogRepository;
        private readonly MedicamentsRepository _medicamentsRepository;
        private readonly SubCategoryMedicamentsRepository _subcategorymedicamentsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly 

        public MedicamentsController(CategoryRepository categoryRepository, SubCategoryRepository subcategoryRepository,
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
        public async Task<FileContentResult> GetImage(int id)
        {
            var item = await _medicamentsRepository.GetMedicament(id);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, item.Image);
            var byteArray = System.IO.File.ReadAllBytes(path);
            return new FileContentResult(byteArray, "image/jpeg");
        }

        [HttpGet]
        public async Task<IActionResult> DetailsMedicament(int id)
        {
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View("Details", await _medicamentsRepository.InfoMedicaments(id));
        }

        // EDIT       

        public async Task<IActionResult> Edit(int id)
        {
            var med = await _medicamentsRepository.GetMedicament(id);
            ViewBag.Subcategory = await _subcategoryRepository.GetAllSubCategory();
            ViewBag.SubcategoryOfMed = await _subcategorymedicamentsRepository.GetSubCategoriesMedicament(med.MedicamentsId);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(med);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Medicaments model, string returnUrl, string[] subcategoriesofMed, IFormFile? picture)
        {
            var subcategory = new List<SubCategory>();
            foreach(var s in subcategoriesofMed)
            {
                subcategory.Add(await _subcategoryRepository.GetSubCategoryS(s));
            }
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    string picturePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "catalogue", picture.FileName);
                    using (FileStream fileStream = new FileStream(picturePath, FileMode.Create))
                        picture.CopyTo(fileStream);

                    model.Image = picturePath;
                }
                else { model.Image = null; }

                await _medicamentsRepository.UpdateAsync(model, subcategory);

                ViewBag.Subcategory = await _subcategoryRepository.GetAllSubCategory();
                return Redirect(returnUrl);
            }

            ViewBag.Subcategory = await _subcategoryRepository.GetAllSubCategory();
            return View(model);

        }

        // DELETE

        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Medicaments med = await _medicamentsRepository.GetMedicament(id);

            if (!string.Equals(med.Image, "no-photo.jpg"))
            {
                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "catalogue", med.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            await _medicamentsRepository.DeleteMedicament(id);
            return RedirectToPage("/CategoryProducts");
        }

        // CREATE

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Subcategory = await _subcategoryRepository.GetAllSubCategory();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Medicaments model, IFormFile? picture, string[] subcategories)
        { 
            ViewBag.Subcategory = await _subcategoryRepository.GetAllSubCategory();
            var listsubcategory = new List<SubCategory>();

            foreach(var i in subcategories)
            {
                listsubcategory.Add(await _subcategoryRepository.GetSubCategoryS(i));
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
                else { 
                    //picturePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "catalogue", "no-photo.jpg");
                    path = Path.Combine("img", "catalogue", "no-photo.jpg");
                }
                

                model.Image = path;

                Medicaments md = await _medicamentsRepository.Create(model.Name, model.Code, model.Price, model.ReleaseForm, model.Dosage, path, model.Description);
                await _subcategorymedicamentsRepository.AddToSubCategory(md, listsubcategory);
                return RedirectToAction("DetailsMedicament", "Medicaments", new { id = md.MedicamentsId });
            }
            return View(model);
        }

    }
}
