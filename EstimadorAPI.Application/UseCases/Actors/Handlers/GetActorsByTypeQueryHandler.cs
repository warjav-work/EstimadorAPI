using AutoMapper;
using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.UseCases.Actors.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;

/// <summary>
/// Handler para obtener actores por tipo
/// </summary>
public class GetActorsByTypeQueryHandler : IRequestHandler<GetActorsByTypeQuery, IEnumerable<ActorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActorsByTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ActorDto>> Handle(GetActorsByTypeQuery request, CancellationToken cancellationToken)
    {
        var useCase = await _unitOfWork.UseCases.GetByIdAsync(request.UseCaseId);
        if (useCase == null)
            throw new KeyNotFoundException($"UseCase {request.UseCaseId} no encontrado");

        var actors = await _unitOfWork.Actors.FindAsync(a =>
            a.UseCaseId == request.UseCaseId &&
            a.Type == request.Type &&
            !a.IsDeleted);

        return _mapper.Map<IEnumerable<ActorDto>>(actors);
    }
}
