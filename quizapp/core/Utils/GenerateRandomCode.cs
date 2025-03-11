using System;

namespace core.Utils;

public class GenerateRandomCode
{
    private static readonly Random _random = new();
    private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string GenerateQuizCode(int length = 9)
    {
        return new string([.. Enumerable.Range(0, length).Select(_ => Characters[_random.Next(Characters.Length)])]);
    }
}
