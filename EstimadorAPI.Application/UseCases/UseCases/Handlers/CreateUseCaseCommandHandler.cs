using AutoMapper;
using EstimadorAPI.Application.DTOs.UseCases;
using EstimadorAPI.Application.UseCases.UseCases.Commands;
using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Handlers;

public class CreateUseCaseCommandHandler : IRequestHandler<CreateUseCaseCommand, UseCaseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUseCaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UseCaseDto> Handle(CreateUseCaseCommand request, CancellationToken cancellationToken)
    {
        var projectExists = await _unitOfWork.Projects.GetByIdAsync(request.Dto.ProjectId);
        if (projectExists == null)
            throw new KeyNotFoundException($"Proyecto con ID {request.Dto.ProjectId} no encontrado");

        var useCase = new UseCase(
            request.Dto.Code,
            request.Dto.Title,
            request.Dto.Description,
            request.Dto.ProjectId
        )
        {
            Preconditions = request.Dto.Preconditions,
            Postconditions = request.Dto.Postconditions
        };

        await _unitOfWork.UseCases.AddAsync(useCase);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UseCaseDto>(useCase);
    }
}
