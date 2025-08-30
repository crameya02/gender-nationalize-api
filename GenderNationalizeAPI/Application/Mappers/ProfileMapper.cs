using Domain.Models;
using Domain.Helpers;
namespace Application.Mappers
{
    public static class ProfileMapper
    {
        public static ProfileResult Map(GenderResult gender, NationalityResult nationality)
        {
            var countries = nationality.Country ?? new List<CountryProbability>();

            return new ProfileResult
            {
                Name = gender.Name,
                Gender = gender.Gender,
                Nationality = countries
                    .OrderByDescending(c => c.Probability)
                    .Select(c => CountryLookup.GetName(c.CountryId))
                    .Distinct()
                    .ToList()
            };
        }
    }

}
