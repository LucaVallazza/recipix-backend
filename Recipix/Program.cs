using Recipix.Services.Recipes;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    builder.Services.AddSingleton<IRecipeService, RecipeService>();

}


// Create app variable
var app = builder.Build();
{

    // Middleware to handle errors
    //    If there is an error, it automaticaly sends a request to the 
    //    specified route.
    app.UseExceptionHandler("/error");
    //app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();


}
