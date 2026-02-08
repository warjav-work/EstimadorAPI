using AutoMapper;
using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.UseCases.Actors.Commands;
using EstimadorAPI.Domain.Enums;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;

/// <summary>
/// Handler para actualizar un actor
/// </summary>
public class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand, ActorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateActorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActorDto> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(request.Id);
        if (actor == null)
            throw new KeyNotFoundException($"Actor con ID {request.Id} no encontrado");

        var duplicate = await _unitOfWork.Actors.FirstOrDefaultAsync(a =>
            a.UseCaseId == actor.UseCaseId &&
            a.Name.ToLower() == request.Dto.Name.ToLower() &&
            a.Id != request.Id &&
            !a.IsDeleted);

        if (duplicate != null)
            throw new InvalidOperationException($"Ya existe otro actor con nombre '{request.Dto.Name}'");

        actor.Name = request.Dto.Name;
        actor.Type = (ActorType)request.Dto.Type;
        actor.Description = request.Dto.Description;
        actor.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Actors.Update(actor);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ActorDto>(actor);
    }
}
