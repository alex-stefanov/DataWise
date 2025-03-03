using DataWise.Data.DbContexts.NonReleational.Models;
using DataWise.Data.Repositories.NonReleational;
using Microsoft.AspNetCore.Mvc;

namespace DataWise.Api.Controllers;

[Route("api/algorithms")]
[ApiController]
public class AlgorithmController(
    IMongoRepository<Algorithm, string> repository)
    : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        [FromRoute] 
        string id)
    {
        var algorithm = await repository.GetByIdAsync(id);
        if (algorithm is null)
            return NotFound($"Algorithm with Id '{id}' not found.");
        return Ok(algorithm);
    }

    [HttpGet("byname/{name}")]
    public IActionResult GetByName(
        [FromRoute] 
        string name)
    {
        var algorithm = repository.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (algorithm is null)
            return NotFound($"Algorithm with Name '{name}' not found.");
        return Ok(algorithm);
    }
}