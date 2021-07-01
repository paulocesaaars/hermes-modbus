using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.BDD.Extensions
{
    [ExcludeFromCodeCoverage]

    public class TestActionResult<T>
    {
        public IEnumerable<string> Messages { get; set; }

        public T Data { get; set; }
    }
}
