using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.Models;
using Application.Mappers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IGenderService _genderService;
    private readonly INationalityService _nationalityService;

        /// <summary>
        /// Constructor for the ProfileController
        /// </summary>
        /// <param name="genderService">The service that provides the gender data</param>
        /// <param name="nationalityService">The service that provides the nationality data</param>
    public ProfileController(IGenderService genderService, INationalityService nationalityService)
    {
        _genderService = genderService;
        _nationalityService = nationalityService;
    }

        /// <summary>
        /// Gets the profile of the given name
        /// </summary>
        /// <param name="name">The name of the person</param>
        /// <returns>Returns the assembled profile as a JSON response to the client.</returns>
        /// <response code="400">The name parameter is required</response>
        /// <response code="500">Internal server error</response>
    [HttpGet]
    public async Task<ActionResult<ProfileResult>> GetProfile([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name parameter is required.");

        try
        {
            var gender = await _genderService.GetGenderAsync(name);
            var nationality = await _nationalityService.GetNationalityAsync(name);

            var profile = ProfileMapper.Map(gender, nationality);
            return Ok(profile);
        }
        catch (Exception ex)
        {
            // You can log the error here if needed
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
