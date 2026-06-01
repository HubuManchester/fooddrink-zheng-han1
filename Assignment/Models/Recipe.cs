using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Assignment.Models;

public class Recipe : INotifyPropertyChanged
{
    private string _id;
    private string _name;
    private string _description;
    private List<string> _ingredients;
    private List<string> _instructions;
    private string _category;
    private string _barcode;
    private double _fat;
    private double _protein;
    private double _carbs;
    private double _calories;

    public string Id
    {
        get => _id;
        set { _id = value; OnPropertyChanged(); }
    }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(); }
    }

    public List<string> Ingredients
    {
        get => _ingredients;
        set { _ingredients = value; OnPropertyChanged(); }
    }

    public List<string> Instructions
    {
        get => _instructions;
        set { _instructions = value; OnPropertyChanged(); }
    }

    public string Category
    {
        get => _category;
        set { _category = value; OnPropertyChanged(); }
    }

    public string Barcode
    {
        get => _barcode;
        set { _barcode = value; OnPropertyChanged(); }
    }

    public double Fat
    {
        get => _fat;
        set { _fat = value; OnPropertyChanged(); }
    }

    public double Protein
    {
        get => _protein;
        set { _protein = value; OnPropertyChanged(); }
    }

    public double Carbs
    {
        get => _carbs;
        set { _carbs = value; OnPropertyChanged(); }
    }

    public double Calories
    {
        get => _calories;
        set { _calories = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}