using System;

namespace Flashcards
{
    public static class GuidGenerator
    {
        public static string GenerateGuid() => Guid.NewGuid().ToString();
    }
}