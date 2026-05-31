using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Assignment.Models;
using System.Collections.ObjectModel;

namespace Assignment.ViewModels;

public partial class RecipeViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Recipe> recipes;

    [ObservableProperty]
    private Recipe selectedRecipe;

    [ObservableProperty]
    private string searchText;

    [ObservableProperty]
    private string selectedCategory = "All";

    public List<string> Categories { get; } = new() { "All", "Breakfast", "Lunch", "Dinner", "Dessert" };

    public RecipeViewModel()
    {
        Recipes = new ObservableCollection<Recipe>();
        LoadSampleRecipes();
    }

    private void LoadSampleRecipes()
    {
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Classic Fried Egg",
            Description = "Simple and delicious breakfast egg",
            Ingredients = new List<string> { "2 eggs", "pinch of salt", "pinch of black pepper", "some oil" },
            Instructions = new List<string> { "1. Heat oil in pan", "2. Crack eggs into pan", "3. Add salt and pepper", "4. Fry until egg white is set" },
            Category = "Breakfast",
            Calories = 180,
            Protein = 12,
            Fat = 14,
            Carbs = 2
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Tomato Pasta",
            Description = "Classic Italian tomato pasta",
            Ingredients = new List<string> { "200g pasta", "3 tomatoes", "2 garlic cloves", "olive oil", "salt" },
            Instructions = new List<string> { "1. Cook pasta until al dente", "2. Sauté garlic, add tomatoes", "3. Mix pasta with sauce" },
            Category = "Dinner",
            Calories = 450,
            Protein = 14,
            Fat = 12,
            Carbs = 68
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Avocado Toast",
            Description = "Healthy breakfast rich in healthy fats",
            Ingredients = new List<string> { "2 whole wheat bread slices", "1 avocado", "lemon juice", "salt & pepper", "chili flakes (optional)" },
            Instructions = new List<string> { "1. Toast bread until golden", "2. Mash avocado with lemon juice, salt, pepper", "3. Spread on toast, sprinkle chili flakes" },
            Category = "Breakfast",
            Calories = 320,
            Protein = 8,
            Fat = 22,
            Carbs = 28
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Quinoa Salad",
            Description = "High protein low fat salad",
            Ingredients = new List<string> { "100g quinoa", "half cucumber", "10 cherry tomatoes", "2 tbsp lemon juice", "1 tbsp olive oil", "some cilantro" },
            Instructions = new List<string> { "1. Cook quinoa and drain", "2. Chop vegetables", "3. Mix all with lemon juice, olive oil, salt" },
            Category = "Lunch",
            Calories = 280,
            Protein = 10,
            Fat = 8,
            Carbs = 42
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Salmon Steak",
            Description = "Omega-3 rich main course",
            Ingredients = new List<string> { "200g salmon steak", "100g asparagus", "half lemon", "salt & black pepper", "olive oil" },
            Instructions = new List<string> { "1. Season salmon with salt and pepper", "2. Pan fry each side for 3 minutes", "3. Blanch asparagus then lightly fry", "4. Plate and squeeze lemon juice" },
            Category = "Dinner",
            Calories = 380,
            Protein = 34,
            Fat = 24,
            Carbs = 4
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Chocolate Protein Shake",
            Description = "Post-workout recovery drink",
            Ingredients = new List<string> { "1 banana", "1 scoop protein powder", "1 tbsp cocoa powder", "250ml almond milk", "1 tbsp peanut butter" },
            Instructions = new List<string> { "1. Put all ingredients in a blender", "2. Blend until smooth", "3. Pour into glass and serve" },
            Category = "Dessert",
            Calories = 350,
            Protein = 25,
            Fat = 12,
            Carbs = 35
        });
    }

    public IEnumerable<Recipe> FilteredRecipes
    {
        get
        {
            var query = Recipes.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(SearchText))
                query = query.Where(r => r.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                        r.Ingredients.Any(i => i.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            if (SelectedCategory != "All")
                query = query.Where(r => r.Category == SelectedCategory);
            return query;
        }
    }

    public void RefreshFilteredRecipes()
    {
        OnPropertyChanged(nameof(FilteredRecipes));
    }

    [RelayCommand]
    private void Search()
    {
        OnPropertyChanged(nameof(FilteredRecipes));
    }

    [RelayCommand]
    private void FilterByCategory(string category)
    {
        SelectedCategory = category;
        OnPropertyChanged(nameof(FilteredRecipes));
    }

    [RelayCommand]
    private async Task AddRecipe()
    {
        await Shell.Current.GoToAsync("///AddEditRecipePage");
    }

    [RelayCommand]
    private async Task EditRecipe(Recipe recipe)
    {
        var parameters = new Dictionary<string, object> { { "Recipe", recipe } };
        await Shell.Current.GoToAsync("///AddEditRecipePage", parameters);
    }

    [RelayCommand]
    private async Task DeleteRecipe(Recipe recipe)
    {
        bool confirm = await Shell.Current.DisplayAlert("Confirm Delete", $"Are you sure you want to delete \"{recipe.Name}\"?", "Delete", "Cancel");
        if (confirm)
        {
            Recipes.Remove(recipe);
            RefreshFilteredRecipes();
            SemanticScreenReader.Default.Announce($"Deleted recipe {recipe.Name}");
        }
    }

    [RelayCommand]
    private async Task GoToDetail(Recipe recipe)
    {
        if (recipe == null) return;
        var parameters = new Dictionary<string, object> { { "Recipe", recipe } };
        await Shell.Current.GoToAsync("///RecipeDetailPage", parameters);
    }
}