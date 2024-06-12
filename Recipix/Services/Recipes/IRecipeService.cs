using ErrorOr;
using Recipix.Contracts.Recipe;
using Recipix.Models;

namespace Recipix.Services.Recipes;

// Create the interface for the Recipe Service

public interface IRecipeService{
    ErrorOr<Created> CreateRecipe(Recipe recipe);
    // Usign ErrorOr package
    ErrorOr<Recipe> GetRecipe(Guid id);    
    ErrorOr<UpsertedRecipe> UpsertRecipe(Recipe recipe);    
    ErrorOr<Deleted> DeleteRecipe(Guid id);    
}