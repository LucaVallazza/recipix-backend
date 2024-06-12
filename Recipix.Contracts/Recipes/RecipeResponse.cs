namespace Recipix.Contracts.Recipe;

public record RecipeResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime,
    DateTime LastModifiedDateTime,
    List<string> Ingredients,
    List<string> Savory
);