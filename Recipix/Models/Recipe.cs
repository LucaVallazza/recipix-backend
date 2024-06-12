using ErrorOr;
using Recipix.ServiceErrors;

namespace Recipix.Models;

// Internal class representation to work with Recipe object easily
public class Recipe
{

    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 15;
    public const int MaxDescriptionLength = 150;


    public Guid Id { get; }

    public string Name { get; }
    public string Description { get; }

    public DateTime EndDateTime { get; }
    public DateTime StartDateTime { get; }

    public DateTime LastModifiedDateTime { get; }

    public List<string> Ingredients { get; }
    public List<string> Savory { get; }


    // Constructor
    private Recipe(Guid id, string name, string description, DateTime end, DateTime start, DateTime lastModified, List<string> ingredients, List<string> savory)
    {

        // Initialize variables
        Id = id;
        Name = name;
        Description = description;
        EndDateTime = end;
        StartDateTime = start;
        LastModifiedDateTime = lastModified;
        Ingredients = ingredients;
        Savory = savory;
    }

    public static ErrorOr<Recipe> Create(string name, string description, DateTime end, DateTime start, List<string> ingredients, List<string> savory)
    {

        List<Error> errors = new List<Error>();

        if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Recipe.InvalidName);
        }

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Recipe.InvalidDescription);
        }

        if(errors.Count > 0){
            return errors;
        }

        var recipe = new Recipe(
        Guid.NewGuid(),
                name,
                description,
                end,
                start,
                DateTime.UtcNow,
                ingredients,
                savory
        );


    }
}