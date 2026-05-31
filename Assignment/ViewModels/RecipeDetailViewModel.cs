using CommunityToolkit.Mvvm.ComponentModel;
using Assignment.Models;

namespace Assignment.ViewModels;

[QueryProperty(nameof(Recipe), "Recipe")]
public partial class RecipeDetailViewModel : ObservableObject
{
    [ObservableProperty]
    private Recipe recipe;
}