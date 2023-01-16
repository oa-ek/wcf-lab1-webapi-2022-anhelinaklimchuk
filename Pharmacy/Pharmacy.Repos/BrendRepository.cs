using Pharmacy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Repos
{
    public class BrendRepository
    {
        public readonly PharmacyDbContext _ctx;

        public BrendRepository(PharmacyDbContext ctx)
        {
            _ctx = ctx;
        }
        public Brend GetBrend(int id)
        {
            return _ctx.Brend.Find(id);
        }
        public List<Brend> GetAllBrend()
        {
            return _ctx.Brend.ToList();
        }
    }
}
