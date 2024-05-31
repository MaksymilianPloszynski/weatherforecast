namespace InterviewConsoleApp;

public static class ListExtension
{
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Predicate<T> predicate) where T : struct
    {
        foreach (var element in list)
        {
            if (predicate(element))
            {
                yield return element;
            }
        }
    }
    
}