
using DAL.Entities;

namespace BLL.Services.Abstractions
{
    public interface IScientistsService
    {
        Task<IEnumerable<Scientist>> GetScientistsAsync(string query);
    }
}
