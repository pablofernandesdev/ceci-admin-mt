using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Service.Validators.Company;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace CeciAdminMT.Test.Validators.Company
{
    public class CompanyDeleteValidatorTest
    {
        private readonly CompanyDeleteValidator _validator;
        private readonly Moq.Mock<IUnitOfWork> _mockUnitOfWork;

        public CompanyDeleteValidatorTest()
        {
            _mockUnitOfWork = new Moq.Mock<IUnitOfWork>();
            _validator = new CompanyDeleteValidator(_mockUnitOfWork.Object);
        }

        [Fact]
        public void There_should_be_an_error_when_properties_are_null()
        {
            //Arrange
            var model = new CompanyDeleteDTO();

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultNoTrackingAsync(x => x.Id.Equals(model.CompanyId)))
                .ReturnsAsync(value: null);

            //act
            var result = _validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(company => company.CompanyId);
        }

        /*[Fact]
        public void There_should_not_be_an_error_for_the_properties()
        {
            //Arrange
            var model = new CompanyDeleteDTO { 
                CompanyId = 1};

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultNoTrackingAsync(x => x.Id.Equals(model.CompanyId)))
                .ReturnsAsync(CompanyFaker.CompanyEntity().Generate());

            //act
            var result = validator.TestValidate(model);

            //assert
            result.ShouldNotHaveValidationErrorFor(company => company.CompanyId);
        }*/
    }
}
