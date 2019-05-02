using System;

namespace Flashcards
{
    public class GuidGenerator
    {
        public static string GenerateGuid() => Guid.NewGuid().ToString();
    }
}