using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.UseCases.Actors.Commands;
using EstimadorAPI.Application.UseCases.Actors.Queries;
using EstimadorAPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstimadorAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ActorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene un actor por ID
    /// </summary>
    /// <param name="id">ID del actor</param>
    /// <returns>Datos del actor</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActorDto>> GetActor(int id)
    {
        try
        {
            var actor = await _mediator.Send(new GetActorQuery(id));
            return Ok(actor);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene todos los actores de un caso de uso
    /// </summary>
    /// <param name="useCaseId">ID del caso de uso</param>
    /// <returns>Lista de actores</returns>
    [HttpGet("usecase/{useCaseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetUseCaseActors(int useCaseId)
    {
        try
        {
            var actors = await _mediator.Send(new GetUseCaseActorsQuery(useCaseId));
            return Ok(actors);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene actores filtrados por tipo
    /// </summary>
    /// <param name="useCaseId">ID del caso de uso</param>
    /// <param name="actorType">Tipo de actor (1=Primary, 2=Secondary, 3=Tertiary)</param>
    /// <returns>Lista de actores del tipo especificado</returns>
    [HttpGet("usecase/{useCaseId}/type/{actorType}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActorsByType(int useCaseId, int actorType)
    {
        try
        {
            var actors = await _mediator.Send(new GetActorsByTypeQuery(useCaseId, (ActorType)actorType));
            return Ok(actors);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene estadísticas de actores en un caso de uso
    /// </summary>
    /// <param name="useCaseId">ID del caso de uso</param>
    /// <returns>Estadísticas de actores</returns>
    [HttpGet("statistics/{useCaseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActorDto>> GetActorStatistics(int useCaseId)
    {
        try
        {
            var statistics = await _mediator.Send(new GetActorStatisticsQuery(useCaseId));
            return Ok(statistics);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Verifica si existe un nombre de actor
    /// </summary>
    /// <param name="useCaseId">ID del caso de uso</param>
    /// <param name="name">Nombre del actor</param>
    /// <param name="excludeActorId">ID del actor a excluir de la búsqueda (opcional)</param>
    /// <returns>true si existe, false si no</returns>
    [HttpGet("check-name")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<ActionResult<bool>> CheckActorNameExists(
        [FromQuery] int useCaseId,
        [FromQuery] string name,
        [FromQuery] int? excludeActorId = null)
    {
        var exists = await _mediator.Send(new CheckActorNameExistsQuery(useCaseId, name, excludeActorId));
        return Ok(exists);
    }

    /// <summary>
    /// Crea un nuevo actor
    /// </summary>
    /// <param name="dto">Datos del nuevo actor</param>
    /// <returns>Actor creado</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ActorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ActorDto>> CreateActor([FromBody] CreateActorDto dto)
    {
        try
        {
            var actor = await _mediator.Send(new CreateActorCommand(dto));
            return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, actor);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza un actor existente
    /// </summary>
    /// <param name="id">ID del actor</param>
    /// <param name="dto">Datos actualizados</param>
    /// <returns>Actor actualizado</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ActorDto>> UpdateActor(int id, [FromBody] UpdateActorDto dto)
    {
        try
        {
            var actor = await _mediator.Send(new UpdateActorCommand(id, dto));
            return Ok(actor);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Cambia el tipo de un actor
    /// </summary>
    /// <param name="id">ID del actor</param>
    /// <param name="newActorType">Nuevo tipo (1=Primary, 2=Secondary, 3=Tertiary)</param>
    /// <returns>Actor actualizado</returns>
    [HttpPatch("{id}/type")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActorDto>> ChangeActorType(int id, [FromQuery] int newActorType)
    {
        try
        {
            var actor = await _mediator.Send(new ChangeActorTypeCommand(id, (ActorType)newActorType));
            return Ok(actor);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Elimina un actor
    /// </summary>
    /// <param name="id">ID del actor</param>
    /// <returns>Resultado de la operación</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteActor(int id)
    {
        try
        {
            await _mediator.Send(new DeleteActorCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

}
