using EstimadorAPI.Application.DTOs.ProjectEstimates;
using EstimadorAPI.Application.DTOs.Projects;
using EstimadorAPI.Application.UseCases.Projects.Commands;
using EstimadorAPI.Application.UseCases.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstimadorAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todos los proyectos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
    {
        var projects = await _mediator.Send(new GetAllProjectsQuery());
        return Ok(projects);
    }

    /// <summary>
    /// Obtiene un proyecto por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDto>> GetProject(int id)
    {
        var project = await _mediator.Send(new GetProjectQuery(id));
        return Ok(project);
    }

    /// <summary>
    /// Obtiene un proyecto con todos sus detalles
    /// </summary>
    [HttpGet("{id}/detail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDetailDto>> GetProjectDetail(int id)
    {
        var project = await _mediator.Send(new GetProjectDetailQuery(id));
        return Ok(project);
    }

    /// <summary>
    /// Obtiene las estimaciones de un proyecto
    /// </summary>
    [HttpGet("{id}/estimates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectEstimatesDto>> GetProjectEstimates(int id)
    {
        var estimates = await _mediator.Send(new GetProjectEstimatesQuery(id));
        return Ok(estimates);
    }

    /// <summary>
    /// Crea un nuevo proyecto
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectDto dto)
    {
        var project = await _mediator.Send(new CreateProjectCommand(dto));
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    /// <summary>
    /// Actualiza un proyecto existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectDto>> UpdateProject(int id, [FromBody] UpdateProjectDto dto)
    {
        var project = await _mediator.Send(new UpdateProjectCommand(id, dto));
        return Ok(project);
    }

    /// <summary>
    /// Elimina un proyecto
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await _mediator.Send(new DeleteProjectCommand(id));
        return NoContent();
    }
}
