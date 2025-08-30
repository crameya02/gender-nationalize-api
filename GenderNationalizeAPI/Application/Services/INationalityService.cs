using Domain.Models;

namespace Application.Services
{
    public interface INationalityService
    {
        Task<NationalityResult> GetNationalityAsync(string name);
    }
}