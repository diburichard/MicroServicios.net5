using Microsoft.AspNetCore.Mvc;
using MS.AFORO255.History.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MS.AFORO255.History.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get(int accountId)
        {
            var result = await _historyService.GetAll();
            var model = result.Where(x => x.AccountId == accountId).ToList();
            return Ok(model);

        }
    }
}
