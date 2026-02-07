using AutoMapper;
using EstimadorAPI.Application.DTOs.Projects;
using EstimadorAPI.Application.UseCases.Projects.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Projects.Handlers;

public class GetProjectDetailQueryHandler : IRequestHandler<GetProjectDetailQuery, ProjectDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProjectDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProjectDetailDto> Handle(GetProjectDetailQuery request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Projects.GetProjectWithUseCasesAsync(request.Id);
        if (project == null)
            throw new KeyNotFoundException($"Proyecto con ID {request.Id} no encontrado");

        return _mapper.Map<ProjectDetailDto>(project);
    }
}
