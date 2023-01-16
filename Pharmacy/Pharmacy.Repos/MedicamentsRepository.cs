using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core;
using Pharmacy.Repos.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Repos
{
    public class MedicamentsRepository
    {
        private readonly PharmacyDbContext _ctx;
        private readonly SubCategoryMedicamentsRepository subCategoryMedicamentsRepos;

        public MedicamentsRepository(PharmacyDbContext ctx, SubCategoryMedicamentsRepository subCategoryMedicamentsRepos)
        {
            _ctx = ctx;
            this.subCategoryMedicamentsRepos = subCategoryMedicamentsRepos;
        }
        public async Task<Medicaments> GetMedicament(int id)
        {
            return await _ctx.Medicaments.Include(x=>x.SubCategories).ThenInclude(x=>x.SubCategory).FirstAsync(x=>x.MedicamentsId == id);
        }


        public async Task<List<Medicaments>> ListMedicaments(List<SubCategoryMedicaments> medicaments)
        {
            List<Medicaments> listmedicaments = new List<Medicaments>();
            foreach (var md in medicaments)
            {
                var medics = await _ctx.Medicaments.FirstAsync(x => x.MedicamentsId == md.MedicamentsId);
                listmedicaments.Add(medics);
            }
            return listmedicaments;
        }

        public async Task<List<Medicaments>> ListSearchMedicaments(List<SubCategoryMedicaments> medicaments, string search)
        {
            if (search == null)
            {
                return await ListMedicaments(medicaments);
            }
            else
            {
                List<Medicaments> listmedicaments = new List<Medicaments>();

                foreach (var md in medicaments)
                {
                    var medics = await _ctx.Medicaments.Where(x => x.Name.Contains(search)).FirstOrDefaultAsync(x => x.MedicamentsId == md.MedicamentsId);
                    listmedicaments.Add(medics);
                }

                return listmedicaments;
            }
        }


        public async Task<List<Medicaments>> GetAllMedicaments()
        {
            return await _ctx.Medicaments.Include(x => x.SubCategories).ThenInclude(x => x.SubCategory).ToListAsync();
        }
        /*public async Task<List<Medicaments>> GetAllMedicamentsFromCategory(SubCategory id)
        {
            return await _ctx.Medicaments.Where(x=>x.SubCategoryMedicaments==id).ToListAsync();
        }*/
        public async Task<Medicaments> InfoMedicaments(int id)
        {
            return await _ctx.Medicaments.Include(x=>x.Brend).Include(x => x.Country).Include(x => x.ProductLine).FirstAsync(x => x.MedicamentsId==id);
        }


        public async Task DeleteMedicament(int id)
        {
            var medicament = await GetMedicament(id);
            _ctx.Medicaments.Remove(medicament);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Medicaments model, List<SubCategory> subcategory)
        {
            var md = await _ctx.Medicaments.FirstAsync(x => x.MedicamentsId == model.MedicamentsId);

            if (md.Name != model.Name)
                md.Name = model.Name;

            if (md.Code != model.Code)
                md.Code = model.Code;

            if (md.Price != model.Price)
                md.Price = model.Price;

            if (md.ReleaseForm != model.ReleaseForm)
                md.ReleaseForm = model.ReleaseForm;

            if (md.Dosage != model.Dosage)
                md.Dosage = model.Dosage;

            if (md.Description != model.Description)
                md.Description = model.Description;
            if (model.Image != null)
            {
                    md.Image = model.Image;
            }
            
            // ProductLine
            // Brend
            // Country
            // SubCategory
            // Photo

            //var admRole = await roleManager.FindByNameAsync("Admin");

            if (await subCategoryMedicamentsRepos.GetSubCategoriesMedicament(md.MedicamentsId) != null)
            {
                await subCategoryMedicamentsRepos.RemoveFromSubCategory(md, await subCategoryMedicamentsRepos.GetSubCategoriesMedicament(md.MedicamentsId));
            }

            if (subcategory.Any())
            {
                await subCategoryMedicamentsRepos.AddToSubCategory(md, subcategory);
            }

            await _ctx.SaveChangesAsync();
        }

        public async Task<Medicaments> Create(string name, string code, float price, string? realiseform, string dosage, string photo, string description)
        {
            var newMd = new Medicaments
            {
                Name = name,
                Code = code,
                Price = price,
                ReleaseForm = realiseform,
                Dosage = dosage,
                Image = photo,
                Description = description,
            };
            _ctx.Medicaments.Add(newMd);
            await _ctx.SaveChangesAsync();


            return await _ctx.Medicaments.FirstAsync(x => x.Code == code);
            //return newMd;
        }
    }
}
