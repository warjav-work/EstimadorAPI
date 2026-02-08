using AutoMapper;
using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.UseCases.Actors.Queries;
using EstimadorAPI.Domain.Enums;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;
/// <summary>
/// Handler para obtener estadísticas de actores de un UseCase
/// </summary>
public class GetActorStatisticsQueryHndler : IRequestHandler<GetActorStatisticsQuery, ActorStatisticsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActorStatisticsQueryHndler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActorStatisticsDto> Handle(GetActorStatisticsQuery request, CancellationToken cancellationToken)
    {
        // Validar que el UseCase existe
        var useCase = await _unitOfWork.UseCases.GetByIdAsync(request.useCaseId);

        if (useCase == null)
        {
            throw new KeyNotFoundException($"UseCase with ID {request.useCaseId} not found.");
        }
        // Obtener todos los actores para el UseCase
        var actors = await _unitOfWork.Actors.GetUseCaseActorsAsync(request.useCaseId);
        var actorlist = actors.ToList();

        // Contar actores por tipo
        var primaryCount = actorlist.Count(a => a.Type == ActorType.Primary);
        var secondaryCount = actorlist.Count(a => a.Type == ActorType.Secondary);
        var tertiaryCount = actorlist.Count(a => a.Type == ActorType.Tertiary);

        return new ActorStatisticsDto
        {
            UseCaseId = request.useCaseId,
            TotalActors = actorlist.Count,
            PrimaryActors = primaryCount,
            SecondaryActors = secondaryCount,
            TertiaryActors = tertiaryCount,
            ActorsList = _mapper.Map<List<ActorDto>>(actorlist)
        };
    }
}
