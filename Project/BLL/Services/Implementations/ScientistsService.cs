using BLL.Services.Abstractions;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Implementations
{
    public class ScientistsService : IScientistsService
    {
        private readonly IDataSourceService _dataSourceService;
        private readonly ApplicationDbContext _context;

        public ScientistsService(IDataSourceService dataSourceService, ApplicationDbContext context)
        {
            _dataSourceService = dataSourceService;
            _context = context;
        }

        public async Task<IEnumerable<Scientist>> GetScientistsAsync(string query)
        {
            var scientists = await _context.Scientists.ToListAsync();

            if (string.IsNullOrEmpty(query) && !scientists.Any())
            {
                scientists = _dataSourceService.GetScientists().ToList();
                await _context.Scientists.AddRangeAsync(scientists);
                await _context.SaveChangesAsync();
            }

            scientists = scientists.Where(s => s.Fullname.Contains(query, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(query))
                .Take(50) 
                .OrderBy(s => s.Fullname)
                .ToList();

            return scientists;
        }

    }
}
