using Domain.Models;

namespace Application.Services
{
    public interface IGenderService
    {
        Task<GenderResult> GetGenderAsync(string name);
    }
}