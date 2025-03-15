using Drinks.selnoom.Helpers;
using Drinks.selnoom.Models;
using Drinks.selnoom.Services;
using System.Reflection;

namespace Drinks.selnoom.Menu;

internal class Menu
{
    private readonly CocktailApiService _cocktailService;

    public Menu(CocktailApiService cocktailService)
    {
        _cocktailService = cocktailService;
    }

    internal async Task ShowMenu()
    {
        while(true)
        {
            Console.Clear();
            string asciiArt = @"
                ⠀⠀⠀⠀⠀⠀⠀⠀⡠⠤⠤⢤⣄⠀⠀⠀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠑⠢⣍⡀⠀⢡⡀
                ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡔⡍⠓⠢⢥
                ⣕⡃⠉⠉⠉⠉⠉⠁⠁⢐⣞⣂⠀⠀⠀⠀
                ⠐⠄⠀⠉⠉⠉⠉⠁⢀⡏⢀⠁⠀⠀⠀⠀
                ⠀⠀⠢⡀⠀⠀⠀⢀⡌⡠⠊⠀⠀⠀⠀⠀
                ⠀⠀⠀⠘⢆⠀⠀⣨⠎⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⢱⢞⠁⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⢘⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⢸⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⢸⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⢸⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⠀⠀⠀⠀⢸⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀
                ⠀⣠⠄⠂⠁⠀⠸⠁⠐⠠⢤⡄⠀⠀⠀⠀
                ⠀⠀⠉⠀⠒⠒⠒⠒⠒⠊⠉⠀⠀⠀⠀⠀";
            Console.WriteLine(asciiArt);

            var categoryResponse = await GetCategories();
            Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("Categories:");
            for (int i = 0; i < categoryResponse.Categories.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {categoryResponse.Categories[i].Name}");
            }
            Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*\n");
            Console.WriteLine("Please select a category or type 0 to exit:\n");

            Category selectedCategory = Validation.ValidateCategoryInput(categoryResponse.Categories);
            if (selectedCategory == null)
            {
                Console.Clear();
                Console.WriteLine("Goodbye!");
                return;
            }

            var drinksByCategory = await GetDrinksByCategory(selectedCategory.Name);

            Console.Clear();
            Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine($"{selectedCategory.Name} Drinks:");
            for (int i = 0; i < drinksByCategory.Drinks.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {drinksByCategory.Drinks[i].DrinkName}");
            }
            Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*\n");
            Console.WriteLine("Please select a drink or type 0 to return to previous menu:\n");

            Drink selectedDrink = Validation.ValidateDrinkInput(drinksByCategory.Drinks);
            if (selectedDrink == null)
            {
                continue;
            }

            var drinkResponse = await _cocktailService.GetDrinkById(selectedDrink.DrinkId);
            Drink drink = drinkResponse.Drinks.FirstOrDefault();

            Console.Clear();
            if (drink != null)
            {
                ShowDrinkDetails(drink);
            }
            else
            {
                Console.WriteLine("Drink does not exist");
            }

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }

    internal async Task<CategoryResponse> GetCategories()
    {
        return await _cocktailService.GetDrinkCategoriesAsync();
    }

    internal async Task<DrinkResponse> GetDrinksByCategory(string category)
    {
        var encodedCategory = Uri.EscapeDataString(category);
        return await _cocktailService.GetDrinksByCategory(encodedCategory);
    }

    internal void ShowDrinkDetails(Drink drink)
    {
        foreach (var prop in drink.GetType().GetProperties())
        {
            if (prop.Name == "DrinkId")
                continue;

            if ((prop.Name.StartsWith("Ingredient") && prop.Name != "Ingredients") ||
                (prop.Name.StartsWith("Measure") && prop.Name != "Quantity"))
                continue;

            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                continue;

            var value = prop.GetValue(drink);
            if (value != null)
            {
                Console.WriteLine($"{prop.Name}: {value}");
            }
        }

        Console.WriteLine("Ingredients and Quantity:");

        for (int i = 0; i < drink.Ingredients.Count; i++)
        {
            string ingredient = drink.Ingredients[i];
            string quantity = i < drink.Quantity.Count ? drink.Quantity[i] : "";
            Console.WriteLine($"- {ingredient} : {quantity}");
        }

        if(drink.Alcoholic == "Alcoholic")
        {
            Console.WriteLine("\nDo not drink and drive :)");
        }

        Console.WriteLine("\nHave fun mixing!");
    }
}
