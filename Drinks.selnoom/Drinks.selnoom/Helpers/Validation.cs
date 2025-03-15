using Drinks.selnoom.Models;

namespace Drinks.selnoom.Helpers;

class Validation
{
    internal static int ValidateStringToInt()
    {
        string userInput = Console.ReadLine();
        int validatedInput;
        while (!int.TryParse(userInput, out validatedInput))
        {
            Console.WriteLine("\nInvalid input. Please try again:");
            userInput = Console.ReadLine();
        }
        return validatedInput;
    }

    internal static Category ValidateCategoryInput (List<Category> categories)
    {
        bool isValidated = false;
        Category selectedCategory = null;
        string input;
        while (!isValidated)
        {
            input = Console.ReadLine();
            if (input == "0")
            {
                return null;
            }
            if (int.TryParse(input, out int index) && index > 0 && index <= categories.Count)
            {
                selectedCategory = categories[index - 1];
                Console.WriteLine($"You selected: {selectedCategory.Name}\n");
                isValidated = true;
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number.\n");
            }
        }

        Console.WriteLine("Press enter to continue:");
        Console.ReadLine();

        return selectedCategory;
    }

    internal static Drink ValidateDrinkInput(List<Drink> drinks)
    {
        bool isValidated = false;
        Drink selectedDrink = null;
        string input;
        while (!isValidated)
        {
            input = Console.ReadLine();
            if (input == "0")
            {
                return null;
            }
            else if (int.TryParse(input, out int index) && index > 0 && index <= drinks.Count)
            {
                selectedDrink = drinks[index - 1];
                Console.WriteLine($"You selected: {selectedDrink.DrinkName}\n");
                isValidated = true;
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number or 0 to return to previous menu.\n");
            }
        }

        Console.WriteLine("Press enter to continue:");
        Console.ReadLine();

        return selectedDrink;
    }
}
