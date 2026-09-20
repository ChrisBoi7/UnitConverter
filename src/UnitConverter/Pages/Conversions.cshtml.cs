using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string ConversionType { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Input { get; set; } = string.Empty;

    public string Output { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public ConversionModel Conversion { get; set; } = new();

    public void OnGet()
    {
        ViewData["Title"] = "Conversions";

        if (!string.IsNullOrEmpty(Conversion.ConversionType))
        {
            ConversionType = Conversion.ConversionType;
        }

        if (!string.IsNullOrEmpty(Conversion.Input))
        {
            Input = Conversion.Input;
        }

        if (string.IsNullOrEmpty(ConversionType))
        {
            ConversionType = ConversionTypes.MilesToKilometers;
        }

        if (string.IsNullOrEmpty(Input))
        {
            Input = "3.1415";
        }

        string? selectedConversion = ConversionTypes.All.Keys.FirstOrDefault(
            key => string.Equals(
                key,
                ConversionType,
                StringComparison.OrdinalIgnoreCase));

        if (selectedConversion == null)
        {
            ViewData["ErrorMessage"] = "Unknown conversion type.";
            return;
        }

        ConversionType = selectedConversion;

        ViewData["ConversionType"] =
            ConversionTypes.All[selectedConversion];

        double numberInput;

        try
        {
            numberInput = Convert.ToDouble(Input);
        }
        catch (FormatException)
        {
            ViewData["ErrorMessage"] =
                "Input invalid, input must be a valid number.";
            return;
        }

        try
        {
            Output = selectedConversion switch
            {
                ConversionTypes.MilesToKilometers =>
                    new UnitOf.Length()
                        .FromMiles(numberInput)
                        .ToKilometers()
                        .ToString(),

                ConversionTypes.KilometersToMiles =>
                    new UnitOf.Length()
                        .FromKilometers(numberInput)
                        .ToMiles()
                        .ToString(),

                ConversionTypes.FahrenheitToCelsius =>
                    new UnitOf.Temperature()
                        .FromFahrenheit(numberInput)
                        .ToCelsius()
                        .ToString(),

                ConversionTypes.CelsiusToFahrenheit =>
                    new UnitOf.Temperature()
                        .FromCelsius(numberInput)
                        .ToFahrenheit()
                        .ToString(),

                ConversionTypes.PoundsToKilograms =>
                    new UnitOf.Mass()
                        .FromPounds(numberInput)
                        .ToKilograms()
                        .ToString(),

                ConversionTypes.KilogramsToPounds =>
                    new UnitOf.Mass()
                        .FromKilograms(numberInput)
                        .ToPounds()
                        .ToString(),

                ConversionTypes.InchesToCentimeters =>
                    new UnitOf.Length()
                        .FromInches(numberInput)
                        .ToCentimeters()
                        .ToString(),

                ConversionTypes.CentimetersToInches =>
                    new UnitOf.Length()
                        .FromCentimeters(numberInput)
                        .ToInches()
                        .ToString(),

                _ => throw new InvalidOperationException(
                    "Unknown conversion type.")
            };
        }
        catch (InvalidOperationException ex)
        {
            ViewData["ErrorMessage"] = ex.Message;
            return;
        }
        catch (Exception)
        {
            ViewData["ErrorMessage"] =
                "Error!, An error occurred during conversion calculation.";
            return;
        }

        Conversion.ConversionType = ConversionType;
        Conversion.Input = Input;
        Conversion.Output = Output;
    }
}
