using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Services;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures.Collections
{
    [CollectionDefinition(nameof(ServicesCollection))]
    public class ServicesCollection : 
        ICollectionFixture<AuthServiceFixture>,
        ICollectionFixture<UserServiceFixture>
    {
    }
}
