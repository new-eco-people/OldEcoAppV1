using System.Collections.Generic;

namespace API.Core.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<object> getCountries();

        IEnumerable<object> getStates(int countryId);
    }
}