using AutoMapper;
using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.DTOs.Steps;
using EstimadorAPI.Application.UseCases.Actors.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;

/// <summary>
/// Handler para obtener actor con sus pasos relacionados
/// </summary>
public class GetActorWithStepsQueryHandler : IRequestHandler<GetActorWithStepsQuery, ActorDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActorWithStepsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActorDetailDto> Handle(GetActorWithStepsQuery request, CancellationToken cancellationToken)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(request.ActorId);
        if (actor == null)
            throw new KeyNotFoundException($"Actor {request.ActorId} no encontrado");

        var steps = await _unitOfWork.UseCaseSteps.FindAsync(s =>
            s.ResponsibleActorId == request.ActorId && !s.IsDeleted);

        var detail = _mapper.Map<ActorDetailDto>(actor);
        detail.ResponsibleForSteps = _mapper.Map<ICollection<UseCaseStepDto>>(steps);

        return detail;
    }
}
