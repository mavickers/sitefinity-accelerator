namespace SitefinityAccelerator.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToOnOffString(this bool source)
        {
            return source ? "on" : "off";
        }
    }
}