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

    public ProfileController(IGenderService genderService, INationalityService nationalityService)
    {
        _genderService = genderService;
        _nationalityService = nationalityService;
    }

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
