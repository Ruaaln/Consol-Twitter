namespace Consol_Twitter.Consol_Help;

public static class ConsoleHelper
{
    public static void PrintColored(string text, ConsoleColor color)
    {
        var oldColor = Console.ForegroundColor; // Mövcud rəngi yadda saxla
        Console.ForegroundColor = color;        // Yeni rəngi təyin et
        Console.WriteLine(text);                // Mətn çıxart
        Console.ForegroundColor = oldColor;     // Əvvəlki rəngə qayıt
    }
}
