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
            Name = "经典煎蛋",
            Description = "简单美味的早餐煎蛋",
            Ingredients = new List<string> { "鸡蛋 2个", "盐少许", "黑胡椒少许", "油适量" },
            Instructions = new List<string> { "1. 热锅倒油", "2. 打入鸡蛋", "3. 撒上盐和胡椒", "4. 煎至蛋白凝固即可" },
            Category = "Breakfast",
            Calories = 180,
            Protein = 12,
            Fat = 14,
            Carbs = 2
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "番茄意面",
            Description = "经典意式番茄面",
            Ingredients = new List<string> { "意面 200g", "番茄 3个", "蒜 2瓣", "橄榄油适量", "盐适量" },
            Instructions = new List<string> { "1. 煮意面至 al dente", "2. 炒香蒜末，加入番茄", "3. 意面捞出，与酱汁混合" },
            Category = "Dinner",
            Calories = 450,
            Protein = 14,
            Fat = 12,
            Carbs = 68
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "牛油果吐司",
            Description = "健康早餐，富含健康脂肪",
            Ingredients = new List<string> { "全麦吐司 2片", "牛油果 1个", "柠檬汁 少许", "盐、胡椒适量", "辣椒碎 可选" },
            Instructions = new List<string> { "1. 吐司烤至金黄", "2. 牛油果捣成泥，加柠檬汁、盐、胡椒", "3. 抹在吐司上，撒辣椒碎" },
            Category = "Breakfast",
            Calories = 320,
            Protein = 8,
            Fat = 22,
            Carbs = 28
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "藜麦沙拉",
            Description = "高蛋白低脂沙拉",
            Ingredients = new List<string> { "藜麦 100g", "黄瓜 半根", "樱桃番茄 10颗", "柠檬汁 2勺", "橄榄油 1勺", "香菜少许" },
            Instructions = new List<string> { "1. 藜麦煮熟沥干", "2. 切好蔬菜", "3. 混合所有材料，加柠檬汁、橄榄油、盐" },
            Category = "Lunch",
            Calories = 280,
            Protein = 10,
            Fat = 8,
            Carbs = 42
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "三文鱼排",
            Description = "富含Omega-3的美味主菜",
            Ingredients = new List<string> { "三文鱼排 200g", "芦笋 100g", "柠檬 半个", "盐、黑胡椒", "橄榄油" },
            Instructions = new List<string> { "1. 三文鱼用盐胡椒腌制", "2. 煎至金黄，每面约3分钟", "3. 芦笋焯水后煎一下", "4. 摆盘挤柠檬汁" },
            Category = "Dinner",
            Calories = 380,
            Protein = 34,
            Fat = 24,
            Carbs = 4
        });
        Recipes.Add(new Recipe
        {
            Id = Guid.NewGuid().ToString(),
            Name = "巧克力蛋白奶昔",
            Description = "运动后恢复饮品",
            Ingredients = new List<string> { "香蕉 1根", "蛋白粉 1勺", "可可粉 1勺", "杏仁奶 250ml", "花生酱 1勺" },
            Instructions = new List<string> { "1. 所有材料放入搅拌机", "2. 搅打至顺滑", "3. 倒入杯中即可" },
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
        bool confirm = await Shell.Current.DisplayAlert("确认删除", $"确定要删除 \"{recipe.Name}\" 吗？", "删除", "取消");
        if (confirm)
        {
            Recipes.Remove(recipe);
            RefreshFilteredRecipes();
            SemanticScreenReader.Default.Announce($"已删除食谱 {recipe.Name}");
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