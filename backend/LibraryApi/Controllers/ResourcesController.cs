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
            Authors = new[]
            {
                "Richard Memeison",
                "Susan Dogeson",
                "Harold Smilesworth",
                "Brian Luckley",
                "Catherine Grump",
                "Erma Gerdman",
                "Philip Sauraptor",
                "Olivia Attached",
                "Ben Distractson",
                "Lola Frogman"
            },
            Publishers = new[]
            {
                "Twist & Shout Publishing",
                "Viral & Sons",
                "Grinworthy Books",
                "Epic Reads Ltd.",
                "Wholesome House",
                "Kek & Co.",
                "Rare Pepe Publishing",
                "Classic Internet Press",
                "Memeology Group",
                "Top Shelf Memes"
            }
        };
        return Ok(resources);
    }
}