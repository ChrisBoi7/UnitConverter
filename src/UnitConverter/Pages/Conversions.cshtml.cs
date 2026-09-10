using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string ConversionType {get; set;} = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Input { get; set; } = string.Empty;

    public string Output { get; set; } = string.Empty;

    public void OnGet()
    {
        ViewData["Title"] = "Conversions";

        if (string.IsNullOrEmpty(ConversionType))
        {
            ConversionType = "MilesToKilometers";
        }

        if (string.IsNullOrEmpty(Input))
        {
            Input = "3.1415";
        }

        if (ConversionType.ToLower() == "milestokilometers")
        {
            ViewData["ConversionType"] = "Miles to Kilometers";
        }

        else
        {
            ViewData["ConversionType"] = ConversionType;
        }

        double numberInput = 0;

        try
        {
            numberInput = Convert.ToDouble(Input);
        }
        catch (FormatException)
        {
            ViewData["ErrorMessage"] = "Input invalid, input must be a valid number.";
            return;
        }

        try
        {
            Output = ConversionType.ToLower() switch
            {
                "milestokilometers" => new UnitOf.Length().FromMiles(numberInput).ToKilometers().ToString(),
                "kilometerstomiles" => new UnitOf.Length().FromKilometers(numberInput).ToMiles().ToString(),
                "fahrenheittocelsius" => new UnitOf.Temperature().FromFahrenheit(numberInput).ToCelsius().ToString(),
                "celsiustofahrenheit" => new UnitOf.Temperature().FromCelsius(numberInput).ToFahrenheit().ToString(),
                "poundstokilograms" => new UnitOf.Mass().FromPounds(numberInput).ToKilograms().ToString(),
                "kilogramstopounds" => new UnitOf.Mass().FromKilograms(numberInput).ToPounds().ToString(),
                "inchestocentimeters" => new UnitOf.Length().FromInches(numberInput).ToCentimeters().ToString(),
                "centimeterstoinches" => new UnitOf.Length().FromCentimeters(numberInput).ToInches().ToString(),


                _ => throw new InvalidOperationException("Unknown conversion type.")
            };


        }
        catch (InvalidOperationException ex)
        {
            ViewData["ErrorMessage"] = ex.Message;
        }
        catch (Exception)
        {
            ViewData["ErrorMessage"] = "Error!, An error occurred during conversion calculation.";
        }
    }
}
