using BLL.DTOs;
using BLL.Services.Abstractions;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScientistsController : ControllerBase
    {
        private readonly IScientistsService _scientiestsService;
        private readonly IDataSourceService _dataSourceService;

        public ScientistsController(IScientistsService scientiestsService, IDataSourceService dataSourceService)
        {
            _scientiestsService = scientiestsService;
            _dataSourceService = dataSourceService;
        }

        [HttpGet("get-scientists")]
        public async Task<ActionResult<IEnumerable<Scientist>>> GetAsync([FromQuery] string query = "")
        {
            var scientists = await _scientiestsService.GetScientistsAsync(query);

            return Ok(scientists);
        }

        [HttpGet("get-scientist")]
        public ActionResult<ScientiestFullModel> Get(long wikiId)
        {
            var scientist = _dataSourceService.GetScientiest(wikiId);

            if (scientist == null)
            {
                return NotFound();
            }

            return Ok(scientist);
        }
    }
}
