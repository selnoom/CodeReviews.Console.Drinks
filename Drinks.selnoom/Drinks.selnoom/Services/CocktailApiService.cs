using Drinks.selnoom.Helpers;
using Drinks.selnoom.Models;
using System.Text.Json;

namespace Drinks.selnoom.Services;

public class CocktailApiService
{
    private readonly HttpClient _httpClient;

    public CocktailApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CategoryResponse> GetDrinkCategoriesAsync()
    {
        var json = await _httpClient.GetStringAsync("list.php?c=list");
        var response = JsonSerializer.Deserialize<CategoryResponse>(json);

        return response;
    }

    public async Task<DrinkResponse> GetDrinksByCategory(string category)
    {
        var json = await _httpClient.GetStringAsync($"filter.php?c={category}");
        var response = JsonSerializer.Deserialize<DrinkResponse>(json);

        return response;
    }

    public async Task<DrinkResponse> GetDrinkById(string id)
    {
        var json = await _httpClient.GetStringAsync($"lookup.php?i={id}");
        var response = JsonSerializer.Deserialize<DrinkResponse>(json);

        if (response?.Drinks != null)
        {
            foreach (var drink in response.Drinks)
            {
                drink.PopulateIngredientsAndMeasures();
            }
        }

        return response;
    }
}
