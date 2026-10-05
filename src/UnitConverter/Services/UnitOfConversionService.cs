using UnitConverter.Models;

namespace UnitConverter.Services;

public class UnitOfConversionService : IConversionService
{
    public decimal Convert(decimal value, string conversionType)
    {
        double numberInput = (double)value;

        double result = conversionType switch
        {
            ConversionTypes.MilesToKilometers =>
                new UnitOf.Length()
                    .FromMiles(numberInput)
                    .ToKilometers(),

            ConversionTypes.KilometersToMiles =>
                new UnitOf.Length()
                    .FromKilometers(numberInput)
                    .ToMiles(),

            ConversionTypes.FahrenheitToCelsius =>
                new UnitOf.Temperature()
                    .FromFahrenheit(numberInput)
                    .ToCelsius(),

            ConversionTypes.CelsiusToFahrenheit =>
                new UnitOf.Temperature()
                    .FromCelsius(numberInput)
                    .ToFahrenheit(),

            ConversionTypes.PoundsToKilograms =>
                new UnitOf.Mass()
                    .FromPounds(numberInput)
                    .ToKilograms(),

            ConversionTypes.KilogramsToPounds =>
                new UnitOf.Mass()
                    .FromKilograms(numberInput)
                    .ToPounds(),

            ConversionTypes.InchesToCentimeters =>
                new UnitOf.Length()
                    .FromInches(numberInput)
                    .ToCentimeters(),

            ConversionTypes.CentimetersToInches =>
                new UnitOf.Length()
                    .FromCentimeters(numberInput)
                    .ToInches(),

            _ => throw new InvalidOperationException(
                "Unknown conversion type.")
        };

        return (decimal)result;
    }
}
