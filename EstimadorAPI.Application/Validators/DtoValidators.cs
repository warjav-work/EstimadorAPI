using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.DTOs.FunctionalRequirements;
using EstimadorAPI.Application.DTOs.Projects;
using EstimadorAPI.Application.DTOs.Steps;
using EstimadorAPI.Application.DTOs.UseCases;

using FluentValidation;

namespace EstimadorAPI.Application.Validators;

public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código es requerido")
            .Length(1, 20).WithMessage("El código debe tener entre 1 y 20 caracteres")
            .Matches(@"^[A-Z0-9\-]+$").WithMessage("El código solo puede contener letras mayúsculas, números y guiones");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .Length(3, 100).WithMessage("El nombre debe tener entre 3 y 100 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es requerida")
            .Length(10, 500).WithMessage("La descripción debe tener entre 10 y 500 caracteres");

        RuleFor(x => x.ClientName)
            .MaximumLength(100).WithMessage("El nombre del cliente no debe exceder 100 caracteres");
    }
}

public class CreateUseCaseDtoValidator : AbstractValidator<CreateUseCaseDto>
{
    public CreateUseCaseDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código es requerido")
            .Length(1, 20).WithMessage("El código debe tener entre 1 y 20 caracteres");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es requerido")
            .Length(5, 150).WithMessage("El título debe tener entre 5 y 150 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es requerida")
            .Length(10, 500).WithMessage("La descripción debe tener entre 10 y 500 caracteres");

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("El ProjectId debe ser válido");
    }
}

public class CreateActorDtoValidator : AbstractValidator<CreateActorDto>
{
    public CreateActorDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del actor es requerido")
            .Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es requerida")
            .Length(5, 200).WithMessage("La descripción debe tener entre 5 y 200 caracteres");

        RuleFor(x => x.UseCaseId)
            .GreaterThan(0).WithMessage("El UseCaseId debe ser válido");
    }
}

public class CreateUseCaseStepDtoValidator : AbstractValidator<CreateUseCaseStepDto>
{
    public CreateUseCaseStepDtoValidator()
    {
        RuleFor(x => x.StepNumber)
            .GreaterThan(0).WithMessage("El número de paso debe ser mayor a 0");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es requerida")
            .Length(5, 300).WithMessage("La descripción debe tener entre 5 y 300 caracteres");

        RuleFor(x => x.UseCaseId)
            .GreaterThan(0).WithMessage("El UseCaseId debe ser válido");
    }
}

public class CreateFunctionalRequirementDtoValidator : AbstractValidator<CreateFunctionalRequirementDto>
{
    public CreateFunctionalRequirementDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código es requerido")
            .Length(1, 20).WithMessage("El código debe tener entre 1 y 20 caracteres");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es requerido")
            .Length(5, 150).WithMessage("El título debe tener entre 5 y 150 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es requerida")
            .Length(10, 500).WithMessage("La descripción debe tener entre 10 y 500 caracteres");

        RuleFor(x => x.UseCaseId)
            .GreaterThan(0).WithMessage("El UseCaseId debe ser válido");
    }
}