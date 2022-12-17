﻿using CeciAdminMT.Domain.DTO.User;
using CeciAdminMT.Service.Validators.User;
using FluentValidation.TestHelper;
using Xunit;

namespace CeciAdminMT.Test.Validators.User
{
    public class UserIdentifierValidatorTest
    {
        private readonly UserIdentifierValidator _validator;

        public UserIdentifierValidatorTest()
        {
            _validator = new UserIdentifierValidator();
        }

        [Fact]
        public void There_should_be_an_error_when_properties_are_null()
        {
            //Arrange
            var model = new UserIdentifierDTO();

            //act
            var result = _validator.TestValidate(model);

            //assert
            result.ShouldHaveValidationErrorFor(user => user.UserId);
        }

        [Fact]
        public void There_should_not_be_an_error_for_the_properties()
        {
            //Arrange
            var model = new UserIdentifierDTO { 
                UserId = 1};

            //act
            var result = _validator.TestValidate(model);

            //assert
            result.ShouldNotHaveValidationErrorFor(user => user.UserId);
        }
    }
}
