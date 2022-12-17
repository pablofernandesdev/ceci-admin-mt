using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Service.Validators.Company;
using FluentValidation.TestHelper;
using Xunit;

namespace CeciAdminMT.Test.Validators.Company
{
    public class CompanyIdentifierValidatorTest
    {
        private readonly CompanyIdentifierValidator _validator;

        public CompanyIdentifierValidatorTest()
        {
            _validator = new CompanyIdentifierValidator();
        }

        [Fact]
        public void There_should_be_an_error_when_properties_are_null()
        {
            //Arrange
            var model = new CompanyIdentifierDTO();

            //act
            var result = _validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(company => company.CompanyId);
        }

        [Fact]
        public void There_should_not_be_an_error_for_the_properties()
        {
            //Arrange
            var model = new CompanyIdentifierDTO
            {
                CompanyId = 1
            };

            //act
            var result = _validator.TestValidate(model);

            //assert
            result.ShouldNotHaveValidationErrorFor(company => company.CompanyId);
        }
    }
}
