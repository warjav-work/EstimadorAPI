namespace EstimadorAPI.Domain.Enums;

/// <summary>
/// Tipos de actores en un caso de uso
/// </summary>
public enum ActorType
{
    Primary = 1,      // Inicia el caso de uso
    Secondary = 2,    // Interactúa pero no inicia
    Tertiary = 3      // Afectado pasivamente
}

/// <summary>
/// Niveles de complejidad de requisitos
/// </summary>
public enum RequirementComplexity
{
    Simple = 1,       // 3 puntos
    Moderate = 2,     // 5 puntos
    Complex = 3       // 8 puntos
}

/// <summary>
/// Estados de un caso de uso
/// </summary>
public enum UseCaseStatus
{
    Draft = 1,
    InReview = 2,
    Approved = 3,
    InProgress = 4,
    Completed = 5,
    Cancelled = 6
}

/// <summary>
/// Estados de un requisito
/// </summary>
public enum RequirementStatus
{
    Pending = 1,
    InProgress = 2,
    Completed = 3,
    OnHold = 4,
    Cancelled = 5
}

/// <summary>
/// Tipos de flujo en un caso de uso
/// </summary>
public enum FlowType
{
    Main = 1,
    Alternative = 2,
    Exception = 3
}