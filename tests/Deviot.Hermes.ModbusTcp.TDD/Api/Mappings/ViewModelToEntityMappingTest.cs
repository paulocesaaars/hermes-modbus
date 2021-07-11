using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using FluentAssertions;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Api.Mappings
{
    public class ViewModelToEntityMappingTest : MappingBaseTest
    {
        [Fact]
        public void LoginViewModelForLogin()
        {
            var viewModel = LoginFake.GetLoginAdmin();
            var entity = _mapper.Map<Login>(viewModel);

            entity.Should().NotBeNull();
            entity.Id.Should().NotBeEmpty();
            entity.UserName.Should().Equals(viewModel.UserName);
            entity.Password.Should().Equals(viewModel.Password);
        }

        [Fact]
        public void UserViewModelForUser()
        {
            var viewModel = UserFake.GetUserAdmin();
            var entity = _mapper.Map<User>(viewModel);

            entity.Should().NotBeNull();
            entity.Id.Should().NotBeEmpty();
            entity.FullName.Should().Equals(viewModel.FullName);
            entity.UserName.Should().Equals(viewModel.UserName);
            entity.Password.Should().Equals(viewModel.Password);
            entity.Enabled.Should().Equals(viewModel.Enabled);
            entity.Administrator.Should().Equals(viewModel.Administrator);
        }

        [Fact]
        public void UserInfoViewModelForUserInfo()
        {
            var viewModel = UserInfoFake.GetUserAdmin();
            var entity = _mapper.Map<UserInfo>(viewModel);

            entity.Should().NotBeNull();
            entity.Id.Should().Equals(viewModel.Id);
            entity.FullName.Should().Equals(viewModel.FullName);
            entity.UserName.Should().Equals(viewModel.UserName);
            entity.Enabled.Should().Equals(viewModel.Enabled);
            entity.Administrator.Should().Equals(viewModel.Administrator);
        }

        [Fact]
        public void UserPasswordViewModelForUserPassword()
        {
            var viewModel = UserPasswordFake.GetPasswordAdminViewModel();
            var entity = _mapper.Map<UserPassword>(viewModel);

            entity.Should().NotBeNull();
            entity.Id.Should().Equals(viewModel.Id);
            entity.Password.Should().Equals(viewModel.Password);
            entity.NewPassword.Should().Equals(viewModel.NewPassword);
        }
    }
}
