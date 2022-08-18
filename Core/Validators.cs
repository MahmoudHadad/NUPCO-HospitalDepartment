namespace Core
{
    public static class Validators
    {
        public static bool Exists<T>(this IEnumerable<T>? enumerable)
        {
            return enumerable != null && enumerable.Any();
        }
        public static bool Exists<T>(this T? enumerable)
        {
            return enumerable != null;
        }
    }
}
