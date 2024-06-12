namespace Recipix.Contracts.Recipe;

public record CreateRecipeRequest(
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime,
    List<string> Ingredients,
    List<string> Savory
);