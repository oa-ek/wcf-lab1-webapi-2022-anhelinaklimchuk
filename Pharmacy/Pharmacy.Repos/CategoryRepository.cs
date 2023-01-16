using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Repos
{
    public class CategoryRepository
    {
        public readonly PharmacyDbContext _ctx;

        public CategoryRepository(PharmacyDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Category> GetCategory(int id)
        {
            return await _ctx.Category.FirstAsync(x => x.Id == id);
        }
        public async Task<Category> GetCategoryS(string id)
        {
            return await _ctx.Category.FirstAsync(x => x.Name == id);
        }
        public async Task<List<Category>> GetAllCategory()
        {
            return await _ctx.Category.Include(x => x.SubCategory).Include(x => x.Catalog).ToListAsync();
        }
        public async Task<List<Category>> GetCategoryCatalogWithSub(int id)
        {
            return await _ctx.Category.Include(x => x.SubCategory).Where(x => x.Catalog.Id == id).ToListAsync();
        }

        public async Task UpdateAsync(Category model, List<Catalog> catalog)
        {
            var md = await _ctx.Category.FirstAsync(x => x.Id == model.Id);

            if (md.Name != model.Name)
                md.Name = model.Name;
            md.Catalog = null;
            if (model.Image != null)
            {
                md.Image = model.Image;
            }

            if (catalog.Any())
            {
                await AddToCatalog(md, catalog);
            }

            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {

            var category = await GetCategory(id);
            _ctx.Category.Remove(category);
            await _ctx.SaveChangesAsync();
        }
        public async Task AddToCatalog(Category ct, List<Catalog> catalog)
        {
            foreach(var c in catalog)
            ct.Catalog = c;

            await _ctx.SaveChangesAsync();
        }

        public async Task<Category> CreateCategory(string name, List<Catalog> catalog, string photo)
        {
            var newCt = new Category
            {
                Name = name,
                Image = photo,
            };
            _ctx.Category.Add(newCt);
            await _ctx.SaveChangesAsync();

            await AddToCatalog(newCt, catalog);


            return await _ctx.Category.FirstAsync(x => x.Name == name);
        }
    }
}
