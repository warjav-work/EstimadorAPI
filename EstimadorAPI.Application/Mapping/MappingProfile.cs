using AutoMapper;
using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.DTOs.FunctionalRequirements;
using EstimadorAPI.Application.DTOs.Projects;
using EstimadorAPI.Application.DTOs.Results;
using EstimadorAPI.Application.DTOs.Steps;
using EstimadorAPI.Application.DTOs.UseCases;
using EstimadorAPI.Domain.Entities;

namespace EstimadorAPI.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Project mappings
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.TotalUseCases, opt => opt.MapFrom(src => src.GetTotalUseCases()))
            .ForMember(dest => dest.TotalRequirements, opt => opt.MapFrom(src => src.GetTotalRequirements()))
            .ForMember(dest => dest.TotalStoryPoints, opt => opt.MapFrom(src => src.GetTotalStoryPoints()));

        CreateMap<Project, ProjectDetailDto>()
            .ForMember(dest => dest.UseCases, opt => opt.MapFrom(src => src.UseCases));

        CreateMap<CreateProjectDto, Project>();
        CreateMap<UpdateProjectDto, Project>();

        // UseCase mappings
        CreateMap<UseCase, UseCaseDto>()
            .ForMember(dest => dest.ActorCount, opt => opt.MapFrom(src => src.GetActorCount()))
            .ForMember(dest => dest.StepCount, opt => opt.MapFrom(src => src.GetTotalRequiredSteps()))
            .ForMember(dest => dest.RequirementCount, opt => opt.MapFrom(src => src.GetRequirementCount()));

        CreateMap<UseCase, UseCaseDetailDto>()
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
            .ForMember(dest => dest.Requirements, opt => opt.MapFrom(src => src.Requirements))
            .ForMember(dest => dest.EstimationResult, opt => opt.MapFrom(src => src.EstimationResult));

        CreateMap<CreateUseCaseDto, UseCase>();
        CreateMap<UpdateUseCaseDto, UseCase>();

        // Actor mappings
        CreateMap<Actor, ActorDto>();
        CreateMap<CreateActorDto, Actor>();

        // UseCaseStep mappings
        CreateMap<UseCaseStep, UseCaseStepDto>()
            .ForMember(dest => dest.ResponsibleActor, opt => opt.MapFrom(src => src.ResponsibleActor));

        CreateMap<CreateUseCaseStepDto, UseCaseStep>();

        // FunctionalRequirement mappings
        CreateMap<FunctionalRequirement, FunctionalRequirementDto>()
            .ForMember(dest => dest.DependentOnIds,
                opt => opt.MapFrom(src => src.Dependencies.Select(d => d.DependsOnRequirementId).ToList()));

        CreateMap<CreateFunctionalRequirementDto, FunctionalRequirement>();

        // EstimationResult mappings
        CreateMap<EstimationResult, EstimationResultDto>()
            .ForMember(dest => dest.ComplexityPercentage, opt => opt.MapFrom(src => src.GetComplexityPercentage()))
            .ForMember(dest => dest.RiskScore, opt => opt.MapFrom(src => src.GetRiskScore()));
    }
}