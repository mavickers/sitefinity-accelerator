namespace SitefinityAccelerator.Extensions
{
    public static class IntExtensions
    {
        public static int ParsedIntOrDefault(this object source, int defaultValue)
        {
            var parsedValue = defaultValue;

            return int.TryParse(source.ToString(), out parsedValue)
                ? parsedValue
                : defaultValue;
        }
    }
}