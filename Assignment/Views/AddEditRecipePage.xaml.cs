using Assignment.Models;
using Assignment.ViewModels;

namespace Assignment.Views;

public partial class AddEditRecipePage : ContentPage
{
    private readonly RecipeViewModel _recipeViewModel;
    private Recipe _editingRecipe;
    private string _barcode;

    public AddEditRecipePage(RecipeViewModel recipeViewModel)
    {
        InitializeComponent();
        _recipeViewModel = recipeViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Dispatcher.Dispatch(() =>
        {
            if (PageTitle != null && PageTitle.Handler != null)
                PageTitle.SetSemanticFocus();
        });

        if (BindingContext is Recipe recipe)
        {
            _editingRecipe = recipe;
            NameEntry.Text = recipe.Name;
            IngredientsEditor.Text = string.Join("\n", recipe.Ingredients);
            InstructionsEditor.Text = string.Join("\n", recipe.Instructions);
            CategoryPicker.SelectedItem = recipe.Category;
        }

        if (Shell.Current?.CurrentPage?.BindingContext is BarcodeScanViewModel barcodeVm && !string.IsNullOrEmpty(barcodeVm.ScannedBarcode))
        {
            _barcode = barcodeVm.ScannedBarcode;
        }
    }

    private bool ValidateInput()
    {
        bool isValid = true;
        NameError.IsVisible = string.IsNullOrWhiteSpace(NameEntry.Text);
        if (NameError.IsVisible) isValid = false;

        IngredientsError.IsVisible = string.IsNullOrWhiteSpace(IngredientsEditor.Text);
        if (IngredientsError.IsVisible) isValid = false;

        InstructionsError.IsVisible = string.IsNullOrWhiteSpace(InstructionsEditor.Text);
        if (InstructionsError.IsVisible) isValid = false;

        return isValid;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!ValidateInput()) return;

        var recipe = _editingRecipe ?? new Recipe { Id = Guid.NewGuid().ToString() };
        recipe.Name = NameEntry.Text;
        recipe.Ingredients = IngredientsEditor.Text.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
        recipe.Instructions = InstructionsEditor.Text.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
        recipe.Category = CategoryPicker.SelectedItem?.ToString() ?? "Lunch";
        recipe.Barcode = _barcode;

        if (_editingRecipe == null)
            _recipeViewModel.Recipes.Add(recipe);
        _recipeViewModel.RefreshFilteredRecipes();

        SemanticScreenReader.Default.Announce($"ÒÑ±£´æÊ³Æ× {recipe.Name}");
        await Shell.Current.GoToAsync("///MainPage");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
}