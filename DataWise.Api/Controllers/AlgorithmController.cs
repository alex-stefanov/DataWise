using Microsoft.AspNetCore.Mvc;
using INTERFACES = DataWise.Core.Services.Interfaces;
using MODELS = DataWise.Data.DbContexts.NonReleational.Models;

namespace DataWise.Api.Controllers;

/// <summary>
/// Controller for managing algorithm-related endpoints.
/// </summary>
/// <param name="algorithmService">The algorithm service.</param>
[Route("api/algorithms")]
[ApiController]
public class AlgorithmController(
    INTERFACES.IAlgorithmService algorithmService)
    : ControllerBase
{
    /// <summary>
    /// Retrieves an algorithm by its identifier.
    /// </summary>
    /// <param name="id">The algorithm identifier.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the algorithm if found, or a corresponding error message.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MODELS.Algorithm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute]
            string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Algorithm id cannot be null or empty.");

        var algorithm = await algorithmService
            .GetAlgorithmByIdAsync(id);

        if (algorithm is null)
            return NotFound($"Algorithm with Id '{id}' not found.");

        return Ok(algorithm);
    }

    /// <summary>
    /// Retrieves an algorithm by its name.
    /// </summary>
    /// <param name="name">The algorithm name.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the algorithm if found, or a corresponding error message.
    /// </returns>
    [HttpGet("byname/{name}")]
    [ProducesResponseType(typeof(MODELS.Algorithm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByName(
        [FromRoute]
            string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Algorithm name cannot be null or empty.");

        var algorithm = await algorithmService
            .GetAlgorithmByNameAsync(name);

        if (algorithm is null)
            return NotFound($"Algorithm with Name '{name}' not found.");

        return Ok(algorithm);
    }
}