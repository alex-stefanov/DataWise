using DataWise.Data.DbContexts.NonReleational.Models;
using DataWise.Data.Repositories.NonReleational;
using Microsoft.AspNetCore.Mvc;

namespace DataWise.Api.Controllers;

[Route("api/structure")]
[ApiController]
public class StructureController(
    IMongoRepository<DataStructure, string> repository)
    : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        [FromRoute]
        string id)
    {
        var structure = await repository.GetByIdAsync(id);
        if (structure is null)
            return NotFound($"DataStructure with Id '{id}' not found.");
        return Ok(structure);
    }

    [HttpGet("byname/{name}")]
    public IActionResult GetByName(
        [FromRoute]
        string name)
    {
        var structure = repository.FirstOrDefault(ds => ds.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (structure is null)
            return NotFound($"DataStructure with Name '{name}' not found.");
        return Ok(structure);
    }
}