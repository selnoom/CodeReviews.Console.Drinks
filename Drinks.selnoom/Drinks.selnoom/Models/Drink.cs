﻿using System.Reflection;
using System.Text.Json.Serialization;

namespace Drinks.selnoom.Models;

public class Drink
{
    [JsonPropertyName("idDrink")]
    public string DrinkId { get; set; }

    [JsonPropertyName("strDrink")]
    public string DrinkName { get; set; }

    [JsonPropertyName("strCategory")]
    public string Category { get; set; }

    [JsonPropertyName("strAlcoholic")]
    public string Alcoholic { get; set; }

    [JsonPropertyName("strGlass")]
    public string Glass { get; set; }

    [JsonPropertyName("strInstructions")]
    public string Instructions { get; set; }

    public List<string> Ingredients { get; set; } = new();
    public List<string> Quantity { get; set; } = new();

    [JsonPropertyName("strIngredient1")]
    public string Ingredient1 { get; set; }
    [JsonPropertyName("strIngredient2")]
    public string Ingredient2 { get; set; }
    [JsonPropertyName("strIngredient3")]
    public string Ingredient3 { get; set; }
    [JsonPropertyName("strIngredient4")]
    public string Ingredient4 { get; set; }
    [JsonPropertyName("strIngredient5")]
    public string Ingredient5 { get; set; }
    [JsonPropertyName("strIngredient6")]
    public string Ingredient6 { get; set; }
    [JsonPropertyName("strIngredient7")]
    public string Ingredient7 { get; set; }
    [JsonPropertyName("strIngredient8")]
    public string Ingredient8 { get; set; }
    [JsonPropertyName("strIngredient9")]
    public string Ingredient9 { get; set; }
    [JsonPropertyName("strIngredient10")]
    public string Ingredient10 { get; set; }
    [JsonPropertyName("strIngredient11")]
    public string Ingredient11 { get; set; }
    [JsonPropertyName("strIngredient12")]
    public string Ingredient12 { get; set; }
    [JsonPropertyName("strIngredient13")]
    public string Ingredient13 { get; set; }
    [JsonPropertyName("strIngredient14")]
    public string Ingredient14 { get; set; }
    [JsonPropertyName("strIngredient15")]
    public string Ingredient15 { get; set; }

    [JsonPropertyName("strMeasure1")]
    public string Measure1 { get; set; }
    [JsonPropertyName("strMeasure2")]
    public string Measure2 { get; set; }
    [JsonPropertyName("strMeasure3")]
    public string Measure3 { get; set; }
    [JsonPropertyName("strMeasure4")]
    public string Measure4 { get; set; }
    [JsonPropertyName("strMeasure5")]
    public string Measure5 { get; set; }
    [JsonPropertyName("strMeasure6")]
    public string Measure6 { get; set; }
    [JsonPropertyName("strMeasure7")]
    public string Measure7 { get; set; }
    [JsonPropertyName("strMeasure8")]
    public string Measure8 { get; set; }
    [JsonPropertyName("strMeasure9")]
    public string Measure9 { get; set; }
    [JsonPropertyName("strMeasure10")]
    public string Measure10 { get; set; }
    [JsonPropertyName("strMeasure11")]
    public string Measure11 { get; set; }
    [JsonPropertyName("strMeasure12")]
    public string Measure12 { get; set; }
    [JsonPropertyName("strMeasure13")]
    public string Measure13 { get; set; }
    [JsonPropertyName("strMeasure14")]
    public string Measure14 { get; set; }
    [JsonPropertyName("strMeasure15")]
    public string Measure15 { get; set; }

    public void PopulateIngredientsAndMeasures()
    {
        for (int i = 1; i <= 15; i++)
        {
            PropertyInfo ingredientProp = GetType().GetProperty($"Ingredient{i}");
            PropertyInfo measureProp = GetType().GetProperty($"Measure{i}");

            string ingredient = ingredientProp?.GetValue(this) as string;
            string measure = measureProp?.GetValue(this) as string;

            if (!string.IsNullOrWhiteSpace(ingredient))
            {
                Ingredients.Add(ingredient);
                Quantity.Add(measure);
            }
        }
    }
}

public class DrinkResponse()
{
    [JsonPropertyName("drinks")]
    public List<Drink> Drinks { get; set; }
}
