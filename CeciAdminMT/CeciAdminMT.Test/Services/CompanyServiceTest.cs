using AutoMapper;
using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Domain.Mapping;
using CeciAdminMT.Service.Services;
using CeciAdminMT.Test.Fakers.Company;
using Moq;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CeciAdminMT.Test.Services
{
    public class CompanyServiceTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public CompanyServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            
            //Auto mapper configuration
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Add_company_successfully()
        {
            //Arrange
            var companyEntityFaker = CompanyFaker.CompanyEntity().Generate();

            _mockUnitOfWork.Setup(x => x.Company.AddAsync(companyEntityFaker))
                .ReturnsAsync(companyEntityFaker);

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.AddAsync(CompanyFaker.CompanyAddDTO().Generate());

            //Assert
            Assert.True(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_to_add_company()
        {
            //Arrange
            var companyEntityFaker = CompanyFaker.CompanyEntity().Generate();

            _mockUnitOfWork.Setup(x => x.Company.AddAsync(companyEntityFaker))
                .ReturnsAsync(companyEntityFaker);

            _mockUnitOfWork.Setup(x => x.CommitAsync())
                .ThrowsAsync(new Exception());

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.AddAsync(CompanyFaker.CompanyAddDTO().Generate());

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Get_all_users()
        {
            //Arrange
            var companyEntityFaker = CompanyFaker.CompanyEntity().Generate(3);
            var companyFilterDto = CompanyFaker.CompanyFilterDTO().Generate();

            _mockUnitOfWork.Setup(x => x.Company.GetByFilterAsync(companyFilterDto))
                .ReturnsAsync(companyEntityFaker);

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.GetAsync(companyFilterDto);

            //Assert
            Assert.True(result.Data.Any() && result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_get_all_companies_by_filter()
        {
            //Arrange
            var companyFilterDto = CompanyFaker.CompanyFilterDTO().Generate();

            _mockUnitOfWork.Setup(x => x.Company.GetByFilterAsync(companyFilterDto))
                  .ThrowsAsync(new Exception());

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.GetAsync(companyFilterDto);

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Delete_company_successfully()
        {
            //Arrange
            var companyDeleteDTOFaker = CompanyFaker.CompanyDeleteDTO().Generate();

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultAsync(c => c.Id == companyDeleteDTOFaker.CompanyId))
                .ReturnsAsync(CompanyFaker.CompanyEntity().Generate());

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.DeleteAsync(companyDeleteDTOFaker);

            //Assert
            Assert.True(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_delete_company()
        {
            //Arrange
            var companyDeleteDTOFaker = CompanyFaker.CompanyDeleteDTO().Generate();

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultAsync(c => c.Id == companyDeleteDTOFaker.CompanyId))
               .ThrowsAsync(new Exception());

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.DeleteAsync(companyDeleteDTOFaker);

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Update_company_successfully()
        {
            //Arrange
            var companyUpdateDTOFaker = CompanyFaker.CompanyUpdateDTO().Generate();

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultAsync(c => c.Id == companyUpdateDTOFaker.CompanyId))
                .ReturnsAsync(CompanyFaker.CompanyEntity().Generate());

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.UpdateAsync(companyUpdateDTOFaker);

            //Assert
            Assert.True(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_update_company()
        {
            //Arrange
            var companyUpdateDTOFaker = CompanyFaker.CompanyUpdateDTO().Generate();

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultAsync(c => c.Id == companyUpdateDTOFaker.CompanyId))
                .ThrowsAsync(new Exception());

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.UpdateAsync(companyUpdateDTOFaker);

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Get_by_id_successfully()
        {
            //Arrange
            var companyEntityFaker = CompanyFaker.CompanyEntity().Generate();
            var companyId = companyEntityFaker.Id;

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultNoTrackingAsync(x => x.Id.Equals(companyId)))
                .ReturnsAsync(companyEntityFaker);

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.GetByIdAsync(companyId);

            //Assert
            Assert.True(result.Data != null && result.StatusCode.Equals(HttpStatusCode.OK));
        }

        [Fact]
        public async Task Failed_get_by_id()
        {
            //Arrange
            var companyEntityFaker = CompanyFaker.CompanyEntity().Generate();
            var companyId = companyEntityFaker.Id;

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultNoTrackingAsync(x => x.Id.Equals(companyId)))
                   .ThrowsAsync(new Exception());

            var companyService = CompanyServiceConstrutor();

            //Act
            var result = await companyService.GetByIdAsync(companyId);

            //Assert
            Assert.False(result.StatusCode.Equals(HttpStatusCode.OK));
        }

        private CompanyService CompanyServiceConstrutor()
        {
            return new CompanyService(_mockUnitOfWork.Object,
                _mapper);
        }
    }
}
