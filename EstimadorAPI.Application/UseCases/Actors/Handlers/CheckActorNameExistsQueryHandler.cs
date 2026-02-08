using EstimadorAPI.Application.UseCases.Actors.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers
{
    public class CheckActorNameExistsQueryHandler : IRequestHandler<CheckActorNameExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckActorNameExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CheckActorNameExistsQuery request, CancellationToken cancellationToken)
        {
            // Buscar actor por el nombre especificado en el UseCase
            var actor = await _unitOfWork.Actors.FirstOrDefaultAsync(a => a.UseCaseId == request.UseCaseId &&
            a.Name.ToLower() == request.Name.ToLower() &&
            !a.IsDeleted);

            if (actor == null)
            {
                return false; // No existe un actor con ese nombre en el UseCase
            }
            if (request.ExcludeActorId.HasValue)
            {
                // Si el actor encontrado es el mismo que se quiere excluir, no se considera como existente
                return actor.Id != request.ExcludeActorId.Value;
            }
            // Si se encontró un actor con ese nombre y no se está excluyendo, entonces sí existe
            return true;
        }
    }
}
