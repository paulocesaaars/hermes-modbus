using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Controllers;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Api.Mappings
{
    [ExcludeFromCodeCoverage]
    [Collection(nameof(EntityMappingCollection))]
    public class EntityToViewModelMappingTest : MappingBaseTest
    {
        private readonly EntityMappingFixture _entityMappingFixture;

        public EntityToViewModelMappingTest(EntityMappingFixture entityMappingFixture)
        {
            _entityMappingFixture = entityMappingFixture;
        }

        [Fact]
        public void TokenForTokenViewModel()
        {
            var entity = _entityMappingFixture.GetToken();
            var viewModel = _mapper.Map<TokenViewModel>(entity);

            viewModel.Should().NotBeNull();
            viewModel.AccessToken.Should().Equals(entity.AccessToken);
            viewModel.User.Id.Should().Equals(entity.User.Id);
            viewModel.User.FullName.Should().Equals(entity.User.FullName);
            viewModel.User.UserName.Should().Equals(entity.User.UserName);
            viewModel.User.Enabled.Should().Equals(entity.User.Enabled);
            viewModel.User.Administrator.Should().Equals(entity.User.Administrator);
        }

        [Fact]
        public void UserInfoForUserInfoViewModel()
        {
            var entity = _entityMappingFixture.GetUserInfo();
            var viewModel = _mapper.Map<UserInfoViewModel>(entity);

            viewModel.Should().NotBeNull();
            viewModel.Id.Should().Equals(entity.Id);
            viewModel.FullName.Should().Equals(entity.FullName);
            viewModel.UserName.Should().Equals(entity.UserName);
            viewModel.Enabled.Should().Equals(entity.Enabled);
            viewModel.Administrator.Should().Equals(entity.Administrator);
        }
    }
}
