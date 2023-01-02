using Microsoft.AspNetCore.Mvc;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Import;

namespace Ynventory.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [HttpPost]
        public async Task<IActionResult> Import(string request)
        {
            try
            {
                await _importService.ImportCSV(request);
                return Ok();
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }
    }
}
