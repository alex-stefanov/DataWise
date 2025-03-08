using Microsoft.AspNetCore.Mvc;
using INTERFACES = DataWise.Core.Services.Interfaces;
using MODELS = DataWise.Data.DbContexts.NonReleational.Models;

namespace DataWise.Api.Controllers;

/// <summary>
/// Controller for managing data structures.
/// </summary>
/// <param name="structureService">The structure service instance.</param>
[Route("api/structure")]
[ApiController]
public class StructureController(
    INTERFACES.IStructureService structureService)
    : ControllerBase
{
    /// <summary>
    /// Retrieves a data structure by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the data structure.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the data structure if found, or an error message.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MODELS.DataStructure), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute]
        string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("DataStructure id cannot be null or empty.");

        var structure = await structureService
            .GetByIdAsync(id);

        if (structure is null)
            return NotFound($"DataStructure with Id '{id}' not found.");

        return Ok(structure);
    }

    /// <summary>
    /// Retrieves a data structure by its name.
    /// </summary>
    /// <param name="name">The name of the data structure.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the data structure if found, or an error message.
    /// </returns>
    [HttpGet("byname/{name}")]
    [ProducesResponseType(typeof(MODELS.DataStructure), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByName(
        [FromRoute]
        string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("DataStructure name cannot be null or empty.");

        var structure = await structureService
            .GetByNameAsync(name);

        if (structure is null)
            return NotFound($"DataStructure with Name '{name}' not found.");

        return Ok(structure);
    }
}