using AutoMapper;
using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.UseCases.Actors.Commands;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;

/// <summary>
/// Handler para cambiar tipo de actor
/// </summary>
public class ChangeActorTypeCommandHandler : IRequestHandler<ChangeActorTypeCommand, ActorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChangeActorTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActorDto> Handle(ChangeActorTypeCommand request, CancellationToken cancellationToken)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(request.Id);
        if (actor == null)
            throw new KeyNotFoundException($"Actor con ID {request.Id} no encontrado");

        actor.Type = request.NewType;
        actor.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Actors.Update(actor);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ActorDto>(actor);
    }
}
