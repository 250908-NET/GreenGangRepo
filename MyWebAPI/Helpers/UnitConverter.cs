namespace MyWebAPI.Helpers
{
    public static class UnitConverter
    {
        public static double ConvertLength(double value, string fromUnit, string toUnit)
        {
            var toMeters = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
            {
                { "meters", 1.0 },
                { "inches", 0.0254 },
                { "feet", 0.3048 }
            };

            if (!toMeters.ContainsKey(fromUnit) || !toMeters.ContainsKey(toUnit))
            {
                throw new ArgumentException("Unsupported unit.");
            }

            double valueInMeters = value * toMeters[fromUnit];
            double convertedValue = valueInMeters / toMeters[toUnit];
            return convertedValue;
        }

        public static double ConvertWeight(double value, string fromUnit, string toUnit)
        {
            var toKilograms = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
            {
                {"kg", 1.0 },
                {"lbs", 0.453592 },
                {"ounces", 0.0283495 }
            };

            if (!toKilograms.ContainsKey(fromUnit) || !toKilograms.ContainsKey(toUnit))
            {
                throw new ArgumentException("Unsupported unit.");
            }

            double valueInKilograms = value * toKilograms[fromUnit];
            double convertedValue = valueInKilograms / toKilograms[toUnit];
            return convertedValue;
        }

        public static double ConvertVolume(double value, string fromUnit, string toUnit)
        {
            var toLiters = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
            {
                {"liters", 1.0 },
                {"gallons", 3.78541 },
                {"cups", 0.24 }
            };

            if (!toLiters.ContainsKey(fromUnit) || !toLiters.ContainsKey(toUnit))
            {
                throw new ArgumentException("Unsupported unit.");
            }

            double valueInLiters = value * toLiters[fromUnit];
            double convertedValue = valueInLiters / toLiters[toUnit];
            return convertedValue;
        }
    }
}