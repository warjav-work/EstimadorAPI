using AutoMapper;
using EstimadorAPI.Application.DTOs.UseCases;
using EstimadorAPI.Application.UseCases.UseCases.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Handlers;

public class GetProjectUseCasesQueryHandler : IRequestHandler<GetProjectUseCasesQuery, IEnumerable<UseCaseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProjectUseCasesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UseCaseDto>> Handle(GetProjectUseCasesQuery request, CancellationToken cancellationToken)
    {
        var useCases = await _unitOfWork.UseCases.GetProjectUseCasesAsync(request.ProjectId);
        return _mapper.Map<IEnumerable<UseCaseDto>>(useCases);
    }
}
