using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures.Controllers
{
    [ExcludeFromCodeCoverage]
    [CollectionDefinition(nameof(EntityMappingCollection))]
    public class EntityMappingCollection : ICollectionFixture<EntityMappingFixture>
    {
    }

    public class EntityMappingFixture : ServiceFixtureBase, IDisposable
    {
        public UserInfo GetUserInfo()
        {
            var user = UserFake.GetUserAdmin();
            return new UserInfo(user.Id, user.FullName, user.UserName, user.Enabled, user.Administrator);
        }

        public Token GetToken()
        {
            return TokenHelper.GetToken(UserFake.GetUserAdmin());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
