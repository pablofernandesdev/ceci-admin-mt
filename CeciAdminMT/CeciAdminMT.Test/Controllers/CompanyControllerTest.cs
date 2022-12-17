using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Domain.Interfaces.Service;
using CeciAdminMT.Test.Fakers.Commons;
using CeciAdminMT.Test.Fakers.Company;
using CeciAdminMT.WebApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CeciAdminMT.Test.Controllers
{
    public class CompanyControllerTest
    {
        private readonly Mock<ICompanyService> _mockCompanyService;

        public CompanyControllerTest()
        {
            _mockCompanyService = new Moq.Mock<ICompanyService>();
        }

        [Fact]
        public async Task Get_all()
        {
            //Arrange
            var companyResultDto = CompanyFaker.CompanyResultDTO().Generate(2);
            var companyFilterDto = CompanyFaker.CompanyFilterDTO().Generate();
            var resultDataResponse = ResultDataResponseFaker.ResultDataResponse<IEnumerable<CompanyResultDTO>>(companyResultDto, It.IsAny<HttpStatusCode>());

            _mockCompanyService.Setup(x => x.GetAsync(companyFilterDto))
                .ReturnsAsync(resultDataResponse);

            var companyController = CompanyControllerConstrutor();

            //Act
            var result = await companyController.Get(companyFilterDto);

            //Assert
            var objResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.IsType<ResultDataResponse<IEnumerable<CompanyResultDTO>>>(objResult.Value);
        }

        [Fact]
        public async Task Add_user()
        {
            //Arrange
            var companyAddDto = CompanyFaker.CompanyAddDTO().Generate();
            var resultResponse = ResultResponseFaker.ResultResponse(It.IsAny<HttpStatusCode>());

            _mockCompanyService.Setup(x => x.AddAsync(companyAddDto))
                .ReturnsAsync(resultResponse);

            var companyController = CompanyControllerConstrutor();

            //Act
            var result = await companyController.Add(companyAddDto);
            _mockCompanyService.Verify(x => x.AddAsync(companyAddDto), Times.Once);

            //Assert
            var objResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.IsType<ResultResponse>(objResult.Value);
        }

        [Fact]
        public async Task Update_user()
        {
            //Arrange
            var companyUpdateDto = CompanyFaker.CompanyUpdateDTO().Generate();
            var resultResponse = ResultResponseFaker.ResultResponse(It.IsAny<HttpStatusCode>());

            _mockCompanyService.Setup(x => x.UpdateAsync(companyUpdateDto))
                .ReturnsAsync(resultResponse);

            var companyController = CompanyControllerConstrutor();

            //Act
            var result = await companyController.Update(companyUpdateDto);
            _mockCompanyService.Verify(x => x.UpdateAsync(companyUpdateDto), Times.Once);

            //Assert
            var objResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.IsType<ResultResponse>(objResult.Value);
        }

        [Fact]
        public async Task Delete_user()
        {
            //Arrange
            var companyDeleteDto = CompanyFaker.CompanyDeleteDTO().Generate();
            var resultResponse = ResultResponseFaker.ResultResponse(It.IsAny<HttpStatusCode>());

            _mockCompanyService.Setup(x => x.DeleteAsync(companyDeleteDto))
                .ReturnsAsync(resultResponse);

            var companyController = CompanyControllerConstrutor();

            //Act
            var result = await companyController.Delete(companyDeleteDto);
            _mockCompanyService.Verify(x => x.DeleteAsync(companyDeleteDto), Times.Once);

            //Assert
            var objResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.IsType<ResultResponse>(objResult.Value);
        }

        [Fact]
        public async Task Get_user_by_id()
        {
            //Arrange
            var companyResultDto = CompanyFaker.CompanyResultDTO().Generate();
            var resultResponse = ResultResponseFaker.ResultResponseData<CompanyResultDTO>(companyResultDto, It.IsAny<HttpStatusCode>());

            _mockCompanyService.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(resultResponse);

            var companyController = CompanyControllerConstrutor();

            //Act
            var result = await companyController.GetById(new CompanyIdentifierDTO
            {
                CompanyId = 1
            });

            //Assert
            var objResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.IsType<ResultResponse<CompanyResultDTO>>(objResult.Value);
        }

        private CompanyController CompanyControllerConstrutor()
        {
            return new CompanyController(_mockCompanyService.Object);
        }
    }
}
