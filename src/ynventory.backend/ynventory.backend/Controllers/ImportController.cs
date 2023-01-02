using Microsoft.AspNetCore.Mvc;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Services.Import;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;

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

        /// <summary>
        /// Returns a list of all import tasks
        /// </summary>
        /// <returns>A list of all import tasks</returns>
        /// <response code="200">The list has been created</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CollectionResponse[]))]
        public async Task<IActionResult> getTasks(){
            return Ok(await _importService.getTasks());
        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="request">The details of the task to create</param>
        /// <returns>The newly created task</returns>
        /// <response code="201">The task was successfuly created</response>
        /// <response code="400"></response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ImportTaskResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Import(CreateImportTaskRequest request)
        {
            try
            {
                await _importService.CreateTask(request);
                return Ok();
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }
        }
    }
}
