using AutoMapper;
using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.UseCases.Actors.Commands;
using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;

/// <summary>
/// Handler para crear un nuevo actor
/// </summary>
public class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, ActorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateActorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActorDto> Handle(CreateActorCommand request, CancellationToken cancellationToken)
    {
        var useCase = await _unitOfWork.UseCases.GetByIdAsync(request.Dto.UseCaseId);
        if (useCase == null)
            throw new KeyNotFoundException($"UseCase con ID {request.Dto.UseCaseId} no encontrado");

        var existingActor = await _unitOfWork.Actors.FirstOrDefaultAsync(a =>
            a.UseCaseId == request.Dto.UseCaseId &&
            a.Name.ToLower() == request.Dto.Name.ToLower() &&
            !a.IsDeleted);

        if (existingActor != null)
            throw new InvalidOperationException($"Ya existe un actor '{request.Dto.Name}' en este UseCase");

        var actor = new Actor(
            request.Dto.Name,
            request.Dto.Type,
            request.Dto.Description,
            request.Dto.UseCaseId
        );

        await _unitOfWork.Actors.AddAsync(actor);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ActorDto>(actor);
    }
}
