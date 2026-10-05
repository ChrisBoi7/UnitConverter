using UnitConverter.Models;
using UnitConverter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UnitConverter.Pages;

public class QuickConversionsModel : PageModel
{
    private readonly IConversionService _conversionService;

    public string Output { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public IEnumerable<SelectListItem> PoundOptions =>
    [
        new("1 pound", "1"),
        new("5 pounds", "5"),
        new("10 pounds", "10"),
        new("25 pounds", "25"),
        new("50 pounds", "50")
    ];

    public QuickConversionsModel(IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    public void OnGet()
    {
    }

    public IActionResult OnGetMilesToKilometers(string input)
    {
        return PerformConversion(
            input,
            ConversionTypes.MilesToKilometers);
    }

    public IActionResult OnGetKilometersToMiles(string input)
    {
        return PerformConversion(
            input,
            ConversionTypes.KilometersToMiles);
    }

    public IActionResult OnGetFahrenheitToCelsius(string input)
    {
        return PerformConversion(
            input,
            ConversionTypes.FahrenheitToCelsius);
    }

    public IActionResult OnGetCelsiusToFahrenheit(string input)
    {
        return PerformConversion(
            input,
            ConversionTypes.CelsiusToFahrenheit);
    }

    public IActionResult OnGetPoundsToKilograms(string input)
    {
        return PerformConversion(
            input,
            ConversionTypes.PoundsToKilograms);
    }

    public IActionResult OnGetKilogramsToPounds(string input)
    {
        return PerformConversion(
            input,
            ConversionTypes.KilogramsToPounds);
    }

    public IActionResult OnGetInchesToCentimeters(string input)
    {
        return PerformConversion(
            input,
            ConversionTypes.InchesToCentimeters);
    }

    public IActionResult OnGetCentimetersToInches(string input)
    {
        return PerformConversion(
            input,
            ConversionTypes.CentimetersToInches);
    }

    private IActionResult PerformConversion(string input, string conversionType)
    {
        if (!decimal.TryParse(input, out decimal value))
        {
            ErrorMessage = "Input invalid, input must be a valid number.";
            return Page();
        }

        try
        {
            decimal result = _conversionService.Convert(value, conversionType);
            Output = result.ToString();
        }
        catch (InvalidOperationException ex)
        {
            ErrorMessage = ex.Message;
        }

        return Page();
    }
}
