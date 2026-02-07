using AutoMapper;
using EstimadorAPI.Application.DTOs.UseCases;
using EstimadorAPI.Application.UseCases.UseCases.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Handlers;

public class GetUseCaseDetailQueryHandler : IRequestHandler<GetUseCaseDetailQuery, UseCaseDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUseCaseDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UseCaseDetailDto> Handle(GetUseCaseDetailQuery request, CancellationToken cancellationToken)
    {
        var useCase = await _unitOfWork.UseCases.GetUseCaseWithDetailsAsync(request.Id);
        if (useCase == null)
            throw new KeyNotFoundException($"Caso de uso con ID {request.Id} no encontrado");

        return _mapper.Map<UseCaseDetailDto>(useCase);
    }
}
