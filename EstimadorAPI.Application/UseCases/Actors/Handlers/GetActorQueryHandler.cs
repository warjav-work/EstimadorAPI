using AutoMapper;
using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.UseCases.Actors.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;

/// <summary>
/// Handler para obtener un actor por ID
/// </summary>
public class GetActorQueryHandler : IRequestHandler<GetActorQuery, ActorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActorDto> Handle(GetActorQuery request, CancellationToken cancellationToken)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(request.Id);
        if (actor == null)
            throw new KeyNotFoundException($"Actor con ID {request.Id} no encontrado");

        return _mapper.Map<ActorDto>(actor);
    }
}
