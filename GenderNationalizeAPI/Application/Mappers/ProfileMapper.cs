using Domain.Models;
using Domain.Helpers;
namespace Application.Mappers
{
    public static class ProfileMapper
    {
        /// <summary>
        /// Maps the given gender and nationality results to a profile result.
        /// The list of countries is sorted by probability descending and
        /// deduplicated by using the country names.
        /// </summary>
        /// <param name="gender">The gender result.</param>
        /// <param name="nationality">The nationality result.</param>
        /// <returns>The mapped profile result.</returns>
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
