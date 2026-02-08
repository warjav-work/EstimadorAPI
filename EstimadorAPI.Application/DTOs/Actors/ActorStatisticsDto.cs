namespace EstimadorAPI.Application.DTOs.Actors;

/// <summary>
/// DTO para estadísticas de actores
/// </summary>
public class ActorStatisticsDto
{
    public int UseCaseId { get; set; }
    public int TotalActors { get; set; }
    public int PrimaryActors { get; set; }
    public int SecondaryActors { get; set; }
    public int TertiaryActors { get; set; }
    public List<ActorDto> ActorsList { get; set; } = new();
}
