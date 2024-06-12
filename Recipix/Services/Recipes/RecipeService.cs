using ErrorOr;
using Microsoft.OpenApi.Any;
using Recipix.Models;
using Recipix.ServiceErrors;

namespace Recipix.Services.Recipes;

// Implementation of the RecipeInterface
public class RecipeService : IRecipeService
{

    //TODO: Use a database instead of a dictionary
    private static Dictionary<Guid, Recipe> _recipes = new();

    public ErrorOr<Created> CreateRecipe(Recipe recipe)
    {
        Console.WriteLine("Recipe description:");
        Console.WriteLine(recipe.Description);

        _recipes.Add(recipe.Id, recipe);

        return Result.Created;
    }

    public ErrorOr<Recipe> GetRecipe(Guid id)
    {
        if(_recipes.TryGetValue(id, out var recipe)){
            return recipe;

        }

        return Errors.Recipe.NotFound;

    }

    public ErrorOr<Deleted> DeleteRecipe(Guid id)
    {
        _recipes.Remove(id);

        return Result.Deleted;

    }

    public ErrorOr<UpsertedRecipe> UpsertRecipe(Recipe recipe)
    {

        var isNewlyCreated = !_recipes.ContainsKey(recipe.Id);
        
        _recipes[recipe.Id] = recipe;

        return new UpsertedRecipe(isNewlyCreated);
    }
}