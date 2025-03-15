using Drinks.selnoom.Menu;
using Drinks.selnoom.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

ServiceCollection services = new();
ConfigureServices(services);
var serviceProvider = services.BuildServiceProvider();

var cocktailService = serviceProvider.GetRequiredService<CocktailApiService>();

var menu = serviceProvider.GetRequiredService<Menu>();

await menu.ShowMenu();


//var drinkByCategoryResponse = await cocktailService.GetDrinksByCategory("Other%20%2F%20Unknown");
//foreach (var drink in drinkByCategoryResponse.Drinks)
//{
//    Console.WriteLine($"- {drink.DrinkName}");
//}

//Console.WriteLine("");

//var drinkById = await cocktailService.GetDrinkById("11007");
//Console.WriteLine($"{drinkById.Drinks[0].DrinkName}");

static void ConfigureServices(ServiceCollection services)
{
    services.AddHttpClient<CocktailApiService>(client =>
    {
        client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
    });
    services.AddTransient<Menu>();
}