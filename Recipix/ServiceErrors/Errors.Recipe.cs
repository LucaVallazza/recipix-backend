using ErrorOr;

namespace Recipix.ServiceErrors;


// Create a class with all the errors we expect to have
public static class Errors
{

    // Define the common errors
    public static class Recipe
    {
        // From ErrorOr package
        public static Error NotFound => Error.NotFound(
            code: "Recipe.NotFound",
            description: "Recipe not found");

        public static Error InvalidName => Error.NotFound(
        code: "Recipe.InvalidName",
        description: $"Recipe must be at least {Models.Recipe.MinNameLength} characters long and a maximung length of {Models.Recipe.MaxNameLength} characters");

        public static Error InvalidDescription => Error.NotFound(
        code: "Recipe.InvalidDescription",
        description: $"Recipe must be at least {Models.Recipe.MinDescriptionLength} characters long and a maximung length of {Models.Recipe.MaxDescriptionLength} characters");
    }

}
