using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Deviot.Hermes.ModbusTcp.TDD.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class TestUtils
    {
        public static string GetGenericString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}
