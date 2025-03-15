using Drinks.selnoom.Menu;
using Drinks.selnoom.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

ServiceCollection services = new();
ConfigureServices(services);
var serviceProvider = services.BuildServiceProvider();

var menu = serviceProvider.GetRequiredService<Menu>();

await menu.ShowMenu();

static void ConfigureServices(ServiceCollection services)
{
    services.AddHttpClient<CocktailApiService>(client =>
    {
        client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
    });
    services.AddTransient<Menu>();
}