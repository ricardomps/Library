using LibraryApi.Contracts.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("[controller]")]
[ApiController]
[Produces("application/json")]
public class ResourcesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResourcesDTO), StatusCodes.Status200OK)]
    public ActionResult<ResourcesDTO> GetResources()
    {
        var resources = new ResourcesDTO
        {
            Authors = new[] { "Author1", "Author2" },
            Publishers = new[] { "Publisher1", "Publisher2" }
        };
        return Ok(resources);
    }
}