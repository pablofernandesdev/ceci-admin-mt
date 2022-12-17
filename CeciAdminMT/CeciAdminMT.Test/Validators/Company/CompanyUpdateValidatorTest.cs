using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Service.Validators.Company;
using CeciAdminMT.Test.Fakers.Company;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CeciAdminMT.Test.Validators.Company
{
    public class CompanyUpdateValidatorTest
    {
        private readonly CompanyUpdateValidator _validator;
        private readonly Moq.Mock<IUnitOfWork> _mockUnitOfWork;

        public CompanyUpdateValidatorTest()
        {
            _mockUnitOfWork = new Moq.Mock<IUnitOfWork>();
            _validator = new CompanyUpdateValidator(_mockUnitOfWork.Object);
        }

        [Fact]
        public void There_should_be_an_error_when_properties_are_null()
        {
            //Arrange
            var model = new CompanyUpdateDTO { DocumentNumber = string.Empty };

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultNoTrackingAsync(x => x.Id.Equals(model.CompanyId)))
                .ReturnsAsync(value: null);

            //act
            var result = _validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(user => user.CompanyId);
            result.ShouldHaveValidationErrorFor(user => user.Name);
            result.ShouldHaveValidationErrorFor(user => user.DocumentNumber);
        }

        [Fact]
        public void There_should_not_be_an_error_for_the_properties()
        {
            //Arrange
            var model = CompanyFaker.CompanyUpdateDTO().Generate();

            _mockUnitOfWork.Setup(x => x.Company.GetFirstOrDefaultNoTrackingAsync(x => x.Id.Equals(model.CompanyId)))
                .ReturnsAsync(CompanyFaker.CompanyEntity().Generate());

            //act
            var result = _validator.TestValidate(model);

            //assert
            result.ShouldNotHaveValidationErrorFor(user => user.Name);
            result.ShouldNotHaveValidationErrorFor(user => user.DocumentNumber);
            //result.ShouldNotHaveValidationErrorFor(user => user.CompanyId);
        }
    }
}
