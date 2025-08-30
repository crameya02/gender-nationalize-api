using Xunit;
using Domain.Models;
using Application.Mappers;
using Domain.Helpers;

public class ProfileMapperTests
{
    public ProfileMapperTests()
    {
        // Load country names for test context
        CountryLookup.LoadFromFile("./Files/ISO3166-1.alpha2.json");
    }

    [Fact]
    public void Map_ExpandsCountryCodesAndRemovesDuplicates()
    {
        var gender = new GenderResult { Name = "cram", Gender = "male" };
        var nationality = new NationalityResult
        {
            Country = new List<CountryProbability>
            {
                new CountryProbability { CountryId = "US", Probability = 0.9 },
                new CountryProbability { CountryId = "PH", Probability = 0.8 },
                new CountryProbability { CountryId = "US", Probability = 0.7 }
            }
        };

        var result = ProfileMapper.Map(gender, nationality);

        Assert.Equal("cram", result.Name);
        Assert.Equal("male", result.Gender);
        Assert.Contains("United States", result.Nationality);
        Assert.Contains("Philippines", result.Nationality);
        Assert.Equal(2, result.Nationality.Count); // deduplicated
    }
    [Fact]
    public void Map_WithEmptyNationality_ReturnsEmptyList()
    {
        var gender = new GenderResult { Name = "cram", Gender = "male" };
        var nationality = new NationalityResult { Country = new List<CountryProbability>() };

        var result = ProfileMapper.Map(gender, nationality);

        Assert.Equal("cram", result.Name);
        Assert.Equal("male", result.Gender);
        Assert.Empty(result.Nationality);
    }
    [Fact]
    public void Map_WithUnknownCountryCode_ReturnsCodeAsFallback()
    {
        var gender = new GenderResult { Name = "cram", Gender = "male" };
        var nationality = new NationalityResult
        {
            Country = new List<CountryProbability>
        {
            new CountryProbability { CountryId = "ZZ", Probability = 0.5 }
        }
        };

        var result = ProfileMapper.Map(gender, nationality);

        Assert.Contains("ZZ", result.Nationality); // fallback to code
    }

}
