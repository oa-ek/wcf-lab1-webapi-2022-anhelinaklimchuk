using Pharmacy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Repos
{
    public class CountryRepository
    {
        public readonly PharmacyDbContext _ctx;

        public CountryRepository(PharmacyDbContext ctx)
        {
            _ctx = ctx;
        }
        public Country GetCountry(int id)
        {
            return _ctx.Country.Find(id);
        }
        public List<Country> GetAllCountry()
        {
            return _ctx.Country.ToList();
        }
    }
}
