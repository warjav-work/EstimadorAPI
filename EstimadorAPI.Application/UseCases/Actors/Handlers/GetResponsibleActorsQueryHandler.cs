using AutoMapper;
using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.UseCases.Actors.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;

/// <summary>
/// Handler para obtener actores responsables de pasos
/// </summary>
public class GetResponsibleActorsQueryHandler : IRequestHandler<GetResponsibleActorsQuery, IEnumerable<ActorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetResponsibleActorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ActorDto>> Handle(GetResponsibleActorsQuery request, CancellationToken cancellationToken)
    {
        var useCase = await _unitOfWork.UseCases.GetByIdAsync(request.UseCaseId);
        if (useCase == null)
            throw new KeyNotFoundException($"UseCase {request.UseCaseId} no encontrado");

        var steps = await _unitOfWork.UseCaseSteps.FindAsync(s =>
            s.UseCase!.Id == request.UseCaseId &&
            s.ResponsibleActorId.HasValue &&
            !s.IsDeleted);

        var actorIds = steps.Select(s => s.ResponsibleActorId!.Value).Distinct();
        var actors = await _unitOfWork.Actors.FindAsync(a => actorIds.Contains(a.Id));

        return _mapper.Map<IEnumerable<ActorDto>>(actors);
    }
}
