using EstimadorAPI.Application.DTOs.Results;
using EstimadorAPI.Application.DTOs.UseCases;
using EstimadorAPI.Application.UseCases.UseCases.Commands;
using EstimadorAPI.Application.UseCases.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstimadorAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UseCasesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UseCasesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene un caso de uso por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UseCaseDto>> GetUseCase(int id)
    {
        var useCase = await _mediator.Send(new GetUseCaseQuery(id));
        return Ok(useCase);
    }

    /// <summary>
    /// Obtiene un caso de uso con todos sus detalles
    /// </summary>
    [HttpGet("{id}/detail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UseCaseDetailDto>> GetUseCaseDetail(int id)
    {
        var useCase = await _mediator.Send(new GetUseCaseDetailQuery(id));
        return Ok(useCase);
    }

    /// <summary>
    /// Obtiene todos los casos de uso de un proyecto
    /// </summary>
    [HttpGet("project/{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UseCaseDto>>> GetProjectUseCases(int projectId)
    {
        var useCases = await _mediator.Send(new GetProjectUseCasesQuery(projectId));
        return Ok(useCases);
    }

    /// <summary>
    /// Obtiene la estimación de un caso de uso
    /// </summary>
    [HttpGet("{id}/estimation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EstimationResultDto>> GetUseCaseEstimation(int id)
    {
        var estimation = await _mediator.Send(new GetUseCaseEstimationQuery(id));
        return Ok(estimation);
    }

    /// <summary>
    /// Crea un nuevo caso de uso
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UseCaseDto>> CreateUseCase([FromBody] CreateUseCaseDto dto)
    {
        var useCase = await _mediator.Send(new CreateUseCaseCommand(dto));
        return CreatedAtAction(nameof(GetUseCase), new { id = useCase.Id }, useCase);
    }

    /// <summary>
    /// Actualiza un caso de uso
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UseCaseDto>> UpdateUseCase(int id, [FromBody] UpdateUseCaseDto dto)
    {
        var useCase = await _mediator.Send(new UpdateUseCaseCommand(id, dto));
        return Ok(useCase);
    }

    /// <summary>
    /// Elimina un caso de uso
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUseCase(int id)
    {
        await _mediator.Send(new DeleteUseCaseCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Estima los requisitos de un caso de uso
    /// </summary>
    [HttpPost("{id}/estimate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EstimationResultDto>> EstimateUseCase(int id)
    {
        var estimation = await _mediator.Send(new EstimateUseCaseCommand(id));
        return Ok(estimation);
    }
}

