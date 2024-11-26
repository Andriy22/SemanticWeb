using BLL.DTOs;
using DAL.Entities;

namespace BLL.Services.Abstractions
{
    public interface IDataSourceService
    {
        IEnumerable<Scientist> GetScientists();

        ScientiestFullModel? GetScientiest(long wikiId);
    }
}
