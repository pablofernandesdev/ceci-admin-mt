using AutoMapper;
using CeciAdminMT.Domain.DTO.User;
using CeciAdminMT.Domain.Entities;
using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Domain.Interfaces.Service;
using CeciAdminMT.Domain.Mapping;
using CeciAdminMT.Service.Services;
using CeciAdminMT.Test.Fakers.Commons;
using CeciAdminMT.Test.Fakers.Email;
using CeciAdminMT.Test.Fakers.User;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace CeciAdminMT.Test.Services
{
    public class UserServiceTest
    {
        private readonly string _claimNameIdentifier = "1";
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IBackgroundJobClient> _mockBackgroundJobClient;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly IMapper _mapper;

        public UserServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEmailService = new Mock<IEmailService>();
            _mockBackgroundJobClient = new Mock<IBackgroundJobClient>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            //Auto mapper configuration
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();

            //http context configuration
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, _claimNameIdentifier),
                new Claim("custom-claim", "example claim value"),
            }, "mock"));

            _mockHttpContextAccessor.Setup(h => h.HttpContext.User).Returns(user);
        }

        [Fact]
        public async Task Add_user_successfully()
        {
            //Arrange
            var userEntityFaker = UserFaker.UserEntity().Generate();
            var emailRequestDTOFaker = EmailFaker.EmailRequestDTO().Generate();

            _mockUnitOfWork.Setup(x => x.User.AddAsync(userEntityFaker))
                .ReturnsAsync(userEntityFaker);

            _mockEmailService.Setup(x => x.SendEmailAsync(emailRequestDTOFaker))
                .ReturnsAsync(ResultResponseFaker.ResultResponse(HttpStatusCode.OK).Generate());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.AddAsync(UserFaker.UserAddDTO().Generate());

            //Assert
            Assert.True(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_to_add_user()
        {
            //Arrange
            var userEntityFaker = UserFaker.UserEntity().Generate();
            var emailRequestDTOFaker = EmailFaker.EmailRequestDTO().Generate();

            _mockUnitOfWork.Setup(x => x.User.AddAsync(userEntityFaker))
                .ReturnsAsync(userEntityFaker);

            _mockUnitOfWork.Setup(x => x.CommitAsync())
                .ThrowsAsync(new Exception());

            _mockEmailService.Setup(x => x.SendEmailAsync(emailRequestDTOFaker))
                .ReturnsAsync(ResultResponseFaker.ResultResponse(HttpStatusCode.BadRequest).Generate());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.AddAsync(UserFaker.UserAddDTO().Generate());

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Get_all_users()
        {
            //Arrange
            var userEntityFaker = UserFaker.UserEntity().Generate(3);
            var userFilterDto = UserFaker.UserFilterDTO().Generate();

            _mockUnitOfWork.Setup(x => x.User.GetByFilterAsync(userFilterDto))
                .ReturnsAsync(userEntityFaker);

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.GetAsync(userFilterDto);

            //Assert
            Assert.True(result.Data.Any() && result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_get_all_users_by_filter()
        {
            //Arrange
            var userFilterDto = UserFaker.UserFilterDTO().Generate();
            _mockUnitOfWork.Setup(x => x.User.GetByFilterAsync(userFilterDto))
                .ThrowsAsync(new Exception());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.GetAsync(userFilterDto);

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Delete_user_successfully()
        {
            //Arrange
            var userDeleteDTOFaker = UserFaker.UserDeleteDTO().Generate();

            _mockUnitOfWork.Setup(x => x.User.GetFirstOrDefaultAsync(c => c.Id == userDeleteDTOFaker.UserId))
                .ReturnsAsync(UserFaker.UserEntity().Generate());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.DeleteAsync(userDeleteDTOFaker);

            //Assert
            Assert.True(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_delete_user()
        {
            //Arrange
            var userDeleteDTOFaker = UserFaker.UserDeleteDTO().Generate();

            _mockUnitOfWork.Setup(x => x.User.GetFirstOrDefaultAsync(c => c.Id == userDeleteDTOFaker.UserId))
                .ThrowsAsync(new Exception());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.DeleteAsync(userDeleteDTOFaker);

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Update_user_successfully()
        {
            //Arrange
            var userUpdateDTOFaker = UserFaker.UserUpdateDTO().Generate();

            _mockUnitOfWork.Setup(x => x.User.GetFirstOrDefaultAsync(c => c.Email == userUpdateDTOFaker.Email && c.Id != userUpdateDTOFaker.UserId))
                .ReturnsAsync(value: null);

            _mockUnitOfWork.Setup(x => x.User.GetFirstOrDefaultAsync(c => c.Id == userUpdateDTOFaker.UserId))
                .ReturnsAsync(_mapper.Map<User>(userUpdateDTOFaker));

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.UpdateAsync(userUpdateDTOFaker);

            //Assert
            Assert.True(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_to_update_user_with_already_registered_email()
        {
            //Arrange
            var userUpdateDTOFaker = UserFaker.UserUpdateDTO().Generate();

            _mockUnitOfWork.Setup(x => x.User.GetFirstOrDefaultAsync(c => c.Email == userUpdateDTOFaker.Email && c.Id != userUpdateDTOFaker.UserId))
                .ReturnsAsync(UserFaker.UserEntity().Generate());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.UpdateAsync(userUpdateDTOFaker);

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_update_user()
        {
            //Arrange
            var userUpdateDTOFaker = UserFaker.UserUpdateDTO().Generate();

            _mockUnitOfWork.Setup(x => x.User.GetFirstOrDefaultAsync(c => c.Email == userUpdateDTOFaker.Email && c.Id != userUpdateDTOFaker.UserId))
                .ThrowsAsync(new Exception());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.UpdateAsync(userUpdateDTOFaker);

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Update_user_role_successfully()
        {
            //Arrange
            var userEntityFaker = UserFaker.UserEntity().Generate();
            var userRoleUpdateDTOFaker = _mapper.Map<UserUpdateRoleDTO>(userEntityFaker);

            _mockUnitOfWork.Setup(x => x.User.GetFirstOrDefaultAsync(c => c.Id == userRoleUpdateDTOFaker.UserId))
                .ReturnsAsync(userEntityFaker);

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.UpdateRoleAsync(userRoleUpdateDTOFaker);

            //Assert
            Assert.True(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Update_user_role_exception()
        {
            //Arrange
            var userEntityFaker = UserFaker.UserEntity().Generate();
            var userRoleUpdateDTOFaker = _mapper.Map<UserUpdateRoleDTO>(userEntityFaker);

            _mockUnitOfWork.Setup(x => x.User.GetFirstOrDefaultAsync(c => c.Id == userRoleUpdateDTOFaker.UserId))
                .ThrowsAsync(new Exception());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.UpdateRoleAsync(userRoleUpdateDTOFaker);

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Get_user_by_id_successfully()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.User.GetUserByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(UserFaker.UserEntity().Generate());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.GetByIdAsync(It.IsAny<int>());

            //Assert
            Assert.True(result.Data != null && result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Get_user_by_id_exception()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.User.GetUserByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var userService = UserServiceConstrutor();

            //Act
            var result = await userService.GetByIdAsync(It.IsAny<int>());

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        private UserService UserServiceConstrutor()
        {
            return new UserService(_mockUnitOfWork.Object,
                _mapper, 
                _mockEmailService.Object, 
                _mockBackgroundJobClient.Object);
        }
    }
}
