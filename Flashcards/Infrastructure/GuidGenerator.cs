using System;

namespace Flashcards.Infrastructure
{
    public class GuidGenerator
    {
        public static string GenerateGuid() => Guid.NewGuid().ToString();
    }
}