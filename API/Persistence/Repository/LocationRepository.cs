using System.Collections.Generic;
using System.Linq;
using API.Core.Domain.Models.Defaults;
using API.Core.Interfaces;

namespace API.Persistence.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DataContext _context;
        public readonly int _allState = 247;

        public LocationRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<object> getCountries() {
            return 
                (from c in _context.Countries
                select new { Id = c.Id, Name = c.Name }).ToList();
        }

        public IEnumerable<object> getStates(int countryId) {
            return 
                (from c in _context.States
                where c.CountryId.Equals(countryId) || c.CountryId.Equals(_allState)
                select new { Id = c.Id, Name = c.Name }).ToList();
        }
    }
}