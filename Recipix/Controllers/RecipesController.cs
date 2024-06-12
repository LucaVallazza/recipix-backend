using System.Runtime.CompilerServices;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Recipix.Contracts.Recipe;
using Recipix.Models;
using Recipix.ServiceErrors;
using Recipix.Services.Recipes;

namespace Recipix.Controllers;

[ApiController]

[Route("[controller]")]
public class RecipesController : ControllerBase
{
    // Use the service
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }


    [HttpPost("")]
    public IActionResult CreateRecipe(CreateRecipeRequest request)
    {
        ErrorOr<Recipe> requestRecipeResult = Recipe.Create(
            request.Name,
            request.Description,
            request.EndDateTime,
            request.StartDateTime,
            request.Ingredients,
            request.Savory
        );

        if (requestRecipeResult.IsError)
        {
            return Problem(requestRecipeResult.Errors[0].Description);
        }

        // Use the service to create a new recipe and pass the recipe received from the client

        var recipe = requestRecipeResult.Value;
        ErrorOr<Created> createRecipeResult = _recipeService.CreateRecipe(recipe);

        if (createRecipeResult.IsError)
        {
            Console.WriteLine("/POST returns problem");
            return Problem(createRecipeResult.FirstError.Description);
        }

        var response = MapRecipeResponse(recipe);

        return CreatedAtGetRecipe(recipe);
    }



    [HttpGet("{id:guid}")]
    public IActionResult GetRecipe(Guid id)
    {

        ErrorOr<Recipe> getRecipeResult = _recipeService.GetRecipe(id);

        return getRecipeResult.Match(
            (recipe) => Ok(MapRecipeResponse(recipe)),
            errors => Problem(errors[0].Description));

        // if(getRecipeResult.IsError && getRecipeResult.FirstError == Errors.Recipe.NotFound ){
        //     return NotFound();
        // }

        // // create a recipe with the value once checked it doesn't return an error
        // Recipe recipe = getRecipeResult.Value;

        // // map the recipe to send the response
        // var response = MapRecipeResponse(recipe);

        // return Ok(response);



    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertRecipe(Guid id, UpsertRecipeRequest request)
    {
        var requestRecipeResult = Recipe.Create(
        request.Name,
        request.Description,
        request.StartDateTime,
        request.EndDateTime,
        request.Ingredients,
        request.Savory,
        id
    );

        if (requestRecipeResult.IsError)
        {
            return Problem(requestRecipeResult.Errors[0].Description);
        }

        var recipe = requestRecipeResult.Value;

        ErrorOr<UpsertedRecipe> upsertRecipeResult = _recipeService.UpsertRecipe(recipe);



        return upsertRecipeResult.Match(
            upserted => (IActionResult)(upserted.isNewlyCreated ?
            CreatedAtGetRecipe(recipe) :
            NoContent()),

            errors => Problem(errors[0].Description)
            );





        // Return 201 if a new breakfast is created

    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteRecipe(Guid id)
    {

        ErrorOr<Deleted> deleteRecipeResult = _recipeService.DeleteRecipe(id);



        return deleteRecipeResult.Match(
            deleted => NoContent(),
            errors =>
            {
                Console.WriteLine(errors);
                return (IActionResult)Problem(errors[0].Description);
            }
        );
    }

    private static RecipeResponse MapRecipeResponse(Recipe recipe)
    {
        return new RecipeResponse(
          Id: recipe.Id,
          Name: recipe.Name,
          recipe.Description,
          recipe.StartDateTime,
          recipe.EndDateTime,
          recipe.LastModifiedDateTime,
          recipe.Ingredients,
          recipe.Savory
        );
    }

    private CreatedAtActionResult CreatedAtGetRecipe(Recipe recipe)
    {
        return CreatedAtAction(
            actionName: nameof(GetRecipe),
            routeValues: new { id = recipe.Id },
            value: MapRecipeResponse(recipe)
            );
    }

}