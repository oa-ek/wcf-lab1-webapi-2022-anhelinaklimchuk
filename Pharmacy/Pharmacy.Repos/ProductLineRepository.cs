using Pharmacy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Repos
{
    public class ProductLineRepository
    {
        public readonly PharmacyDbContext _ctx;

        public ProductLineRepository(PharmacyDbContext ctx)
        {
            _ctx = ctx;
        }
        public ProductLine GetProductLine(int id)
        {
            return _ctx.ProductLine.Find(id);
        }
        public List<ProductLine> GetAllProductLine()
        {
            return _ctx.ProductLine.ToList();
        }
    }
}
