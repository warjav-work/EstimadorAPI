using AutoMapper;
using EstimadorAPI.Application.DTOs.UseCases;
using EstimadorAPI.Application.UseCases.UseCases.Commands;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Handlers;

public class UpdateUseCaseCommandHandler : IRequestHandler<UpdateUseCaseCommand, UseCaseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUseCaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UseCaseDto> Handle(UpdateUseCaseCommand request, CancellationToken cancellationToken)
    {
        var useCase = await _unitOfWork.UseCases.GetByIdAsync(request.Id);
        if (useCase == null)
            throw new KeyNotFoundException($"Caso de uso con ID {request.Id} no encontrado");

        useCase.UpdateUseCase(request.Dto.Title, request.Dto.Description,
            request.Dto.Preconditions, request.Dto.Postconditions);
        _unitOfWork.UseCases.Update(useCase);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UseCaseDto>(useCase);
    }
}
